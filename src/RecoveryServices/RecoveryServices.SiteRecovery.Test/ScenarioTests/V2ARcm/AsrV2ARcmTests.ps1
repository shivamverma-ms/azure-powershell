# encoding: utf-8
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

# Inputs
$vaultRg = "vmwaresrcrg"
$vaultName = "v2aRcm-powershell-test"
$primaryFabricName = "v2aRcm-powershell-testreplicationfabric"
$primaryContainerName = "v2aRcm-powershell-test90e9replicationcontainer"
$policyName = "v2aRcm-powershell-testpolicy"
$failbackPolicyName = "24-hour-replication-failbackpolicy"
$primaryContainerMappingName = "v2aRcm-powershell-testmapping"
$recoveryContainerMappingName = "default-failback-containerpairing"

$vmName = "v2aRcm-powershell-vm"
$recoveryVmName = "v2aRcm-powershell-vm"
$fabricDiscoveryMachineId = "/subscriptions/7c943c1b-5122-4097-90c8-861411bdd574/resourceGroups/vmwaresrcrg/providers/Microsoft.OffAzure/VMwareSites/v2arcm-app7d6dvmwaresite/machines/bcdr-vcenter-fareast-corp-micro-cfcc5a24-a40e-56b9-a6af-e206c9ca4f93_50064b2f-493c-bad3-cf79-6ac1e97d050e"
$recoveryAzureNetworkId = "/subscriptions/7c943c1b-5122-4097-90c8-861411bdd574/resourceGroups/vmwaresrcrgsea/providers/Microsoft.Network/virtualNetworks/agentUpgradeVaultNetwork"
$testNetworkId = "/subscriptions/7c943c1b-5122-4097-90c8-861411bdd574/resourceGroups/vmwaresrcrgsea/providers/Microsoft.Network/virtualNetworks/agentUpgradeVaultNetwork"
$recoveryAzureSubnetName = "default"
$testSubnetName = "default"
$recoveryResourceGroupId = "/subscriptions/7c943c1b-5122-4097-90c8-861411bdd574/resourceGroups/vmwaresrcrgsea"
$logStorageAccountId = "/subscriptions/7c943c1b-5122-4097-90c8-861411bdd574/resourceGroups/vmwaresrcrgsea/providers/Microsoft.Storage/storageAccounts/agentupgradevaultsa"
$recoveryBootDiagnosticsStorageAccountId = "/subscriptions/7c943c1b-5122-4097-90c8-861411bdd574/resourceGroups/vmwaresrcrgsea/providers/Microsoft.Storage/storageAccounts/agentupgradevaultsa"
$runAsAccountsId = "/subscriptions/7c943c1b-5122-4097-90c8-861411bdd574/resourceGroups/vmwaresrcrg/providers/Microsoft.OffAzure/VMwareSites/v2arcm-app7d6dvmwaresite/runasaccounts/3ec0d6eb-2aaa-5ad7-8fb6-1813306ef7d2"
$dataStoreName = "datastore1 (26)"

<#
.SYNOPSIS 
    Site Recovery V2A RCM Fabric Tests
#>
function Test-V2ARcmFabric {
    
    # Set vault context
    $Vault = Get-AzRecoveryServicesVault -ResourceGroupName $vaultRg -Name $vaultName
    Set-ASRVaultContext -Vault $Vault
    
	# Get fabric
    $fabric = Get-AzRecoveryServicesAsrFabric -Name $primaryFabricName
	Assert-NotNull($fabric)
    Assert-NotNull($fabric.FriendlyName)
    Assert-NotNull($fabric.name)
    Assert-NotNull($fabric.ID)
    Assert-NotNull($fabric.FabricSpecificDetails)
    Assert-true { $fabric.name -eq $primaryFabricName }

    $fabricDetails = $fabric.FabricSpecificDetails

    Assert-NotNull($fabricDetails.VmwareSiteId)
    Assert-NotNull($fabricDetails.PhysicalSiteId)
    Assert-NotNull($fabricDetails.ServiceEndpoint)
    Assert-NotNull($fabricDetails.ServiceResourceId)
    Assert-NotNull($fabricDetails.ServiceContainerId)
    Assert-NotNull($fabricDetails.DataPlaneUri)
    Assert-NotNull($fabricDetails.ControlPlaneUri)
}

<#
.SYNOPSIS 
    Site Recovery V2A RCM Container Tests
