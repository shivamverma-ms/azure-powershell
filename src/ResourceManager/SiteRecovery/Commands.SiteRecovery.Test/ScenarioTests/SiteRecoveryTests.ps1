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


<#
.SYNOPSIS
Site Recovery Enumeration Tests
#>
function Test-SiteRecoveryEnumerationTests
{
	param([string] $vaultSettingsFilePath)

	# Import Azure Site Recovery Vault Settings
	Import-AzureRmSiteRecoveryVaultSettingsFile $vaultSettingsFilePath

	# Enumerate Vaults
	$vaults = Get-AzureRmSiteRecoveryVault
	Assert-True { $vaults.Count -gt 0 }
	Assert-NotNull($vaults)
	foreach($vault in $vaults)
	{
		Assert-NotNull($vault.Name)
		Assert-NotNull($vault.ID)
	}

	# Enumerate Servers
	$servers = Get-AzureRmSiteRecoveryServer
	Assert-True { $servers.Count -gt 0 }
	Assert-NotNull($servers)
	foreach($server in $servers)
	{
		Assert-NotNull($server.Name)
		Assert-NotNull($server.ID)
	}

	# Enumerate Protection Containers
	$protectionContainers = Get-AzureRmSiteRecoveryProtectionContainer
	Assert-True { $protectionContainers.Count -gt 0 }
	Assert-NotNull($protectionContainers)
	foreach($protectionContainer in $protectionContainers)
	{
		Assert-NotNull($protectionContainer.Name)
		Assert-NotNull($protectionContainer.ID)
	}
}

<#
.SYNOPSIS
Site Recovery Create profile Test
#>
function Test-SiteRecoveryCreateProfile
{
	param([string] $vaultSettingsFilePath)

	# Import Azure Site Recovery Vault Settings
	Import-AzureRmSiteRecoveryVaultSettingsFile $vaultSettingsFilePath

	# Create profile
	$job = New-AzureRmSiteRecoveryPolicy -Name ppAzure -ReplicationProvider HyperVReplicaAzure -ReplicationFrequencyInSeconds 30 -RecoveryPoints 1 -ApplicationConsistentSnapshotFrequencyInHours 0 -RecoveryAzureStorageAccountId "/subscriptions/aef7cd8f-a06f-407d-b7f0-cc78cfebaab0/resourceGroups/rgn1/providers/Microsoft.Storage/storageAccounts/e2astoragev2"
	# WaitForJobCompletion -JobId $job.Name
}

<#
.SYNOPSIS
Site Recovery Delete profile Test
#>
function Test-SiteRecoveryDeleteProfile
{
	param([string] $vaultSettingsFilePath)

	# Import Azure Site Recovery Vault Settings
	Import-AzureRmSiteRecoveryVaultSettingsFile $vaultSettingsFilePath

	# Get a profile created in previous test (with name pp)
	$profiles = Get-AzureRmSiteRecoveryPolicy -FriendlyName ppAzure
	Assert-True { $profiles.Count -gt 0 }
	Assert-NotNull($profiles)

	# Delete the profile
	$job = Remove-AzureRmSiteRecoveryPolicy -Policy $profiles[0]
	# WaitForJobCompletion -JobId $job.Name
}

<#
.SYNOPSIS
Site Recovery Associate profile Test
#>
function Test-SiteRecoveryAssociateProfile
{
	param([string] $vaultSettingsFilePath)

	# Import Azure Site Recovery Vault Settings
	Import-AzureRmSiteRecoveryVaultSettingsFile $vaultSettingsFilePath

	# Get the primary cloud, recovery cloud, and protection profile
	$pri = Get-AzureRmSiteRecoveryProtectionContainer -FriendlyName Cloud_0_15b7884b_30016OE62978
	$pp = Get-AzureRmSiteRecoveryPolicy -Name ppAzure;

	# Associate the profile
	# $job = Start-AzureRmSiteRecoveryPolicyAssociationJob -Policy $pp -PrimaryProtectionContainer $pri
	# WaitForJobCompletion -JobId $job.Name
}


<#
.SYNOPSIS
Site Recovery Dissociate profile Test
#>
function Test-SiteRecoveryDissociateProfile
{
	param([string] $vaultSettingsFilePath)

	# Import Azure Site Recovery Vault Settings
	Import-AzureRmSiteRecoveryVaultSettingsFile $vaultSettingsFilePath

	# Get the primary cloud, recovery cloud, and protection profile
	$pri = Get-AzureRmSiteRecoveryProtectionContainer -FriendlyName Cloud_0_15b7884b_30016OE62978
	$pp = Get-AzureRmSiteRecoveryPolicy -Name ppAzure;

	# Dissociate the profile
	$job = Start-AzureRmSiteRecoveryPolicyDissociationJob -Policy $pp -PrimaryProtectionContainer $pri
	# WaitForJobCompletion -JobId $job.Name
}

<#
.SYNOPSIS
Site Recovery Enable protection Test
#>
function Test-SiteRecoveryEnableDR
{
	param([string] $vaultSettingsFilePath)

	# Import Azure Site Recovery Vault Settings
	Import-AzureRmSiteRecoveryVaultSettingsFile $vaultSettingsFilePath

	# Get the primary cloud, recovery cloud, and protection profile
	$pri = Get-AzureRmSiteRecoveryProtectionContainer -FriendlyName Cloud_0_15b7884b_30016OE62978
	$pp = Get-AzureRmSiteRecoveryPolicy -Name ppAzure;

	# EnableDR
	$VM = Get-AzureRMSiteRecoveryProtectionEntity -ProtectionContainer $pri -FriendlyName vm1
	$job = Set-AzureRMSiteRecoveryProtectionEntity -ProtectionEntity $VM -Protection Enable -Force -Policy $pp -RecoveryAzureStorageAccountId "/subscriptions/aef7cd8f-a06f-407d-b7f0-cc78cfebaab0/resourceGroups/rgn1/providers/Microsoft.Storage/storageAccounts/e2astoragev2"
	# WaitForJobCompletion -JobId $job.Name
	# WaitForIRCompletion -VM $VM 
}

<#
.SYNOPSIS
Site Recovery Disable protection Test
#>
function Test-SiteRecoveryDisableDR
{
	param([string] $vaultSettingsFilePath)

	# Import Azure Site Recovery Vault Settings
	Import-AzureRmSiteRecoveryVaultSettingsFile $vaultSettingsFilePath

	# Get the primary cloud, recovery cloud, and protection profile
	$pri = Get-AzureRmSiteRecoveryProtectionContainer -FriendlyName Cloud_0_15b7884b_30016OE62978

	# DisableDR
	$VM = Get-AzureRMSiteRecoveryProtectionEntity -ProtectionContainer $pri -FriendlyName vm1
	$job = Set-AzureRMSiteRecoveryProtectionEntity -ProtectionEntity $VM -Protection Disable -Force
}

