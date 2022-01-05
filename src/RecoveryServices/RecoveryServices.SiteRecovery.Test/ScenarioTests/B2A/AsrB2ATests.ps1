# ----------------------------------------------------------------------------------
#
# Copyright Microsoft Corporation
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# http://www.apache.org/licenses/LICENSE-2.0
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
# ----------------------------------------------------------------------------------

########################## Site Recovery Tests #############################

$JobQueryWaitTimeInSeconds = 0
$ResourceGroupName = "E2ERg"
$VaultName = "E2ETest"
$PrimaryFabricName = "ForPowershell"

$RecoveryFabricName = "IDCLAB-A147.ntdev.corp.microsoft.com"
$PolicyName = "B2APolicyTest1"
$PrimaryProtectionContainerName = "ForPowershell"
$RecoveryProtectionContainerName = "recovery"
$ProtectionContainerMappingName = "B2AClP26mapping"
$PrimaryNetworkFriendlyName = "corp"
$RecoveryNetworkFriendlyName = "corp"
$NetworkMappingName = "corp96map"
$VMName = "PowershellVm1"
$RecoveryPlanName = "RPSwag96"
$VmList = "PowershellVm1,PowershellVm2"
$RecoveryAzureStorageAccountId = "/subscriptions/b364ed8d-4279-4bf8-8fd1-56f8fa0ae05c/resourceGroups/Arpita-air/providers/Microsoft.Storage/storageAccounts/sah2atest" 
$RecoveryResourceGroupId  = "/subscriptions/b364ed8d-4279-4bf8-8fd1-56f8fa0ae05c/resourceGroups/prakccyrg" 
$AzureVmNetworkId = "/subscriptions/b364ed8d-4279-4bf8-8fd1-56f8fa0ae05c/resourceGroups/Arpita-air/providers/Microsoft.Network/virtualNetworks/vnh2atest"

$AzureNetworkID = $AzureVmNetworkId
$subnet = "Subnet1"
$storageAccountId = $RecoveryAzureStorageAccountId
$PrimaryCloudName = $PrimaryFabricName
$ProtectionProfileName = $PolicyName
$avZone = "1"

<#
.SYNOPSIS
Wait for job completion
Usage:
    WaitForJobCompletion -JobId $Job.ID
    WaitForJobCompletion -JobId $Job.ID -NumOfSecondsToWait 10
#>
function WaitForJobCompletion
{ 
    param(
        [string] $JobId,
        [int] $JobQueryWaitTimeInSeconds = 0,
        [string] $Message = "NA"
        )
        $isJobLeftForProcessing = $true;
        do
        {
            $Job = Get-AzRecoveryServicesAsrJob -Name $JobId
            Write-Host $("Job Status:") -ForegroundColor Green
            $Job

            if($Job.State -eq "InProgress" -or $Job.State -eq "NotStarted")
            {
	            $isJobLeftForProcessing = $true
            }
            else
            {
                $isJobLeftForProcessing = $false
            }

            if($isJobLeftForProcessing)
	        {
                if($Message -ne "NA")
                {
                    Write-Host $Message -ForegroundColor Yellow
                }
                else
                {
                    Write-Host $($($Job.JobType) + " in Progress...") -ForegroundColor Yellow
                }
		        Write-Host $("Waiting for: " + $JobQueryWaitTimeInSeconds.ToString() + " Seconds") -ForegroundColor Yellow
		        [Microsoft.Rest.ClientRuntime.Azure.TestFramework.TestUtilities]::Wait($JobQueryWaitTimeInSeconds * 1000)
	        }
        }While($isJobLeftForProcessing)
}

<#
.SYNOPSIS
Wait for IR job completion
Usage:
    WaitForJobCompletion -VM $VM
    WaitForJobCompletion -VM $VM -NumOfSecondsToWait 10
#>
Function WaitForIRCompletion
{ 
    param(
        [PSObject] $VM,
        [int] $JobQueryWaitTimeInSeconds = 60
        )
        $isProcessingLeft = $true
        $IRjobs = $null

        Write-Host $("IR in Progress...") -ForegroundColor Yellow
        do
        {
            $IRjobs = Get-AzRecoveryServicesAsrJob -TargetObjectId $VM.Name | Sort-Object StartTime -Descending | select -First 5 | Where-Object{$_.JobType -eq "IrCompletion"}
            if($IRjobs -eq $null -or $IRjobs.Count -ne 1)
            {
                $isProcessingLeft = $true
            }
            else
            {
                $isProcessingLeft = $false
            }

            if($isProcessingLeft)
            {
                Write-Host $("IR in Progress...") -ForegroundColor Yellow
                Write-Host $("Waiting for: " + $JobQueryWaitTimeInSeconds.ToString() + " Seconds") -ForegroundColor Yellow
                [Microsoft.Rest.ClientRuntime.Azure.TestFramework.TestUtilities]::Wait($JobQueryWaitTimeInSeconds * 1000)
            }
        }While($isProcessingLeft)

        Write-Host $("Finalize IR jobs:") -ForegroundColor Green
        $IRjobs
        WaitForJobCompletion -JobId $IRjobs[0].Name -JobQueryWaitTimeInSeconds $JobQueryWaitTimeInSeconds -Message $("Finalize IR in Progress...")
}

<#
.SYNOPSIS
Site Recovery Enumeration Tests
#>
function Test-CreateFabric
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath

    $currentJob = New-AzRecoveryServicesAsrFabric -Name $PrimaryCloudName
	$currentJob
	WaitForJobCompletion -JobId $currentJob.Name
}