#>
function Test-V2ARcmContainer {
    
    # Set vault context
    $Vault = Get-AzRecoveryServicesVault -ResourceGroupName $vaultRg -Name $vaultName
    Set-ASRVaultContext -Vault $Vault
    
	# Get fabric
    $Fabric = Get-AzRecoveryServicesAsrFabric -Name $primaryFabricName
	Assert-NotNull($Fabric)
    Assert-true { $Fabric.name -eq $primaryFabricName }

	# Get container
	$ContainerList = Get-ASRProtectionContainer -Fabric $Fabric
    Assert-NotNull($ContainerList)
    $Container = $ContainerList[0]
    Assert-NotNull($Container)
    Assert-NotNull($Container.id)
    Assert-AreEQUAL -actual $Container.FabricType -expected "InMageRcm"

	$Container = Get-AzRecoveryServicesAsrProtectionContainer -Name $primaryContainerName -Fabric $Fabric
	Assert-NotNull($Container)
    Assert-NotNull($Container.id)
	Assert-NotNull($Container.name)
	Assert-true { $Container.name -eq $primaryContainerName }
    Assert-AreEQUAL -actual $Container.FabricType -expected "InMageRcm"
}

<#
.SYNOPSIS
	Site Recovery V2A RCM Policy Tests
#>
function Test-V2ARcmPolicy {

    # Set vault context
    $Vault = Get-AzRecoveryServicesVault -ResourceGroupName $vaultRg -Name $vaultName
    Set-ASRVaultContext -Vault $Vault

	# Create policy
	$Job = New-AzRecoveryServicesAsrPolicy -VMwareRcmToAzure -Name $policyName -RecoveryPointRetentionInHours 40 `
	-ApplicationConsistentSnapshotFrequencyInHours 15 -MultiVmSyncStatus Disable
    WaitForJobCompletion -JobId $Job.Name
     
    # Get policy details
	$Policy = Get-AzRecoveryServicesAsrPolicy -Name $policyName
    Assert-NotNull($Policy)
	Assert-True { $Policy.Count -gt 0 }

    $FailbackPolicy = Get-AzRecoveryServicesAsrPolicy -Name $failbackPolicyName
    Assert-NotNull($FailbackPolicy)
	Assert-True { $FailbackPolicy.Count -gt 0 }
}

<#
.SYNOPSIS
	Site Recovery V2A RCM Container Mapping Test
#>
function Test-V2ARcmContainerMapping {
	
    # Set vault context
    $Vault = Get-AzRecoveryServicesVault -ResourceGroupName $vaultRg -Name $vaultName
    Set-ASRVaultContext -Vault $Vault
    
	# Get fabric
    $Fabric = Get-AzRecoveryServicesAsrFabric -Name $primaryFabricName
	Assert-NotNull($Fabric)
    Assert-true { $Fabric.name -eq $primaryFabricName }

	# Get container
	$Container = Get-AzRecoveryServicesAsrProtectionContainer -Name $primaryContainerName -Fabric $Fabric
	Assert-NotNull($Container)
	Assert-true { $Container.name -eq $primaryContainerName }

	# Get policy
	$Policy = Get-AzRecoveryServicesAsrPolicy -Name $policyName
    Assert-NotNull($Policy)
	Assert-True { $Policy.Count -gt 0 }

    $FailbackPolicy = Get-AzRecoveryServicesAsrPolicy -Name $failbackPolicyName
    Assert-NotNull($FailbackPolicy)
	Assert-True { $FailbackPolicy.Count -gt 0 }

	# Create mapping 
	$Job = New-AzRecoveryServicesAsrProtectionContainerMapping -Name $primaryContainerMappingName -policy $Policy `
	-PrimaryProtectionContainer $Container
    WaitForJobCompletion -JobId $Job.Name

	# Get mapping
	$Mapping = Get-AzRecoveryServicesAsrProtectionContainerMapping -ProtectionContainer $Container `
	-Name $primaryContainerMappingName    
	Assert-NotNull($Mapping)
}

<#
.SYNOPSIS
	Site Recovery V2A RCM Enable Replication Test
#>
function Test-V2ARcmEnableDR {

	# Set vault context
    $Vault = Get-AzRecoveryServicesVault -ResourceGroupName $vaultRg -Name $vaultName
    Set-ASRVaultContext -Vault $Vault
    
	# Get fabric
    $Fabric = Get-AzRecoveryServicesAsrFabric -Name $primaryFabricName
	Assert-NotNull($Fabric)
    Assert-true { $Fabric.name -eq $primaryFabricName }

	# Get container
	$Container = Get-AzRecoveryServicesAsrProtectionContainer -Name $primaryContainerName -Fabric $Fabric
	Assert-NotNull($Container)
	Assert-true { $Container.name -eq $primaryContainerName }
	
	# Get policy
	$Policy = Get-AzRecoveryServicesAsrPolicy -Name $policyName
    Assert-NotNull($Policy)
	Assert-True { $Policy.Count -gt 0 }

	# Get mapping
	$Mapping = Get-AzRecoveryServicesAsrProtectionContainerMapping -ProtectionContainer $Container `
	-Name $primaryContainerMappingName    
	Assert-NotNull($Mapping)

	# Refresh dra
	$dras = Get-ASRServicesProvider -Fabric $fabric 
	$Job = Update-ASRServicesProvider -InputObject $dras[0]
	WaitForJobCompletion -JobId $Job.Name

	# Get updated fabric details
	$Fabric = Get-AzRecoveryServicesAsrFabric -Name $primaryFabricName
	Assert-NotNull($Fabric)
	Assert-NotNull($Fabric.FabricSpecificDetails.ProcessServers)
	Assert-NotNull($Fabric.FabricSpecificDetails.ProcessServers[0].ID)

    # Enable replication
    $Job = New-AzRecoveryServicesAsrReplicationProtectedItem -VMwareRcmToAzure -FabricDiscoveryMachineId $fabricDiscoveryMachineId `
	-Name $vmName -RecoveryVmName $recoveryVmName -TestNetworkId $testNetworkId `
	-TestSubnetName $testSubnetName	-ProtectionContainerMapping $Mapping -RunAsAccountId $runAsAccountsId `
	-LogStorageAccountId $LogStorageAccountId -ProcessServerId $Fabric.FabricSpecificDetails.ProcessServers[0].ID `
	-RecoveryAzureNetworkId $recoveryAzureNetworkId -RecoveryAzureSubnetName $recoveryAzureSubnetName `
	-RecoveryResourceGroupId $recoveryResourceGroupId -RecoveryBootDiagStorageAccountId $recoveryBootDiagnosticsStorageAccountId `
	-DiskType Standard_LRS
}