<#
.SYNOPSIS
Site Recovery Create Recovery Plan Test
#>
function Test-SiteRecoveryCreateRecoveryPlan
{
	param([string] $vaultSettingsFilePath)

	# Import Azure Site Recovery Vault Settings
	Import-AzureRmSiteRecoveryVaultSettingsFile $vaultSettingsFilePath

	# Get the primary cloud, recovery cloud, and protection profile
	$pri = Get-AzureRmSiteRecoveryProtectionContainer -FriendlyName Cloud_0_15b7884b_30016OE62978	
	$PrimaryServer = Get-AzureRMSiteRecoveryServer -FriendlyName sriramvu-test.ntdev.corp.microsoft.com
	$VM = Get-AzureRMSiteRecoveryProtectionEntity -ProtectionContainer $pri -FriendlyName vm1

	$job = New-AzureRmSiteRecoveryRecoveryPlan -Name rp -PrimaryServer $PrimaryServer -Azure -FailoverDeploymentModel ResourceManager -ProtectionEntityList $VM
	# WaitForJobCompletion -JobId $job.Name
}

<#
.SYNOPSIS
Site Recovery Enumerate Recovery Plan Test
#>
function Test-SiteRecoveryEnumerateRecoveryPlan
{
	param([string] $vaultSettingsFilePath)

	# Import Azure Site Recovery Vault Settings
	Import-AzureRmSiteRecoveryVaultSettingsFile $vaultSettingsFilePath

	$RP = Get-AzureRmSiteRecoveryRecoveryPlan -Name rp
	Assert-NotNull($RP)
	Assert-True { $RP.Count -gt 0 }
}

<#
.SYNOPSIS
Site Recovery Remove Recovery Plan Test
#>
function Test-SiteRecoveryRemoveRecoveryPlan
{
	param([string] $vaultSettingsFilePath)

	# Import Azure Site Recovery Vault Settings
	Import-AzureRmSiteRecoveryVaultSettingsFile $vaultSettingsFilePath

	$RP = Get-AzureRmSiteRecoveryRecoveryPlan -Name rp
	$job = Remove-AzureRmSiteRecoveryRecoveryPlan -RecoveryPlan $RP
}

<#
.SYNOPSIS
Wait for job completion
Usage:
	WaitForJobCompletion -JobId $job.ID
	WaitForJobCompletion -JobId $job.ID -NumOfSecondsToWait 10
#>
<#
function WaitForJobCompletion
{
    param([string] $JobId, [Int] $NumOfSecondsToWait = 120)
	$endStateDescription = @('Succeeded','Failed','Cancelled','Suspended')

	$timeElapse = 0;
	$interval = 5;
	do
	{
		Wait-Seconds $interval
		$timeElapse = $timeElapse + $interval
		$job = Get-AzureRmSiteRecoveryJob -Name $JobId;
	} while((-not ($endStateDescription -ccontains $job.State)) -and ($timeElapse -lt $NumOfSecondsToWait))

	Assert-True { $endStateDescription -ccontains $job.State } "Job did not reached desired state within $NumOfSecondsToWait seconds."
}
#>

function WaitForJobCompletion
{ 
	param(
        [string] $JobId,
        [int] $JobQueryWaitTimeInSeconds = 60,
        [string] $Message = "NA"
        )
        $isJobLeftForProcessing = $true;
        do
        {
            $Job = Get-AzureRMSiteRecoveryJob -Name $JobId
            Write-Host $("Job Status:") -ForegroundColor Green
            $Job

            if($Job.State -eq "InProgress" -or $Job.State -eq "NotStarted")
            {
	            $isJobLeftForProcessing = $true
            }
			elseif($Job.State -eq "Failed")
			{
				$exception = New-Object System.Exception ("Job failed...")
				throw $exception
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
		        Start-Sleep -Seconds $JobQueryWaitTimeInSeconds
	        }
        }While($isJobLeftForProcessing)
}

function WaitForIRCompletion
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
            $IRjobs = Get-AzureRMSiteRecoveryJob -TargetObjectId $VM.Name | Sort-Object StartTime -Descending | select -First 5 | Where-Object{$_.JobType -eq "IrCompletion"}
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
		        Start-Sleep -Seconds $JobQueryWaitTimeInSeconds
	        }
        }While($isProcessingLeft)

        Write-Host $("Finalize IR jobs:") -ForegroundColor Green
        $IRjobs
        WaitForJobCompletion -JobId $IRjobs[0].Name -JobQueryWaitTimeInSeconds $JobQueryWaitTimeInSeconds -Message $("Finalize IR in Progress...")
}
<#
.SYNOPSIS
Site Recovery Vault CRUD Tests
#>
function Test-SiteRecoveryVaultCRUDTests
{
	# Create vault
	$vaultCreationResponse = New-AzureRmSiteRecoveryVault -Name v2 -ResourceGroupName rg1 -Location westus
	Assert-NotNull($vaultCreationResponse.Name)
	Assert-NotNull($vaultCreationResponse.ID)
	Assert-NotNull($vaultCreationResponse.Type)

	# Enumerate Vaults
	$vaults = Get-AzureRmSiteRecoveryVault
	Assert-True { $vaults.Count -gt 0 }
	Assert-NotNull($vaults)
	foreach($vault in $vaults)
	{
		Assert-NotNull($vault.Name)
		Assert-NotNull($vault.ID)
		Assert-NotNull($vault.Type)
	}

	# Get the created vault
	$vaultToBeRemoved = Get-AzureRmSiteRecoveryVault -ResourceGroupName rg1 -Name v2
	Assert-NotNull($vaultToBeRemoved.Name)
	Assert-NotNull($vaultToBeRemoved.ID)
	Assert-NotNull($vaultToBeRemoved.Type)

	# Remove Vault
	Remove-AzureRmSiteRecoveryVault -Vault $vaultToBeRemoved
	$vaults = Get-AzureRmSiteRecoveryVault -ResourceGroupName rg1 -Name v2
	Assert-True { $vaults.Count -eq 0 }
}