<#
.SYNOPSIS
Site Recovery Create Policy Test
#>
function Test-CreatePolicy
{
    param([string] $vaultSettingsFilePath)
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath
    $name = "h2avault"
    $resourceGroupName = "h2arg"
    $CredsPath = "."
    $SiteName = "h2asite"
		$vault = Get-AzRecoveryServicesVault -Name $name -ResourceGroupName $resourceGroupName
        #$job = New-AsrFabric -Type HyperVSite -Name $SiteName
        $SiteIdentifier = "2c4ba2cf-b87e-50e8-89fe-a0912c022a57" #Get-AsrFabric -Name $SiteName | Select -ExpandProperty SiteIdentifier
        #WaitForJobCompletion -JobId $job.Name

        #$cert = New-SelfSignedCertificate -CertStoreLocation Cert:\CurrentUser\My -FriendlyName $CertName -subject "Windows Azure Tools" -KeyExportPolicy Exportable -NotAfter $(Get-Date).AddHours(48) -NotBefore $(Get-Date).AddHours(-24) -KeyProtection None -KeyUsage None -TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.2") -Provider "Microsoft Enhanced Cryptographic Provider v1.0"
#$certficate = [convert]::ToBase64String($cert.Export([System.Security.Cryptography.X509Certificates.X509ContentType]::Pfx))
$certficate ="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"
$CredsFilename = Get-AzRecoveryServicesVaultSettingsFile -Vault $vault -Path $CredsPath -Certificate $certficate -SiteFriendlyName $SiteName -SiteIdentifier $SiteIdentifier

		# 2. Get-AzRecoveryServicesVaultSettingsFile
		#$file = Get-AzRecoveryServicesVaultSettingsFile -Vault $vault -Backup

    # Import Azure RecoveryServices Vault Settings File
    #Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath
    $currentJob = New-AzRecoveryServicesAsrPolicy -Name $ProtectionProfileName -ReplicationProvider HyperVReplicaAzure -ReplicationFrequencyInSeconds 30 -RecoveryPoints 1 -ApplicationConsistentSnapshotFrequencyInHours 0 -RecoveryAzureStorageAccountId $StorageAccountID
    WaitForJobCompletion -JobId $currentJob.Name
    $ProtectionProfile = Get-AzRecoveryServicesAsrPolicy -Name $ProtectionProfileName
    $ProtectionProfile
    
    $Policy = Get-AzRecoveryServicesAsrPolicy -Name $PolicyName
    Assert-True { $Policy.Count -gt 0 }
    Assert-NotNull($Policy)
}

<#
.SYNOPSIS
Site Recovery remove Policy Test
#>
function Test-SiteRecoveryRemovePolicy
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath

    # Get a policy created in previous test
    $Policy = Get-AzRecoveryServicesAsrPolicy -Name $PolicyName
    Assert-True { $Policy.Count -gt 0 }
    Assert-NotNull($Policy)

    # Delete the profile
    $Job = Remove-AzRecoveryServicesAsrPolicy -Policy $Policy
    #WaitForJobCompletion -JobId $Job.Name
}

<#
.SYNOPSIS
Site Recovery remove Policy Test
#>
function Test-RemoveFabric
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath

    # Get a policy created in previous test
    $fabric = Get-AzRecoveryServicesAsrFabric -FriendlyName $PrimaryFabricName 
    $job = Remove-ASRFabric -InputObject $fabric
    WaitForJobCompletion -JobId $job.Name

    Get-AzRecoveryServicesAsrFabric|Remove-ASRFabric
    #WaitForJobCompletion -JobId $Job.Name
}
<#
.SYNOPSIS
Site Recovery new protection container mapping test
#>
function Test-CreatePCMap
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath
    $Policy = Get-AzRecoveryServicesAsrPolicy -Name $PolicyName
    $PrimaryProtectionContainer = Get-AzRecoveryServicesAsrFabric -FriendlyName $PrimaryFabricName| Get-AzRecoveryServicesAsrProtectionContainer
    
	$currentJob = New-AzRecoveryServicesAsrProtectionContainerMapping -Name $ProtectionContainerMappingName -Policy $Policy -PrimaryProtectionContainer  $PrimaryProtectionContainer
    $currentJob
    WaitForJobCompletion -JobId $currentJob.Name 
   
    # Get protection conatiner mapping
    $ProtectionContainerMapping = Get-AzRecoveryServicesAsrProtectionContainerMapping -Name $ProtectionContainerMappingName -ProtectionContainer $PrimaryProtectionContainer
    Assert-NotNull($ProtectionContainerMapping)
}

<#
.SYNOPSIS
Site Recovery Enable protection Test
#>
function Test-SiteRecoveryEnableDR
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath

    $Policy = Get-AzRecoveryServicesAsrPolicy -Name $PolicyName
    $PrimaryProtectionContainer = Get-AzRecoveryServicesAsrFabric -FriendlyName $PrimaryFabricName| Get-AzRecoveryServicesAsrProtectionContainer | where { $_.FriendlyName -eq $PrimaryProtectionContainerName }
    $ProtectionContainerMapping = Get-AzRecoveryServicesAsrProtectionContainerMapping -Name $ProtectionContainerMappingName -ProtectionContainer $PrimaryProtectionContainer

    foreach($EnableVMName in $VmList.Split(','))
    {
        # Get protectable item
        $VM = Get-AzRecoveryServicesAsrProtectableItem -FriendlyName $EnableVMName -ProtectionContainer $PrimaryProtectionContainer  
        # EnableDR
        $Job = New-AzRecoveryServicesAsrReplicationProtectedItem -ProtectableItem $VM -Name $VM.Name -ProtectionContainerMapping $ProtectionContainerMapping -RecoveryAzureStorageAccountId $StorageAccountID -OSDiskName $EnableVMName -OS Windows -RecoveryResourceGroupId $RecoveryResourceGroupId
    }
}