<#
.SYNOPSIS
	Site Recovery V2A RCM TFO Test
#>
function Test-V2ARcmTestFailover {

	# Set vault context
    $Vault = Get-AzRecoveryServicesVault -ResourceGroupName $vaultRg -Name $vaultName
    Set-ASRVaultContext -Vault $Vault
    
	# Get fabric
    $Fabric = Get-AzRecoveryServicesAsrFabric -Name $primaryFabricName
	Assert-NotNull($Fabric)
    Assert-true { $Fabric.name -eq $primaryFabricName }

	# Get container
	$Container = Get-AzRecoveryServicesAsrProtectionContainer -Name $primaryContainerName -Fabric $Fabric
	Assert-NotNull($Container)
	Assert-true { $Container.name -eq $primaryContainerName }

	# Get protected item
    $RPI = Get-AzRecoveryServicesAsrReplicationProtectedItem -ProtectionContainer $Container -FriendlyName $vmName
    Assert-NotNull($RPI)
	Assert-NotNull($RPI.providerSpecificDetails)
		
    # Test failover
	$job = Start-ASRTestFailoverJob -ReplicationProtectedItem $RPI -Direction PrimaryToRecovery -AzureVMNetworkId $testNetworkID -UseMultiVmSyncPoint False
    WaitForJobCompletion -JobId $Job.Name

	# Test failover clean-up
    $job = Start-ASRTestFailoverCleanupJob -ReplicationProtectedItem $RPI
    WaitForJobCompletion -JobId $Job.Name
}

<#
.SYNOPSIS
	Site Recovery V2A RCM Failover Test