<#
.SYNOPSIS
Site Recovery Fabric Tests New model
#>
function Test-SiteRecoveryFabricTest
{
	# Enumerate vaults and set Azure Site Recovery Vault Settings
	$vault = Get-AzureRmSiteRecoveryVault -ResourceGroupName ReleaseResourceGroup -Name ReleaseVault
	Assert-NotNull($vault)
	Assert-True { $vault.Count -gt 0 }
	Assert-NotNull($vault.Name)
	Assert-NotNull($vault.ID)
	Set-AzureRmSiteRecoveryVaultSettings -ASRVault $vault

	# Create Fabric
	$job = New-AzureRmSiteRecoveryFabric -Name ReleaseFabric -Type HyperVSite
	Assert-NotNull($job)

	# Enumerate Fabrics
	$fabrics =  Get-AzureRmSiteRecoveryFabric 
	Assert-True { $fabrics.Count -gt 0 }
	Assert-NotNull($fabrics)
	foreach($fabric in $fabrics)
	{
		Assert-NotNull($fabrics.Name)
		Assert-NotNull($fabrics.ID)
	}

	# Enumerate specific Fabric
	$fabric =  Get-AzureRmSiteRecoveryFabric -Name ReleaseFabric
	Assert-NotNull($fabric)
	Assert-NotNull($fabrics.Name)
	Assert-NotNull($fabrics.ID)

	# Remove specific fabric
	$job = Remove-AzureRmSiteRecoveryFabric -Fabric $fabric
	Assert-NotNull($job)
	WaitForJobCompletion -JobId $job.Name
	$fabric =  Get-AzureRmSiteRecoveryFabric | Where-Object {$_.Name -eq "ReleaseFabric"}
	Assert-Null($fabric)
}


<#
.SYNOPSIS
Site Recovery New model End to End
#>
function Test-SiteRecoveryNewModelE2ETest
{
	$JobQueryWaitTimeInSeconds = 30

	# Enumerate vaults and set Azure Site Recovery Vault Settings
	$vault = Get-AzureRmSiteRecoveryVault -ResourceGroupName ReleaseResourceGroup -Name ReleaseVault
	Assert-NotNull($vault)
	Assert-True { $vault.Count -eq 1 }
	Assert-NotNull($vault.Name)
	Assert-NotNull($vault.ID)
	Set-AzureRmSiteRecoveryVaultSettings -ASRVault $vault

	# Enumerate Fabrics
	$fabrics =  Get-AzureRmSiteRecoveryFabric 
	Assert-True { $fabrics.Count -gt 0 }
	Assert-NotNull($fabrics)
	foreach($fabric in $fabrics)
	{
		Assert-NotNull($fabrics.Name)
		Assert-NotNull($fabrics.ID)
	}

	# Enumerate RSPs
	$rsps = Get-AzureRmSiteRecoveryFabric | Get-AzureRmSiteRecoveryServicesProvider
	Assert-True { $rsps.Count -gt 0 }
	Assert-NotNull($rsps)
	foreach($rsp in $rsps)
	{
		Assert-NotNull($rsp.Name)
	}

	$StorageAccountID = "/subscriptions/19b823e2-d1f3-4805-93d7-401c5d8230d5/resourceGroups/releaseresourcegroup/providers/Microsoft.Storage/storageAccounts/releasestorageav" 

	# Create Policy
	$currentJob = New-AzureRmSiteRecoveryPolicy -Name "PP1" -ReplicationProvider HyperVReplicaAzure -ReplicationFrequencyInSeconds 30 -RecoveryPoints 1 -ApplicationConsistentSnapshotFrequencyInHours 0 -Encryption Disable -RecoveryAzureStorageAccountId $StorageAccountID 
    $ProtectionProfile = Get-AzureRMSiteRecoveryPolicy -Name "PP1"
	Assert-NotNull($ProtectionProfile)
	Assert-NotNull($ProtectionProfile.Name)

	# Get conatiners
	$ProtectionContainers = Get-AzureRmSiteRecoveryFabric | Get-AzureRmSiteRecoveryProtectionContainer
	$PrimaryContainer = $ProtectionContainers | where { $_.FriendlyName -eq "hark-123" }
	Assert-NotNull($PrimaryContainer)
	Assert-NotNull($PrimaryContainer.Name)

	# Create new Conatiner mapping 
	$currentJob = New-AzureRmSiteRecoveryProtectionContainerMapping -Name $("hark123" + "PP1") -Policy $ProtectionProfile -PrimaryProtectionContainer $PrimaryContainer

	# Get container mapping
	$ProtectionContainerMapping = Get-AzureRmSiteRecoveryProtectionContainerMapping -Name $("hark123" + "PP1") -ProtectionContainer $PrimaryContainer
	Assert-NotNull($ProtectionContainerMapping)
	Assert-NotNull($ProtectionContainerMapping.Name)

	# Get protectable item
	$protectable = Get-AzureRmSiteRecoveryProtectableItem -ProtectionContainer $PrimaryContainer -FriendlyName "vm3"
	Assert-NotNull($protectable)
	Assert-NotNull($protectable.Name)

	# New replication protected item
	$currentJob = New-AzureRmSiteRecoveryReplicationProtectedItem -ProtectableItem $protectable -Name $protectable.Name -ProtectionContainerMapping $ProtectionContainerMapping -RecoveryAzureStorageAccountId $StorageAccountID
	Assert-NotNull($currentJob)

	# Get replication protected item
	$protected = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $PrimaryContainer -Name $protectable.Name
	Assert-NotNull($protected)
	Assert-NotNull($protected.Name)

	# Remove protected item
	$currentJob = Remove-AzureRmSiteRecoveryReplicationProtectedItem -ReplicationProtectedItem $protected -Force -Confirm:$false
	$protected = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $PrimaryContainer | Where-Object {$_.Name -eq $protectable.Name} 
	Assert-Null($protected)

	# Remove conatiner mapping
	$currentJob = Remove-AzureRmSiteRecoveryProtectionContainerMapping -ProtectionContainerMapping $ProtectionContainerMapping
	$ProtectionContainerMapping = Get-AzureRmSiteRecoveryProtectionContainerMapping -ProtectionContainer $PrimaryContainer | Where-Object {$_.Name -eq $("hark123" + "PP1")}
	Assert-Null($ProtectionContainerMapping)

	# Remove Policy
	$currentJob = Remove-AzureRMSiteRecoveryPolicy -Policy $ProtectionProfile
	$ProtectionProfile = Get-AzureRMSiteRecoveryPolicy | Where-Object {$_.Name -eq "PP1"}
	Assert-Null($ProtectionProfile)
}