<#
.SYNOPSIS
Site Recovery Update RPI
#>
function Test-UpdateRPI
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath

    $Policy = Get-AzRecoveryServicesAsrPolicy -Name $PolicyName
    $PrimaryProtectionContainer = Get-AzRecoveryServicesAsrFabric -FriendlyName $PrimaryFabricName| Get-AzRecoveryServicesAsrProtectionContainer | where { $_.FriendlyName -eq $PrimaryProtectionContainerName }
    
    foreach($EnableVMName in $VmList.Split(','))
    {
        # Get protectable item
        $v = Get-AzRecoveryServicesAsrReplicationProtectedItem -ProtectionContainer $PrimaryProtectionContainer -FriendlyName $EnableVMName
        $currentJob = Set-AzRecoveryServicesAsrReplicationProtectedItem -ReplicationProtectedItem $v -UpdateNic $v.NicDetailsList[0].NicId -RecoveryNetworkId $AzureNetworkID -RecoveryNicSubnetName $subnet
    }
}

<#
.SYNOPSIS
Site Recovery Network Mapping
#>
function Test-MapNetwork
{
    param([string] $vaultSettingsFilePath)
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath

    # Get the primary container
    $PrimaryFabric = Get-AzRecoveryServicesAsrFabric -FriendlyName $PrimaryFabricName
    $RecoveryFabric = Get-AzRecoveryServicesAsrFabric -FriendlyName $RecoveryFabricName

    # Get primary network
    $PrimaryNetwork = Get-AzRecoveryServicesAsrNetwork -Fabric $PrimaryFabric | where { $_.FriendlyName -eq $PrimaryNetworkFriendlyName}
    $RecoveryNetwork = Get-AzRecoveryServicesAsrNetwork -Fabric $RecoveryFabric | where { $_.FriendlyName -eq $RecoveryNetworkFriendlyName}

    # Create network mapping
    $Job = New-AzRecoveryServicesAsrNetworkMapping -Name $NetworkMappingName -PrimaryNetwork $PrimaryNetwork -RecoveryNetwork $RecoveryNetwork
    WaitForJobCompletion -JobId $Job.Name

    # Get network mapping
    $NetworkMapping = Get-AzRecoveryServicesAsrNetworkMapping -Name $NetworkMappingName -Network $PrimaryNetwork

    }

    function Test-RemoveNetworkPairing
    {
        param([string] $vaultSettingsFilePath)
        Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath

        # Get the primary container
        $PrimaryFabric = Get-AzRecoveryServicesAsrFabric -FriendlyName $PrimaryFabricName
        $RecoveryFabric = Get-AzRecoveryServicesAsrFabric -FriendlyName $RecoveryFabricName

        # Get primary network
        $PrimaryNetwork = Get-AzRecoveryServicesAsrNetwork -Fabric $PrimaryFabric | where { $_.FriendlyName -eq $PrimaryNetworkFriendlyName}
        $RecoveryNetwork = Get-AzRecoveryServicesAsrNetwork -Fabric $RecoveryFabric | where { $_.FriendlyName -eq $RecoveryNetworkFriendlyName}

        # Get network mapping
        $job = Get-AzRecoveryServicesAsrNetworkMapping -Name $NetworkMappingName -Network $PrimaryNetwork |Remove-ASRNetworkMapping
        WaitForJobCompletion -JobId $Job.Name
    }
<#
.SYNOPSIS
Site Recovery Test Failover
#>
function Test-TFO
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath

    # Get the primary container
    $PrimaryProtectionContainer = Get-AzRecoveryServicesAsrFabric -FriendlyName $PrimaryFabricName | Get-AzRecoveryServicesAsrProtectionContainer | where { $_.FriendlyName -eq $PrimaryProtectionContainerName }
    
    $rpi = Get-AzRecoveryServicesAsrReplicationProtectedItem -FriendlyName $VMName -ProtectionContainer $PrimaryProtectionContainer 

	$job = Start-ASRTestFailoverJob -ReplicationProtectedItem $rpi -Direction PrimaryToRecovery -AzureVMNetworkId $AzureNetworkID
    WaitForJobCompletion -JobId $Job.Name

    $job = Start-ASRTestFailoverCleanupJob -ReplicationProtectedItem $rpi
    WaitForJobCompletion -JobId $Job.Name
}

<#
.SYNOPSIS
Site Recovery Planned Failover
#>
function Test-PlannedFailover
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath

    # Get the primary container
    $PrimaryProtectionContainer = Get-AzRecoveryServicesAsrFabric -FriendlyName $PrimaryFabricName | Get-AzRecoveryServicesAsrProtectionContainer | where { $_.FriendlyName -eq $PrimaryProtectionContainerName }

    $rpi = Get-AzRecoveryServicesAsrReplicationProtectedItem -FriendlyName $VMName -ProtectionContainer $PrimaryProtectionContainer 
 
    $job = Start-AzRecoveryServicesAsrPlannedFailoverJob -ReplicationProtectedItem $rpi -Direction PrimaryToRecovery

    WaitForJobCompletion -JobId $Job.Name
}