#>
function Test-V2ARcmFailover {

	# Set vault context
    $Vault = Get-AzRecoveryServicesVault -ResourceGroupName $vaultRg -Name $vaultName
    Set-ASRVaultContext -Vault $Vault
    
	# Get fabric
    $Fabric = Get-AzRecoveryServicesAsrFabric -Name $primaryFabricName
	Assert-NotNull($Fabric)
    Assert-true { $Fabric.name -eq $primaryFabricName }

	# Get container
	$Container = Get-AzRecoveryServicesAsrProtectionContainer -Name $primaryContainerName -Fabric $Fabric
	Assert-NotNull($Container)
	Assert-true { $Container.name -eq $primaryContainerName }
	
	# Get protected item
    $RPI = Get-AzRecoveryServicesAsrReplicationProtectedItem -ProtectionContainer $Container -FriendlyName $vmName
    Assert-NotNull($RPI)
	Assert-NotNull($RPI.providerSpecificDetails)
		
    # Failover
    $failoverjob = Start-AzRecoveryServicesAsrUnPlannedFailoverJob -ReplicationProtectedItem $RPI `
	-Direction PrimaryToRecovery -PerformShutdown False
    WaitForJobCompletion -JobId $failoverjob.Name
}

<#
.SYNOPSIS
	Site Recovery V2A RCM Reprotect Test
#>
function Test-V2ARcmReprotect {

	# Set vault context
    $Vault = Get-AzRecoveryServicesVault -ResourceGroupName $vaultRg -Name $vaultName
    Set-ASRVaultContext -Vault $Vault
    
	# Get fabric
    $Fabric = Get-AzRecoveryServicesAsrFabric -Name $primaryFabricName
	Assert-NotNull($Fabric)
    Assert-true { $Fabric.name -eq $primaryFabricName }
	Assert-NotNull($Fabric.FabricSpecificDetails.ProcessServers)
	Assert-NotNull($Fabric.FabricSpecificDetails.ProcessServers[0].ID)
	Assert-NotNull($Fabric.FabricSpecificDetails.ReprotectAgents[0].ID)

	# Get container
	$Container = Get-AzRecoveryServicesAsrProtectionContainer -Name $primaryContainerName -Fabric $Fabric
	Assert-NotNull($Container)
	Assert-true { $Container.name -eq $primaryContainerName }
	
	# Get mapping
	$FailbackMapping = Get-AzRecoveryServicesAsrProtectionContainerMapping -ProtectionContainer $Container `
	-Name $recoveryContainerMappingName    
	Assert-NotNull($FailbackMapping)

	# Get protected item
    $RPI = Get-AzRecoveryServicesAsrReplicationProtectedItem -ProtectionContainer $Container -FriendlyName $vmName
    Assert-NotNull($RPI)
	Assert-NotNull($RPI.providerSpecificDetails)

    # Reprotect
    $Job = Update-AzRecoveryServicesAsrProtectionDirection -AzureToVMwareRcm -ProtectionContainerMapping $FailbackMapping `
	-ReplicationProtectedItem $RPI -Direction RecoveryToPrimary `
	-ReprotectAgentId $Fabric.FabricSpecificDetails.ReprotectAgents[0].ID `
    -LogStorageAccountId $logStorageAccountId `
    -DatastoreName $dataStoreName `
}

<#
.SYNOPSIS
	Site Recovery V2A RCM Failback Test
#>
function Test-V2ARcmFailback {

	# Set vault context
    $Vault = Get-AzRecoveryServicesVault -ResourceGroupName $vaultRg -Name $vaultName
    Set-ASRVaultContext -Vault $Vault
    
	# Get fabric
    $Fabric = Get-AzRecoveryServicesAsrFabric -Name $primaryFabricName
	Assert-NotNull($Fabric)
    Assert-true { $Fabric.name -eq $primaryFabricName }

	# Get container
	$Container = Get-AzRecoveryServicesAsrProtectionContainer -Name $primaryContainerName -Fabric $Fabric
	Assert-NotNull($Container)
	Assert-true { $Container.name -eq $primaryContainerName }
	
	# Get protected item
    $RPI = Get-AzRecoveryServicesAsrReplicationProtectedItem -ProtectionContainer $Container -FriendlyName $vmName
    Assert-NotNull($RPI)
	Assert-NotNull($RPI.providerSpecificDetails)
		
    # Failback
    $failoverjob = Start-AzRecoveryServicesAsrPlannedFailoverJob -ReplicationProtectedItem $RPI `
	-Direction RecoveryToPrimary
    WaitForJobCompletion -JobId $failoverjob.Name
}

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
        [int] $JobQueryWaitTimeInSeconds =$JobQueryWaitTimeInSeconds
        )
        $isJobLeftForProcessing = $true;
        do
        {
            $Job = Get-AzRecoveryServicesAsrJob -Name $JobId
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
                [Microsoft.Rest.ClientRuntime.Azure.TestFramework.TestUtilities]::Wait($JobQueryWaitTimeInSeconds * 1000)
            }
        }While($isJobLeftForProcessing)
}