<#
.SYNOPSIS
Site Recovery New model End to End for V2A using Single VM
#>
function Test-SiteRecoveryNewModelV2ATestSingleVM
{
	param([string] $vaultSettingsV2AFilePath)

	# Set the V2A Vault Settings Path if not set already
	if ($vaultSettingsV2AFilePath -eq "")
	{
		$vaultSettingsV2AFilePath = "..\bin\Debug\ScenarioTests\vaultSettingsV2A.VaultCredentials"
	}
	
	##############################
	### PREPARE INFRASTRUCTURE ###
	##############################

	# Import V2A Azure Recovery Services Vault Settings
	Import-AzureRmSiteRecoveryVaultSettingsFile $vaultSettingsV2AFilePath

	# Get Fabric
	$fabricName = "V2A-w2K12-660"
	$fabric =  Get-AzureRmSiteRecoveryFabric -FriendlyName $fabricName
	Assert-NotNull($fabric)
	Assert-True { $fabric.Count -eq 1 }
	Assert-NotNull($fabric.Name)
	Assert-NotNull($fabric.ID)

	# Get the RSPs
	$rsps = Get-AzureRmSiteRecoveryServicesProvider -Fabric $fabric
	Assert-NotNull($rsps)
	Assert-True { $rsps.Count -gt 0 }
	foreach($rsp in $rsps)
	{
		Assert-NotNull($rsp.Name)
	}

	# Get Protection Containers
	$pcList = Get-AzureRmSiteRecoveryProtectionContainer -Fabric $fabric
	$pc = $pcList[0]
	Assert-NotNull($pc)
	Assert-NotNull($pc.Name)

	# Create Policies - Failover Policy
	$policyName1 = "V2A-w2K12-660-Policy1"
	$currentJob = New-AzureRmSiteRecoveryPolicy -Name $policyName1 -ReplicationProvider InMageAzureV2 -RecoveryPoints 24 -ApplicationConsistentSnapshotFrequencyInMinutes 60 -RPOWarningThresholdInMinutes 15
	WaitForJobCompletion -JobId $currentJob.Name
	$policy1 = Get-AzureRmSiteRecoveryPolicy -Name $policyName1
	Assert-NotNull($policy1)
	Assert-NotNull($policy1.Name)

	# Create Policies - Failback Policy
	$policyName2 = "V2A-w2K12-660-Policy1-failback"
	$currentJob = New-AzureRmSiteRecoveryPolicy -Name $policyName2 -ReplicationProvider InMage -RecoveryPoints 24 -ApplicationConsistentSnapshotFrequencyInMinutes 60 -RPOWarningThresholdInMinutes 15
	WaitForJobCompletion -JobId $currentJob.Name
	$policy2 = Get-AzureRmSiteRecoveryPolicy -Name $policyName2
	Assert-NotNull($policy2)
	Assert-NotNull($policy2.Name)

	# Create Protection Container Mappings - Forward Mapping
	$pcmName1 = "FailoverPolicy"
	$currentJob = New-AzureRmSiteRecoveryProtectionContainerMapping -Name $pcmName1 -Policy $policy1 -PrimaryProtectionContainer $pc
	WaitForJobCompletion -JobId $currentJob.Name
	$pcm1 = Get-AzureRmSiteRecoveryProtectionContainerMapping -Name $pcmName1 -ProtectionContainer $pc
	Assert-NotNull($pcm1)
	Assert-NotNull($pcm1.Name)
	
	# Create Protection Container Mappings - Reverse Mapping
	$pcmName2 = "FailbackPolicy"
	$currentJob = New-AzureRmSiteRecoveryProtectionContainerMapping -Name $pcmName2 -Policy $policy2 -PrimaryProtectionContainer $pc -RecoveryProtectionContainer $pc
	WaitForJobCompletion -JobId $currentJob.Name
	$pcm2 = Get-AzureRmSiteRecoveryProtectionContainerMapping -Name $pcmName2 -ProtectionContainer $pc
	Assert-NotNull($pcm2)
	Assert-NotNull($pcm2.Name)

	#############################
	### REPLICATE APPLICATION ###
	#############################

	# Discover and Get Protectable Item
	$piName = "V2A-w2K12-653"
	$piIpAddress = "10.150.0.127"
	$piId = "596e63c1-a0d4-11e6-95ed-005056be7a5f"
	$pi = Get-AzureRmSiteRecoveryProtectableItem -ProtectionContainer $pc | Where-Object {$_.FabricSpecificVMDetails.IpAddress -match $piIpAddress -and $_.Name -match $piId }
	Assert-NotNull($pi)
	Assert-NotNull($pi.Name)

	# Create and Get Replication Protected Item
	$storage = "/subscriptions/c183865e-6077-46f2-a3b1-deb0f4f4650a/resourceGroups/Default-Storage-WestUS/providers/Microsoft.ClassicStorage/storageAccounts/hikewalrstoragewestus"
	$network = "/subscriptions/c183865e-6077-46f2-a3b1-deb0f4f4650a/resourceGroups/Default-Networking/providers/Microsoft.ClassicNetwork/virtualNetworks/ExpressRouteVNet-WUS-1"
	$subnet = "TenantSubnet"
	$rpiName = "V2A-w2K12-653"
	
	$currentJob = New-AzureRmSiteRecoveryReplicationProtectedItem -ProtectableItem $pi -Name $pi.Name -ProtectionContainerMapping $pcm1 -ProcessServer $fabric.FabricSpecificDetails.ProcessServers[0] -Account $fabric.FabricSpecificDetails.RunAsAccounts[1] -RecoveryAzureStorageAccountId $storage -RecoveryAzureNetworkId $network -RecoveryAzureSubnetId $subnet
	WaitForJobCompletion -JobId $currentJob.Name
	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	Assert-NotNull($rpi)
	Assert-NotNull($rpi.Name)

	WaitForIRCompletion -VM $rpi
	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName

	while ( $rpi.NicDetailsList.Count -lt 1) {
		Start-Sleep -s 60
		$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	}

	# todo: Resource group update #
	# Modify Replicated Protected Item
	$currentJob = Set-AzureRmSiteRecoveryReplicationProtectedItem -ReplicationProtectedItem $rpi -Name $pi.FriendlyName -Size Basic_A2 -PrimaryNic $rpi.NicDetailsList[0].NicId -RecoveryNetworkId $network -RecoveryNicSubnetName $subnet
	WaitForJobCompletion -JobId $currentJob.Name
	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName

	# Get Recovery Points
	$recPtList = Get-AzureRmSiteRecoveryRecoveryPoint -ReplicationProtectedItem $rpi
	while ( $recPtList.Count -lt 5) {
		Start-Sleep -s 300
		$recPtList = Get-AzureRmSiteRecoveryRecoveryPoint -ReplicationProtectedItem $rpi
	}
	Assert-NotNull($recPtList)

	Assert-True { $recPtList.Count -gt 0 }
	foreach($recPt in $recPtList)
	{
		Assert-NotNull($recPt.Name)
	}

	###########################
	### FAILOVER / FAILBACK ###
	###########################

	# Replication Protection Items - Test Failover
	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	$currentJob = Start-AzureRmSiteRecoveryTestFailoverJob -ReplicationProtectedItem $rpi -AzureVMNetworkId $network -Direction PrimaryToRecovery -RecoveryPoint $recPtList[-2]
	WaitForJobCompletion -JobId $currentJob.Name
	$currentJob = Get-AzureRmSiteRecoveryJob -State Suspended -TargetObjectId $piId
	$resumeJob = Resume-AzureRmSiteRecoveryJob -Job $currentJob
	WaitForJobCompletion -JobId $resumeJob.Name
	$currentJob = Get-AzureRmSiteRecoveryJob -Job $currentJob
	WaitForJobCompletion -JobId $currentJob.Name
	
	Start-Sleep -s 300

	# Replication Protection Items - Unplanned Failover
	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	# Get Recovery Points
	$recPtList = Get-AzureRmSiteRecoveryRecoveryPoint -ReplicationProtectedItem $rpi
	Assert-NotNull($recPtList)

	$currentJob = Start-AzureRmSiteRecoveryUnplannedFailoverJob -ReplicationProtectedItem $rpi -Direction PrimaryToRecovery -PerformSourceSideActions -RecoveryPoint $recPtList[-2]
	WaitForJobCompletion -JobId $currentJob.Name
	
	# Replication Protection Items - Commit
	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	$currentJob = Start-AzureRmSiteRecoveryCommitFailoverJob -ReplicationProtectedItem $rpi
	WaitForJobCompletion -JobId $currentJob.Name
	
	# sleeping for 30mins to report on azure vm to cs.
	Start-Sleep -s 1800

	# Replication Protection Items - Switch Protection
	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	$currentJob = Update-AzureRmSiteRecoveryProtectionDirection -ReplicationProtectedItem $rpi -Direction RecoveryToPrimary -ProcessServer $fabric.FabricSpecificDetails.ProcessServers[0] -MasterTarget $fabric.FabricSpecificDetails.MasterTargetServers[0] -Account $fabric.FabricSpecificDetails.RunAsAccounts[1] -DataStore $fabric.FabricSpecificDetails.MasterTargetServers[0].DataStores[0].SymbolicName -RetentionVolume $fabric.FabricSpecificDetails.MasterTargetServers[0].RetentionVolumes[1].VolumeName -ProtectionContainerMapping $pcm2
	WaitForJobCompletion -JobId $currentJob.Name

	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	Assert-NotNull($rpi)
	Assert-NotNull($rpi.Name)

	while($rpi.ProtectionState -ne "Protected")
	{
		Start-Sleep -s 300
		$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	}

	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	
	# Get Recovery Points
	$recPtList = Get-AzureRmSiteRecoveryRecoveryPoint -ReplicationProtectedItem $rpi
	while ( $recPtList.Count -lt 10) {
		Start-Sleep -s 300
		$recPtList = Get-AzureRmSiteRecoveryRecoveryPoint -ReplicationProtectedItem $rpi
	}
	Assert-NotNull($recPtList)

	Assert-True { $recPtList.Count -gt 0 }
	foreach($recPt in $recPtList)
	{
		Assert-NotNull($recPt.Name)
	}

	Start-Sleep -s 600

	# Replication Protection Items - Failback (Unplanned Failover)
	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	$currentJob = Start-AzureRmSiteRecoveryUnplannedFailoverJob -ReplicationProtectedItem $rpi -Direction PrimaryToRecovery -PerformSourceSideActions -RecoveryTag Latest
	WaitForJobCompletion -JobId $currentJob.Name

	Start-Sleep -s 120
	
	# Replication Protection Items - Commit (after Failback)
	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	$currentJob = Start-AzureRmSiteRecoveryCommitFailoverJob -ReplicationProtectedItem $rpi
	WaitForJobCompletion -JobId $currentJob.Name
	
	# sleeping for 10mins to report the recovered vm to cs.
	Start-Sleep -s 600

	# Replication Protection Items - Reprotect
	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	$currentJob = Update-AzureRmSiteRecoveryProtectionDirection -ReplicationProtectedItem $rpi -Direction RecoveryToPrimary -ProcessServer $fabric.FabricSpecificDetails.ProcessServers[0] -Account $fabric.FabricSpecificDetails.RunAsAccounts[1] -ProtectionContainerMapping $pcm1 -RecoveryAzureStorageAccountId $null
	WaitForJobCompletion -JobId $currentJob.Name

	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	Assert-NotNull($rpi)
	Assert-NotNull($rpi.Name)

	WaitForIRCompletion -VM $rpi
	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	
	###############
	### CLEANUP ###
	###############

	# Remove Replicated Protected Item
	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	$currentJob = Remove-AzureRmSiteRecoveryReplicationProtectedItem -ReplicationProtectedItem $rpi
	WaitForJobCompletion -JobId $currentJob.Name
	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc | Where-Object {$_.FriendlyName -eq $rpiName }
	Assert-Null($rpi)

	# Remove Protection Container Mappings - Reverse Mapping
	$currentJob = Remove-AzureRmSiteRecoveryProtectionContainerMapping -ProtectionContainerMapping $pcm2
	WaitForJobCompletion -JobId $currentJob.Name
	$pcm2 = Get-AzureRmSiteRecoveryProtectionContainerMapping -ProtectionContainer $pc | Where-Object {$_.Name -eq $pcmName2 }
	Assert-Null($pcm2)
	
	# Remove Protection Container Mappings - Forward Mapping
	$currentJob = Remove-AzureRmSiteRecoveryProtectionContainerMapping -ProtectionContainerMapping $pcm1
	WaitForJobCompletion -JobId $currentJob.Name
	$pcm1 = Get-AzureRmSiteRecoveryProtectionContainerMapping -ProtectionContainer $pc | Where-Object {$_.Name -eq $pcmName1 }
	Assert-Null($pcm1)

	# Remove Policies - Failback Policy
	$currentJob = Remove-AzureRmSiteRecoveryPolicy -Policy $policy2
	WaitForJobCompletion -JobId $currentJob.Name
	$policy2 = Get-AzureRmSiteRecoveryPolicy | Where-Object {$_.Name -eq $policyName2 }
	Assert-Null($policy2)

	# Remove Policies - Failover Policy
	$currentJob = Remove-AzureRmSiteRecoveryPolicy -Policy $policy1
	WaitForJobCompletion -JobId $currentJob.Name
	$policy1 = Get-AzureRmSiteRecoveryPolicy | Where-Object {$_.Name -eq $policyName1 }
	Assert-Null($policy1)
}