<#
.SYNOPSIS
Site Recovery Commit and Reprotect
#>
function Test-Reprotect
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath

    # Get the primary container
    $PrimaryProtectionContainer = Get-AzRecoveryServicesAsrFabric -FriendlyName $PrimaryFabricName | Get-AzRecoveryServicesAsrProtectionContainer | where { $_.FriendlyName -eq $PrimaryProtectionContainerName }

    $rpi = Get-AzRecoveryServicesAsrReplicationProtectedItem -FriendlyName $VMName -ProtectionContainer $PrimaryProtectionContainer 
    $currentJob = Update-ASRProtectionDirection -ReplicationProtectedItem $rpi -Direction RecoveryToPrimary
    WaitForJobCompletion -JobId $currentJob.Name 
}

<#
.SYNOPSIS
Site Recovery Commit and Reprotect
#>
function Test-FailbackReprotect
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath

    # Get the primary container
    $PrimaryProtectionContainer = Get-AzRecoveryServicesAsrFabric -FriendlyName $PrimaryFabricName | Get-AzRecoveryServicesAsrProtectionContainer | where { $_.FriendlyName -eq $PrimaryProtectionContainerName }

    $rpi = Get-AzRecoveryServicesAsrReplicationProtectedItem -FriendlyName $VMName -ProtectionContainer $PrimaryProtectionContainer 

    $job =  Start-AzRecoveryServicesAsrPlannedFailoverJob -ReplicationProtectedItem $rpi -Direction RecoveryToPrimary

    WaitForJobCompletion -JobId $Job.Name

    $rpi = Get-AzRecoveryServicesAsrReplicationProtectedItem -FriendlyName $VMName -ProtectionContainer $PrimaryProtectionContainer 

    $job = Start-ASRCommitFailoverJob -ReplicationProtectedItem $rpi 
    WaitForJobCompletion -JobId $Job.Name

    $rpi = Get-AzRecoveryServicesAsrReplicationProtectedItem -FriendlyName $VMName -ProtectionContainer $PrimaryProtectionContainer 
    $currentJob = Update-ASRProtectionDirection -ReplicationProtectedItem $rpi -Direction PrimaryToRecovery

    WaitForJobCompletion -JobId $currentJob.Name 
}

<#
.SYNOPSIS
Site Recovery Commit and Reprotect
#>
function Test-UFOandFailback
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath

    # Get the primary container
    $PrimaryProtectionContainer = Get-AzRecoveryServicesAsrFabric -FriendlyName $PrimaryFabricName | Get-AzRecoveryServicesAsrProtectionContainer | where { $_.FriendlyName -eq $PrimaryProtectionContainerName }

    $rpi = Get-ASRReplicationProtectedItem -FriendlyName $VMName -ProtectionContainer $PrimaryProtectionContainer 

    $job =  Start-AsrUnPlannedFailoverJob -ReplicationProtectedItem $rpi -Direction PrimaryToRecovery
    WaitForJobCompletion -JobId $Job.Name

    $rpi = Get-AsrReplicationProtectedItem -FriendlyName $VMName -ProtectionContainer $PrimaryProtectionContainer 
    $currentJob = Update-ASRProtectionDirection -ReplicationProtectedItem $rpi -Direction RecoveryToPrimary
    WaitForJobCompletion -JobId $currentJob.Name 
    WaitForIRCompletion -VM $rpi 
    #timeout 120

    $rpi = Get-AzRecoveryServicesAsrReplicationProtectedItem -FriendlyName $VMName -ProtectionContainer $PrimaryProtectionContainer 
    $job =  Start-AzRecoveryServicesAsrUnPlannedFailoverJob -ReplicationProtectedItem $rpi -Direction RecoveryToPrimary
    WaitForJobCompletion -JobId $Job.Name
    $rpi = Get-AzRecoveryServicesAsrReplicationProtectedItem -FriendlyName $VMName -ProtectionContainer $PrimaryProtectionContainer 
    $currentJob = Update-ASRProtectionDirection -ReplicationProtectedItem $rpi -Direction PrimaryToRecovery
    WaitForJobCompletion -JobId $currentJob.Name  
}

<#
.SYNOPSIS
Site Recovery remove protection container mapping test
#>
function Test-RemovePCMap
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath

    # Get the primary container
    $PrimaryProtectionContainer = Get-AzRecoveryServicesAsrFabric -FriendlyName $PrimaryFabricName| Get-AzRecoveryServicesAsrProtectionContainer | where { $_.FriendlyName -eq $PrimaryProtectionContainerName }

    # Get protection conatiner mapping
    $ProtectionContainerMapping = Get-AzRecoveryServicesAsrProtectionContainerMapping -Name $ProtectionContainerMappingName -ProtectionContainer $PrimaryProtectionContainer

    # Remove protection conatiner mapping
    $Job = Remove-AzRecoveryServicesAsrProtectionContainerMapping -ProtectionContainerMapping $ProtectionContainerMapping
    #WaitForJobCompletion -JobId $Job.Name
}




<#
.SYNOPSIS
Site Recovery Disable protection Test
#>
function Test-SiteRecoveryDisableDR
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath

    # Get the primary container
    $PrimaryProtectionContainer = Get-AzRecoveryServicesAsrFabric -FriendlyName $PrimaryFabricName | Get-AzRecoveryServicesAsrProtectionContainer | where { $_.FriendlyName -eq $PrimaryProtectionContainerName }

    # Get protected item
    $VM = Get-AzRecoveryServicesAsrReplicationProtectedItem -FriendlyName $VMName -ProtectionContainer $PrimaryProtectionContainer  

    # DisableDR
    $Job = Remove-AzRecoveryServicesAsrReplicationProtectedItem -ReplicationProtectedItem $VM

    WaitForJobCompletion -JobId $Job.Name

    Get-ASRReplicationProtectedItem -ProtectionContainer $PrimaryProtectionContainer  | Remove-AzRecoveryServicesAsrReplicationProtectedItem
    #WaitForJobCompletion -JobId $Job.Name
}

<#
.SYNOPSIS
Site Recovery Create Recovery Plan Test
#>
function Test-SiteRecoveryCreateRecoveryPlan
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath

    # Get the fabric and container
    $PrimaryFabric = Get-AzRecoveryServicesAsrFabric -FriendlyName $PrimaryFabricName
    $RecoveryFabric = Get-AzRecoveryServicesAsrFabric -FriendlyName $RecoveryFabricName
    $PrimaryProtectionContainer = Get-AzRecoveryServicesAsrProtectionContainer -FriendlyName $PrimaryProtectionContainerName -Fabric $PrimaryFabric
    $VM = Get-AzRecoveryServicesAsrReplicationProtectedItem -FriendlyName $VMName -ProtectionContainer $PrimaryProtectionContainer

    $Job = New-AzRecoveryServicesAsrRecoveryPlan -Name $RecoveryPlanName -PrimaryFabric $PrimaryFabric -RecoveryFabric $RecoveryFabric -ReplicationProtectedItem $VM
    #WaitForJobCompletion -JobId $Job.Name
}

<#
.SYNOPSIS
Site Recovery Enumerate Recovery Plan Test
#>
function Test-SiteRecoveryEnumerateRecoveryPlan
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath

    $RP = Get-AzRecoveryServicesAsrRecoveryPlan -Name $RecoveryPlanName
    Assert-NotNull($RP)
    Assert-True { $RP.Count -gt 0 }
}

<#
.SYNOPSIS
Site Recovery Remove Recovery Plan Test
#>
function Test-EditRecoveryPlan
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath

    $RP = Get-AsrRecoveryPlan -Name $RecoveryPlanName
    $RP = Edit-ASRRecoveryPlan -RecoveryPlan $RP -AppendGroup

    $VMNameList = $VMList.split(',')
    $PrimaryFabric = Get-AzRecoveryServicesAsrFabric -FriendlyName $PrimaryFabricName
    $PrimaryProtectionContainer = Get-AzRecoveryServicesAsrProtectionContainer -FriendlyName $PrimaryProtectionContainerName -Fabric $PrimaryFabric
    
    $VMList = Get-ASRReplicationProtectedItem -ProtectionContainer $PrimaryProtectionContainer
    $VM = $VMList | where { $_.FriendlyName -eq $VMNameList[1] }
    #-or  $_.FriendlyName -eq $VMNameList[2]}

    $RP = Edit-ASRRecoveryPlan -RecoveryPlan $RP -Group $RP.Groups[3] -AddProtectedItems $VM
    $RP.Groups

    Write-Host $("Triggered Update RP") -ForegroundColor Green
    $currentJob = Update-ASRRecoveryPlan -RecoveryPlan $RP
    WaitForJobCompletion -JobId $currentJob.Name
    #WaitForJobCompletion -JobId $Job.Name
}

<#
.SYNOPSIS
Site Recovery Remove Recovery Plan Test
#>
function Test-RecoveryPlanJob
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath

    $RP = Get-AsrRecoveryPlan -Name $RecoveryPlanName
    $RecoveryFabric = Get-AzRecoveryServicesAsrFabric -FriendlyName $RecoveryFabricName

    $RecoveryNetwork = Get-AzRecoveryServicesAsrNetwork -Fabric $RecoveryFabric | where { $_.FriendlyName -eq $RecoveryNetworkFriendlyName}

    $currentJob = Start-ASRTestFailoverJob -RecoveryPlan $RP -Direction PrimaryToRecovery -VMNetwork $RecoveryNetwork
    WaitForJobCompletion -JobId $currentJob.Name
    $currentJob = Start-ASRTestFailoverCleanupJob -RecoveryPlan $RP
    WaitForJobCompletion -JobId $currentJob.Name

    $currentJob = Start-ASRTestFailoverJob -RecoveryPlan $RP -Direction PrimaryToRecovery
    WaitForJobCompletion -JobId $currentJob.Name
    $currentJob = Start-ASRTestFailoverCleanupJob -RecoveryPlan $RP
    WaitForJobCompletion -JobId $currentJob.Name

    $currentJob = Start-ASRPlannedFailoverJob -RecoveryPlan $RP -Direction PrimaryToRecovery
    WaitForJobCompletion -JobId $currentJob.Name 

    $currentJob = Start-AsrCommitFailoverJob -RecoveryPlan $RP
    $currentJob
    WaitForJobCompletion -JobId $currentJob.Name

    $currentJob = Update-AsrProtectionDirection -RecoveryPlan $RP -Direction RecoveryToPrimary 
    $currentJob
    WaitForJobCompletion -JobId $currentJob.Name
    
    #timeout 1200

    $currentJob = Start-AsrUnPlannedFailoverJob -RecoveryPlan $RP -Direction RecoveryToPrimary
    $currentJob
    WaitForJobCompletion -JobId $currentJob.Name

    $currentJob = Start-AsrCommitFailoverJob -RecoveryPlan $RP
    $currentJob
    WaitForJobCompletion -JobId $currentJob.Name 
    #WaitForJobCompletion -JobId $Job.Name
}
<#
.SYNOPSIS
Site Recovery Remove Recovery Plan Test
#>
function Test-SiteRecoveryRemoveRecoveryPlan
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath

    $RP = Get-AzRecoveryServicesAsrRecoveryPlan -Name $RecoveryPlanName
    $Job = Remove-AzRecoveryServicesAsrRecoveryPlan -RecoveryPlan $RP
    #WaitForJobCompletion -JobId $Job.Name
}