<#
.SYNOPSIS
Site Recovery New model End to End for V2A using RP
#>
function Test-SiteRecoveryNewModelV2ATestRP
{
	param([string] $vaultSettingsV2AFilePath)

	# Set the V2A Vault Settings Path if not set already
	if ($vaultSettingsV2AFilePath -eq "")
	{
		$vaultSettingsV2AFilePath = "..\bin\Debug\ScenarioTests\vaultSettingsV2A.VaultCredentials"
	}
	
	##############################
	### PREPARE INFRASTRUCTURE ###
	##############################

	# Import V2A Azure Recovery Services Vault Settings
	Import-AzureRmSiteRecoveryVaultSettingsFile $vaultSettingsV2AFilePath

	# Get Fabric
	$fabricName = "V2A-w2K12-660"
	$fabric =  Get-AzureRmSiteRecoveryFabric -FriendlyName $fabricName
	Assert-NotNull($fabric)
	Assert-True { $fabric.Count -eq 1 }
	Assert-NotNull($fabric.Name)
	Assert-NotNull($fabric.ID)

	# Get the RSPs
	$rsps = Get-AzureRmSiteRecoveryServicesProvider -Fabric $fabric
	Assert-NotNull($rsps)
	Assert-True { $rsps.Count -gt 0 }
	foreach($rsp in $rsps)
	{
		Assert-NotNull($rsp.Name)
	}

	# Get Protection Containers
	$pcList = Get-AzureRmSiteRecoveryProtectionContainer -Fabric $fabric
	$pc = $pcList[0]
	Assert-NotNull($pc)
	Assert-NotNull($pc.Name)

	# Create Policies - Failover Policy
	$policyName1 = "V2A-w2K12-660-Policy1"
	$currentJob = New-AzureRmSiteRecoveryPolicy -Name $policyName1 -ReplicationProvider InMageAzureV2 -RecoveryPoints 24 -ApplicationConsistentSnapshotFrequencyInMinutes 60 -RPOWarningThresholdInMinutes 15
	WaitForJobCompletion -JobId $currentJob.Name
	$policy1 = Get-AzureRmSiteRecoveryPolicy -Name $policyName1
	Assert-NotNull($policy1)
	Assert-NotNull($policy1.Name)

	# Create Policies - Failback Policy
	$policyName2 = "V2A-w2K12-660-Policy1-failback"
	$currentJob = New-AzureRmSiteRecoveryPolicy -Name $policyName2 -ReplicationProvider InMage -RecoveryPoints 24 -ApplicationConsistentSnapshotFrequencyInMinutes 60 -RPOWarningThresholdInMinutes 15
	WaitForJobCompletion -JobId $currentJob.Name
	$policy2 = Get-AzureRmSiteRecoveryPolicy -Name $policyName2
	Assert-NotNull($policy2)
	Assert-NotNull($policy2.Name)

	# Create Protection Container Mappings - Forward Mapping
	$pcmName1 = "FailoverPolicy"
	$currentJob = New-AzureRmSiteRecoveryProtectionContainerMapping -Name $pcmName1 -Policy $policy1 -PrimaryProtectionContainer $pc
	WaitForJobCompletion -JobId $currentJob.Name
	$pcm1 = Get-AzureRmSiteRecoveryProtectionContainerMapping -Name $pcmName1 -ProtectionContainer $pc
	Assert-NotNull($pcm1)
	Assert-NotNull($pcm1.Name)
	
	# Create Protection Container Mappings - Reverse Mapping
	$pcmName2 = "FailbackPolicy"
	$currentJob = New-AzureRmSiteRecoveryProtectionContainerMapping -Name $pcmName2 -Policy $policy2 -PrimaryProtectionContainer $pc -RecoveryProtectionContainer $pc
	WaitForJobCompletion -JobId $currentJob.Name
	$pcm2 = Get-AzureRmSiteRecoveryProtectionContainerMapping -Name $pcmName2 -ProtectionContainer $pc
	Assert-NotNull($pcm2)
	Assert-NotNull($pcm2.Name)

	#############################
	### REPLICATE APPLICATION ###
	#############################

	# Discover and Get Protectable Item
	$piName = "V2A-w2K12-653"
	$piIpAddress = "10.150.0.127"
	$piId = "596e63c1-a0d4-11e6-95ed-005056be7a5f"
	$pi = Get-AzureRmSiteRecoveryProtectableItem -ProtectionContainer $pc | Where-Object {$_.FabricSpecificVMDetails.IpAddress -match $piIpAddress -and $_.Name -match $piId }
	Assert-NotNull($pi)
	Assert-NotNull($pi.Name)

	# Create and Get Replication Protected Item
	$storage = "/subscriptions/c183865e-6077-46f2-a3b1-deb0f4f4650a/resourceGroups/Default-Storage-WestUS/providers/Microsoft.ClassicStorage/storageAccounts/hikewalrstoragewestus"
	$network = "/subscriptions/c183865e-6077-46f2-a3b1-deb0f4f4650a/resourceGroups/Default-Networking/providers/Microsoft.ClassicNetwork/virtualNetworks/ExpressRouteVNet-WUS-1"
	$subnet = "TenantSubnet"
	$rpiName = "V2A-w2K12-653"
	
	$currentJob = New-AzureRmSiteRecoveryReplicationProtectedItem -ProtectableItem $pi -Name $pi.Name -ProtectionContainerMapping $pcm1 -ProcessServer $fabric.FabricSpecificDetails.ProcessServers[0] -Account $fabric.FabricSpecificDetails.RunAsAccounts[1] -RecoveryAzureStorageAccountId $storage -RecoveryAzureNetworkId $network -RecoveryAzureSubnetId $subnet
	WaitForJobCompletion -JobId $currentJob.Name
	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	Assert-NotNull($rpi)
	Assert-NotNull($rpi.Name)

	WaitForIRCompletion -VM $rpi
	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName

	while ( $rpi.NicDetailsList.Count -lt 1) {
		Start-Sleep -s 60
		$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	}

	# todo: Resource group update #
	# Modify Replicated Protected Item
	$currentJob = Set-AzureRmSiteRecoveryReplicationProtectedItem -ReplicationProtectedItem $rpi -Name $pi.FriendlyName -Size Basic_A2 -PrimaryNic $rpi.NicDetailsList[0].NicId -RecoveryNetworkId $network -RecoveryNicSubnetName $subnet
	WaitForJobCompletion -JobId $currentJob.Name
	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName

	# Get Recovery Points
	$recPtList = Get-AzureRmSiteRecoveryRecoveryPoint -ReplicationProtectedItem $rpi
	while ( $recPtList.Count -lt 5) {
		Start-Sleep -s 300
		$recPtList = Get-AzureRmSiteRecoveryRecoveryPoint -ReplicationProtectedItem $rpi
	}
	Assert-NotNull($recPtList)

	Assert-True { $recPtList.Count -gt 0 }
	foreach($recPt in $recPtList)
	{
		Assert-NotNull($recPt.Name)
	}

	#############################
	### MANAGE RECOVERY PLANS ###
	#############################

	# Create Recovery Plan
	$rpName = "RP-660" 
	$currentJob = New-AzureRmSiteRecoveryRecoveryPlan -Name $rpName -PrimaryFabric $fabric -Azure -FailoverDeploymentModel Classic -ReplicationProtectedItem $rpi
	WaitForJobCompletion -JobId $currentJob.Name
	$rp = Get-AzureRmSiteRecoveryRecoveryPlan -Name $rpName
	Assert-NotNull($rp)

	###########################
	### FAILOVER / FAILBACK ###
	###########################

	# Recovery Plan - Test Failover
	$rp = Get-AzureRmSiteRecoveryRecoveryPlan -Name $rpName
	$currentJob = Start-AzureRmSiteRecoveryTestFailoverJob -RecoveryPlan $rp -AzureVMNetworkId $network -Direction PrimaryToRecovery -RecoveryTag Latest
	WaitForJobCompletion -JobId $currentJob.Name
	$currentJob = Get-AzureRmSiteRecoveryJob -State Suspended -TargetObjectId $piId
	$resumeJob = Resume-AzureRmSiteRecoveryJob -Job $currentJob
	WaitForJobCompletion -JobId $resumeJob.Name
	$currentJob = Get-AzureRmSiteRecoveryJob -Job $currentJob
	WaitForJobCompletion -JobId $currentJob.Name
	
	Start-Sleep -s 300
	
	# Recovery Plan - Unplanned Failover
	$rp = Get-AzureRmSiteRecoveryRecoveryPlan -Name $rpName
	$currentJob = Start-AzureRmSiteRecoveryUnplannedFailoverJob -RecoveryPlan $rp -Direction PrimaryToRecovery -PerformSourceSideActions -RecoveryTag Latest
	WaitForJobCompletion -JobId $currentJob.Name
	
	# Recovery Plan - Commit
	$rp = Get-AzureRmSiteRecoveryRecoveryPlan -Name $rpName
	$currentJob = Start-AzureRmSiteRecoveryCommitFailoverJob -RecoveryPlan $rp
	WaitForJobCompletion -JobId $currentJob.Name

	# sleeping for 30mins to report on azure vm to cs.
	Start-Sleep -s 1800
	
	# Replication Protection Items - Switch Protection (Recovery Plan Switch Protection is blocked)
	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	$currentJob = Update-AzureRmSiteRecoveryProtectionDirection -ReplicationProtectedItem $rpi -Direction RecoveryToPrimary -ProcessServer $fabric.FabricSpecificDetails.ProcessServers[0] -MasterTarget $fabric.FabricSpecificDetails.MasterTargetServers[0] -Account $fabric.FabricSpecificDetails.RunAsAccounts[1] -DataStore $fabric.FabricSpecificDetails.MasterTargetServers[0].DataStores[0].SymbolicName -RetentionVolume $fabric.FabricSpecificDetails.MasterTargetServers[0].RetentionVolumes[1].VolumeName -ProtectionContainerMapping $pcm2
	WaitForJobCompletion -JobId $currentJob.Name

	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	Assert-NotNull($rpi)
	Assert-NotNull($rpi.Name)

	while($rpi.ProtectionState -ne "Protected")
	{
		Start-Sleep -s 300
		$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	}

	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	
	# Get Recovery Points
	$recPtList = Get-AzureRmSiteRecoveryRecoveryPoint -ReplicationProtectedItem $rpi
	while ( $recPtList.Count -lt 10) {
		Start-Sleep -s 300
		$recPtList = Get-AzureRmSiteRecoveryRecoveryPoint -ReplicationProtectedItem $rpi
	}
	Assert-NotNull($recPtList)

	Assert-True { $recPtList.Count -gt 0 }
	foreach($recPt in $recPtList)
	{
		Assert-NotNull($recPt.Name)
	}

	Start-Sleep -s 600
	
	# Recovery Plan - Failback (Unplanned Failover)
	$rp = Get-AzureRmSiteRecoveryRecoveryPlan -Name $rpName
	$currentJob = Start-AzureRmSiteRecoveryUnplannedFailoverJob -RecoveryPlan $rp -Direction RecoveryToPrimary -PerformSourceSideActions -RecoveryTag Latest
	WaitForJobCompletion -JobId $currentJob.Name

	Start-Sleep -s 120
	
	# Recovery Plan - Commit (after Failback)
	$rp = Get-AzureRmSiteRecoveryRecoveryPlan -Name $rpName
	$currentJob = Start-AzureRmSiteRecoveryCommitFailoverJob -RecoveryPlan $rp

	WaitForJobCompletion -JobId $currentJob.Name
	
	# sleeping for 10mins to report the recovered vm to cs.
	Start-Sleep -s 600
	
	# Replication Protection Items - Reprotect (Recovery Plan Switch Protection is blocked)
	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	$currentJob = Update-AzureRmSiteRecoveryProtectionDirection -ReplicationProtectedItem $rpi -Direction RecoveryToPrimary -ProcessServer $fabric.FabricSpecificDetails.ProcessServers[0] -Account $fabric.FabricSpecificDetails.RunAsAccounts[1] -ProtectionContainerMapping $pcm1 -RecoveryAzureStorageAccountId null
	WaitForJobCompletion -JobId $currentJob.Name

	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	Assert-NotNull($rpi)
	Assert-NotNull($rpi.Name)

	WaitForIRCompletion -VM $rpi
	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	
	###############
	### CLEANUP ###
	###############

	# Delete Recovery Plan
	$currentJob = Remove-AzureRmSiteRecoveryRecoveryPlan -RecoveryPlan $rp
	$rp = Get-AzureRmSiteRecoveryRecoveryPlan | Where-Object { $_.Name -eq $rpName }
	Assert-Null($rp)

	# Remove Replicated Protected Item
	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc -FriendlyName $rpiName
	$currentJob = Remove-AzureRmSiteRecoveryReplicationProtectedItem -ReplicationProtectedItem $rpi
	WaitForJobCompletion -JobId $currentJob.Name
	$rpi = Get-AzureRmSiteRecoveryReplicationProtectedItem -ProtectionContainer $pc | Where-Object {$_.FriendlyName -eq $rpiName }
	Assert-Null($rpi)

	# Remove Protection Container Mappings - Reverse Mapping
	$currentJob = Remove-AzureRmSiteRecoveryProtectionContainerMapping -ProtectionContainerMapping $pcm2
	WaitForJobCompletion -JobId $currentJob.Name
	$pcm2 = Get-AzureRmSiteRecoveryProtectionContainerMapping -ProtectionContainer $pc | Where-Object {$_.Name -eq $pcmName2 }
	Assert-Null($pcm2)
	
	# Remove Protection Container Mappings - Forward Mapping
	$currentJob = Remove-AzureRmSiteRecoveryProtectionContainerMapping -ProtectionContainerMapping $pcm1
	WaitForJobCompletion -JobId $currentJob.Name
	$pcm1 = Get-AzureRmSiteRecoveryProtectionContainerMapping -ProtectionContainer $pc | Where-Object {$_.Name -eq $pcmName1 }
	Assert-Null($pcm1)

	# Remove Policies - Failback Policy
	$currentJob = Remove-AzureRmSiteRecoveryPolicy -Policy $policy2
	WaitForJobCompletion -JobId $currentJob.Name
	$policy2 = Get-AzureRmSiteRecoveryPolicy | Where-Object {$_.Name -eq $policyName2 }
	Assert-Null($policy2)

	# Remove Policies - Failover Policy
	$currentJob = Remove-AzureRmSiteRecoveryPolicy -Policy $policy1
	WaitForJobCompletion -JobId $currentJob.Name
	$policy1 = Get-AzureRmSiteRecoveryPolicy | Where-Object {$_.Name -eq $policyName1 }
	Assert-Null($policy1)
}