<#
.SYNOPSIS
Site Recovery Fabric Tests New model
#>
function Test-SiteRecoveryFabricTest
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath

    # Create Fabric
    $Job = New-AzRecoveryServicesAsrFabric -Name $FabricNameToBeCreated -Type HyperVSite
    #WaitForJobCompletion -JobId $Job.Name -JobQueryWaitTimeInSeconds $JobQueryWaitTimeInSeconds
    Assert-NotNull($Job)
    WaitForJobCompletion -JobId $job.name

    # Enumerate Fabrics
    $fabrics =  Get-AzRecoveryServicesAsrFabric 
    Assert-True { $fabrics.Count -gt 0 }
    Assert-NotNull($fabrics)
    foreach($fabric in $fabrics)
    {
        Assert-NotNull($fabrics.Name)
        Assert-NotNull($fabrics.ID)
    }

    # Enumerate specific Fabric
    $fabric =  Get-AzRecoveryServicesAsrFabric -Name $FabricNameToBeCreated
    Assert-NotNull($fabric)
    Assert-NotNull($fabrics.Name)
    Assert-NotNull($fabrics.ID)

    # Remove specific fabric
    $Job = Remove-AzRecoveryServicesAsrFabric -Fabric $fabric
    Assert-NotNull($Job)
    #WaitForJobCompletion -JobId $Job.Name -JobQueryWaitTimeInSeconds $JobQueryWaitTimeInSeconds
    $fabric =  Get-AzRecoveryServicesAsrFabric | Where-Object {$_.Name -eq $FabricNameToBeCreated }
    Assert-Null($fabric)
}


<#
.SYNOPSIS
Site Recovery New model End to End
#>
function Test-SiteRecoveryNewModelE2ETest
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath

    # Enumerate Fabrics
    $Fabrics =  Get-AzRecoveryServicesAsrFabric 
    Assert-True { $fabrics.Count -gt 0 }
    Assert-NotNull($fabrics)
    foreach($fabric in $fabrics)
    {
        Assert-NotNull($fabrics.Name)
        Assert-NotNull($fabrics.ID)
    }
    $PrimaryFabric = $Fabrics | Where-Object { $_.FriendlyName -eq $PrimaryFabricName}
    $RecoveryFabric = $Fabrics | Where-Object { $_.FriendlyName -eq $RecoveryFabricName}

    # Enumerate RSPs
    $rsps = Get-AzRecoveryServicesAsrFabric | Get-AzRecoveryServicesAsrServicesProvider
    Assert-True { $rsps.Count -gt 0 }
    Assert-NotNull($rsps)
    foreach($rsp in $rsps)
    {
        Assert-NotNull($rsp.Name)
    }

    # Create Policy
    $Job = New-AzRecoveryServicesAsrPolicy -Name $PolicyName -ReplicationProvider HyperVReplica2012R2 -ReplicationMethod Online -ReplicationFrequencyInSeconds 30 -RecoveryPoints 1 -ApplicationConsistentSnapshotFrequencyInHours 0 -ReplicationPort 8083 -Authentication Kerberos -ReplicaDeletion Required
    #WaitForJobCompletion -JobId $Job.Name

    $Policy = Get-AzRecoveryServicesAsrPolicy -Name $PolicyName
    Assert-NotNull($Policy)
    Assert-NotNull($Policy.Name)

    # Get conatiners
    $PrimaryProtectionContainer = Get-AzRecoveryServicesAsrFabric | Get-AzRecoveryServicesAsrProtectionContainer | where { $_.FriendlyName -eq $PrimaryProtectionContainerName }
    Assert-NotNull($PrimaryProtectionContainer)
    Assert-NotNull($PrimaryProtectionContainer.Name)
    $RecoveryProtectionContainer = Get-AzRecoveryServicesAsrFabric | Get-AzRecoveryServicesAsrProtectionContainer | where { $_.FriendlyName -eq $RecoveryProtectionContainerName }
    Assert-NotNull($RecoveryProtectionContainer)
    Assert-NotNull($RecoveryProtectionContainer.Name)

    # Create new Conatiner mapping 
    $Job = New-AzRecoveryServicesAsrProtectionContainerMapping -Name $ProtectionContainerMappingName -Policy $Policy -PrimaryProtectionContainer $PrimaryProtectionContainer -RecoveryProtectionContainer $RecoveryProtectionContainer
    #WaitForJobCompletion -JobId $Job.Name

    # Get container mapping
    $ProtectionContainerMapping = Get-AzRecoveryServicesAsrProtectionContainerMapping -Name $ProtectionContainerMappingName -ProtectionContainer $PrimaryProtectionContainer
    Assert-NotNull($ProtectionContainerMapping)
    Assert-NotNull($ProtectionContainerMapping.Name)

    # Get primary network
    $PrimaryNetwork = Get-AzRecoveryServicesAsrNetwork -Fabric $PrimaryFabric | where { $_.FriendlyName -eq $PrimaryNetworkFriendlyName}
    $RecoveryNetwork = Get-AzRecoveryServicesAsrNetwork -Fabric $RecoveryFabric | where { $_.FriendlyName -eq $RecoveryNetworkFriendlyName}

    # Create network mapping
    $Job = New-AzRecoveryServicesAsrNetworkMapping -Name $NetworkMappingName -PrimaryNetwork $PrimaryNetwork -RecoveryNetwork $RecoveryNetwork
    #WaitForJobCompletion -JobId $Job.Name

    # Get network mapping
    $NetworkMapping = Get-AzRecoveryServicesAsrNetworkMapping -Name $NetworkMappingName -Network $PrimaryNetwork

    # Get protectable item
    $protectable = Get-AzRecoveryServicesAsrProtectableItem -ProtectionContainer $PrimaryProtectionContainer -FriendlyName $VMName
    Assert-NotNull($protectable)
    Assert-NotNull($protectable.Name)

    # New replication protected item
    $Job = New-AzRecoveryServicesAsrReplicationProtectedItem -ProtectableItem $protectable -Name $protectable.Name -ProtectionContainerMapping $ProtectionContainerMapping
    #WaitForJobCompletion -JobId $Job.Name
    #WaitForIRCompletion -VM $protectable 
    Assert-NotNull($Job)

    # Get replication protected item
    $protected = Get-AzRecoveryServicesAsrReplicationProtectedItem -ProtectionContainer $PrimaryProtectionContainer -Name $protectable.Name
    Assert-NotNull($protected)
    Assert-NotNull($protected.Name)

    # Remove protected item
    $Job = Remove-AzRecoveryServicesAsrReplicationProtectedItem -ReplicationProtectedItem $protected
    #WaitForJobCompletion -JobId $Job.Name
    $protected = Get-AzRecoveryServicesAsrReplicationProtectedItem -ProtectionContainer $PrimaryProtectionContainer | Where-Object {$_.Name -eq $protectable.Name} 
    Assert-Null($protected)

    # Remove network mapping
    $Job = Remove-AzRecoveryServicesAsrNetworkMapping -NetworkMapping $NetworkMapping
    #WaitForJobCompletion -JobId $Job.Name

    # Remove conatiner mapping
    $Job = Remove-AzRecoveryServicesAsrProtectionContainerMapping -ProtectionContainerMapping $ProtectionContainerMapping
    #WaitForJobCompletion -JobId $Job.Name
    $ProtectionContainerMapping = Get-AzRecoveryServicesAsrProtectionContainerMapping -ProtectionContainer $PrimaryProtectionContainer | Where-Object {$_.Name -eq $ProtectionContainerMappingName}
    Assert-Null($ProtectionContainerMapping)

    # Remove Policy
    $Job = Remove-AzRecoveryServicesAsrPolicy -Policy $Policy
    #WaitForJobCompletion -JobId $Job.Name
    $Policy = Get-AzRecoveryServicesAsrPolicy | Where-Object {$_.Name -eq $PolicyName}
    Assert-Null($Policy)
}