<#
.SYNOPSIS
Site Recovery vCenter Tests.
#>
function Test-SiteRecoveryVCenterTest
{
	param([string] $vaultSettingsV2AFilePath)

	# Set the V2A Vault Settings Path if not set already
	if ($vaultSettingsV2AFilePath -eq "")
	{
		$vaultSettingsV2AFilePath = "..\bin\Debug\ScenarioTests\vaultSettingsV2A.VaultCredentials"
	}

	# Import V2A Azure Recovery Services Vault Settings
	Import-AzureRmSiteRecoveryVaultSettingsFile $vaultSettingsV2AFilePath

	# Get Fabric
	$fabricName = "V2A-w2K12-660"
	$fabric =  Get-AzureRmSiteRecoveryFabric -FriendlyName $fabricName
	Assert-NotNull($fabric)
	Assert-True { $fabric.Count -eq 1 }
	Assert-NotNull($fabric.Name)
	Assert-NotNull($fabric.ID)

    # Add vCenter
	$vcenterName = "esx-155"
	$ipOrFqdn = "inmtest155"
	$port = 443
    $job=New-AzureRmSiteRecoveryVCenterServer -Fabric $fabric -Name $vcenterName -Server $ipOrFqdn -Port $port -ProcessServerId $fabric.FabricSpecificDetails.ProcessServers[0].Id -Account $fabric.FabricSpecificDetails.RunAsAccounts[2].AccountId
    Assert-NotNull($job)
	WaitForJobCompletion -JobId $job.Name

	# Enumerate specific vCenter
	$vcenter=Get-AzureRmSiteRecoveryVCenterServer -Fabric $fabric -Name $vcenterName
	Assert-NotNull($vcenter)
	Assert-NotNull($vcenter.Name)
	Assert-NotNull($vcenter.ID)

    #update vCenter credentials
    $job=Update-AzureRmSiteRecoveryVCenterServer -VCenter $vcenter -Account $fabric.FabricSpecificDetails.RunAsAccounts[1].AccountId
    Assert-NotNull($job)
	WaitForJobCompletion -JobId $job.Name

	# Remove specific vCenter
	$job = Remove-AzureRmSiteRecoveryVCenterServer -VCenter $vcenter
	Assert-NotNull($job)
	WaitForJobCompletion -JobId $job.Name
	$vcenter =  Get-AzureRmSiteRecoveryVCenterServer -Fabric $fabric | Where-Object {$_.Name -eq $vcenterName}
	Assert-Null($vcenter)
}