<#
.SYNOPSIS
Site Recovery Update RPI with DiskIdToDiskEncryptionSetMap
#>
function Test-UpdateRPIWithDiskEncryptionSetMap
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath
    $fabric =  Get-AsrFabric -FriendlyName $PrimaryFabricName
    $pc =  Get-ASRProtectionContainer -Fabric $fabric
    $rpi = Get-AsrReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $VMName
    $diskId="/subscriptions/b364ed8d-4279-4bf8-8fd1-56f8fa0ae05c/resourceGroups/testccy/providers/Microsoft.Compute/diskEncryptionSets/testccyrsades2"
    $diskEncryptionSetMap = New-Object "System.Collections.Generic.Dictionary``2[System.String,System.String]"
    $diskEncryptionSetMap.Add($rpi.ProviderSpecificDetails.AzureVMDiskDetails[0].DiskId, $diskId)
    
    $currentJob = Set-AsrReplicationProtectedItem -InputObject $rpi -DiskIdToDiskEncryptionSetMap $diskEncryptionSetMap -UpdateNic $rpi.NicDetailsList[0].NicId -RecoveryNetworkId $AzureNetworkID -RecoveryNicSubnetName $subnet
    WaitForJobCompletion -JobId $currentJob.Name

    $rpi = Get-AsrReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $VMName
    Assert-NotNull($rpi.ProviderSpecificDetails.AzureVMDiskDetails[0].DiskEncryptionSetId)
}

<#
.SYNOPSIS
Site Recovery Create RPI with ProximityPlacementGroup, AvailabilitySet, TargetVmSize, SqlServerLicenseType, UseManagedDisk, ResourceTagging
#>
function Test-CreateRPIWithAdditionalProperties
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath
    $fabric =  Get-AsrFabric -FriendlyName $PrimaryFabricName
    $pc =  Get-ASRProtectionContainer -Fabric $fabric
    $pcm = Get-ASRProtectionContainerMapping -ProtectionContainer $pc
    $policy = Get-AzRecoveryServicesAsrPolicy -Name $PolicyName
    $VM= Get-AsrProtectableItem -ProtectionContainer $pc -FriendlyName $VMName
    $ppg = "/subscriptions/b364ed8d-4279-4bf8-8fd1-56f8fa0ae05c/resourceGroups/prakccyrg/providers/Microsoft.Compute/proximityPlacementGroups/h2appgenable"    
    $avset="/subscriptions/b364ed8d-4279-4bf8-8fd1-56f8fa0ae05c/resourceGroups/prakccyrg/providers/Microsoft.Compute/availabilitySets/h2aavsetenable"
    $size = "Standard_B1s"
    $sqlLicenseType = "AHUB"
    $vmTag = New-Object "System.Collections.Generic.Dictionary``2[System.String,System.String]"
    $vmTag.Add("VmTag1","powershellVm")
    $diskTag = New-Object "System.Collections.Generic.Dictionary``2[System.String,System.String]"
    $diskTag.Add("DiskTag1","powershellDisk")
    $nicTag = New-Object "System.Collections.Generic.Dictionary``2[System.String,System.String]"
    $nicTag.Add("NicTag1","powershellNic")
    $EnableDRjob = New-AsrReplicationProtectedItem -ProtectableItem $VM -Name $VM.Name -ProtectionContainerMapping $pcm -RecoveryAzureStorageAccountId $StorageAccountID -OSDiskName $VMName -OS Windows -RecoveryResourceGroupId $RecoveryResourceGroupId -RecoveryProximityPlacementGroupId $ppg -UseManagedDisk true -RecoveryAvailabilitySetId $avset -Size $size -SqlServerLicenseType $sqlLicenseType -RecoveryVmTag $vmTag -RecoveryNicTag $nicTag -DiskTag $diskTag -RecoveryAzureNetworkId $AzureVmNetworkId
}