<#
.SYNOPSIS
Site Recovery Events and alerts Tests.
#>
function Test-SiteRecoveryEventsAlerts
{
	param([string] $vaultSettingsV2AFilePath)

	# Set the V2A Vault Settings Path if not set already
	if ($vaultSettingsV2AFilePath -eq "")
	{
		$vaultSettingsV2AFilePath = "..\bin\Debug\ScenarioTests\vaultSettingsV2A.VaultCredentials"
	}

	# Import V2A Azure Recovery Services Vault Settings
	Import-AzureRmSiteRecoveryVaultSettingsFile $vaultSettingsV2AFilePath

	# Get Fabric
	$fabricName = "V2A-w2K12-660"
	$fabric =  Get-AzureRmSiteRecoveryFabric -FriendlyName $fabricName

	# Get notification settings
	$settings=Get-AzureRmSiteRecoveryNotificationSettings
	Assert-NotNull($settings)

    # Set notification settings
    $settings=Set-AzureRmSiteRecoveryNotification -EmailSubscriptionOwners -CustomEmailAddresses @("admin@aad296.ccsctp.net") -LocaleID fr-FR
    Assert-NotNull($settings)

	# Clear the settings
	$settings = Set-AzureRmSiteRecoveryNotification -Disable
	Assert-NotNull($settings)
    
    # DataTime for events
    $startDate=Get-Date -Year 2016 -Month 10 -Day 18
    $endDate=Get-Date -Year 2016 -Month 10 -Day 19

    # Get events
    $events=Get-AzureRmSiteRecoveryEvents -Fabric $fabric -AffectedObjectName $fabricName -StartTime $startDate -EndTime $endDate -Severity Warning -Type VmHealth
}