<#
.SYNOPSIS
Site Recovery Update RPI with ProximityPlacementGroup, AvailabilitySet, SqlServerLicenseType, ResourceTagging
#>
function Test-UpdateRPIWithAdditionalProperties
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath
    $fabric =  Get-AsrFabric -FriendlyName $PrimaryFabricName
    $pc =  Get-ASRProtectionContainer -Fabric $fabric
    $pcm = Get-ASRProtectionContainerMapping -ProtectionContainer $pc
    $policy = Get-AzRecoveryServicesAsrPolicy -Name $PolicyName
    $rpi = Get-AsrReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $VMName
    $ppg = "/subscriptions/b364ed8d-4279-4bf8-8fd1-56f8fa0ae05c/resourceGroups/prakccyrg/providers/Microsoft.Compute/proximityPlacementGroups/h2appgupdate"
    $avset="/subscriptions/b364ed8d-4279-4bf8-8fd1-56f8fa0ae05c/resourceGroups/prakccyrg/providers/Microsoft.Compute/availabilitySets/h2aavsetupdate"
    $sqlLicenseType = "PAYG"
    $vmTag = New-Object "System.Collections.Generic.Dictionary``2[System.String,System.String]"
    $vmTag.Add("VmTag2","powershellVm")
    $diskTag = New-Object "System.Collections.Generic.Dictionary``2[System.String,System.String]"
    $diskTag.Add("DiskTag2","powershellDisk")
    $nicTag = New-Object "System.Collections.Generic.Dictionary``2[System.String,System.String]"
    $nicTag.Add("NicTag2","powershellNic")

    $currentJob = Set-AsrReplicationProtectedItem -InputObject $rpi -RecoveryProximityPlacementGroupId $ppg -UseManagedDisk true -RecoveryAvailabilitySet $avset -SqlServerLicenseType $sqlLicenseType -RecoveryVmTag $vmTag -RecoveryNicTag $nicTag -DiskTag $diskTag
    WaitForJobCompletion -JobId $currentJob.Name

    $rpi = Get-AsrReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $VMName
    Assert-NotNull($rpi.ProviderSpecificDetails.RecoveryVmTag)
    Assert-NotNull($rpi.ProviderSpecificDetails.DiskTag)
    Assert-NotNull($rpi.ProviderSpecificDetails.RecoveryNicTag)
}

<#
.SYNOPSIS
Site Recovery Create RPI with AvailabilityZone
#>
function Test-CreateRPIWithAvailabilityZone
{
    param([string] $vaultSettingsFilePath)

    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath
    $fabric =  Get-AsrFabric -FriendlyName $PrimaryFabricName
    $pc =  Get-ASRProtectionContainer -Fabric $fabric
    $ProtectionContainerMapping = Get-ASRProtectionContainerMapping -ProtectionContainer $pc
    $policy = Get-AzRecoveryServicesAsrPolicy -Name $PolicyName
    $VM= Get-AsrProtectableItem -ProtectionContainer $pc -FriendlyName $VMName
    $EnableDRjob = New-AzRecoveryServicesAsrReplicationProtectedItem -ProtectableItem $VM -Name $VM.Name -ProtectionContainerMapping $ProtectionContainerMapping -RecoveryAzureStorageAccountId $StorageAccountID -OSDiskName $VMName -OS Windows -RecoveryResourceGroupId $RecoveryResourceGroupId -RecoveryAvailabilityZone $avZone
}

<#
.SYNOPSIS
Site Recovery Update RPI with AvailabilityZone
#>
function Test-UpdateRPIWithAvailabilityZone
{
    param([string] $vaultSettingsFilePath)

    # Import Azure RecoveryServices Vault Settings File
    Import-AzRecoveryServicesAsrVaultSettingsFile -Path $vaultSettingsFilePath
    $fabric =  Get-AsrFabric -FriendlyName $PrimaryFabricName
    $pc =  Get-ASRProtectionContainer -Fabric $fabric
    $rpi = Get-AsrReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $VMName
    $avZoneSet="2"
    $currentJob = Set-AsrReplicationProtectedItem -InputObject $rpi -RecoveryAvailabilityZone $avZoneSet -UpdateNic $rpi.NicDetailsList[0].NicId -RecoveryNetworkId $AzureNetworkID -RecoveryNicSubnetName $subnet
    WaitForJobCompletion -JobId $currentJob.Name
    
    $rpi = Get-AsrReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $VMName
    Assert-NotNull($rpi.ProviderSpecificDetails.RecoveryAvailabilityZone)
}