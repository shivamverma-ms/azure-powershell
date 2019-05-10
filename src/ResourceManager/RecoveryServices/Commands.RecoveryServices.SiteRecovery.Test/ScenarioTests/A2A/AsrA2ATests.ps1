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

##Default Value ##

<#
.SYNOPSIS
    NewA2ADiskReplicationConfiguration creation test.
#>

function Test-NewA2ADiskReplicationConfiguration
{
    $recoveryStorageAccountId ="/subscriptions/7c943c1b-5122-4097-90c8-861411bdd574/resourceGroups/a2a-rg/providers/Microsoft.Storage/storageAccounts/a2argdisks412"
    $logStorageAccountId = "/subscriptions/7c943c1b-5122-4097-90c8-861411bdd574/resourceGroups/ltrgp1705152333/providers/Microsoft.Storage/storageAccounts/stagingsa2name1705152333"
    $vhdUri = "https://powershelltestdiag414.blob.core.windows.net/vhds/pslinV2-520180112143232.vhd"

    $v = New-AzureRmRecoveryServicesAsrAzureToAzureDiskReplicationConfig -VhdUri  $vhdUri `
        -RecoveryAzureStorageAccountId $recoveryStorageAccountId `
        -LogStorageAccountId   $logStorageAccountId

     Assert-True { $v.vhdUri -eq $vhdUri }
     Assert-True { $v.recoveryAzureStorageAccountId -eq $recoveryStorageAccountId  }
     Assert-True { $v.logStorageAccountId -eq $logStorageAccountId }
}


<#
.SYNOPSIS
    NewA2ADiskReplicationConfiguration creation test.
#>
function Test-NewA2AManagedDiskReplicationConfiguration
{
    $logStorageAccountId = "fdd"
    $DiskId = "diskId"
    $RecoveryResourceGroupId = "3"
    $RecoveryReplicaDiskAccountType = "Premium_LRS"
    $RecoveryTargetDiskAccountType = "Premium_LRS"

    $v = New-AzureRmRecoveryServicesAsrAzureToAzureDiskReplicationConfig -managed -LogStorageAccountId $logStorageAccountId `
         -DiskId "diskId" -RecoveryResourceGroupId  $RecoveryResourceGroupId -RecoveryReplicaDiskAccountType  $RecoveryReplicaDiskAccountType `
         -RecoveryTargetDiskAccountType $RecoveryTargetDiskAccountType

     Assert-True { $v.LogStorageAccountId -eq $LogStorageAccountId }
     Assert-True { $v.DiskId -eq $DiskId  }
     Assert-True { $v.RecoveryResourceGroupId -eq $RecoveryResourceGroupId }
}

<#
.SYNOPSIS 
    Test GetAsrFabric new parametersets
#>
function Test-NewAsrFabric {
    $seed = 35;
        $vaultRgLocation = getVaultRgLocation
        $vaultName = getVaultName
        $vaultLocation = getVaultLocation
        $vaultRg = getVaultRg
        $primaryLocation = getPrimaryLocation
        $recoveryLocation = getRecoveryLocation
        $primaryFabricName = getPrimaryFabric
        $recoveryFabricName = getRecoveryFabric
        
        New-AzureRmResourceGroup -name $vaultRg -location $vaultRgLocation -force
        [Microsoft.Rest.ClientRuntime.Azure.TestFramework.TestUtilities]::Wait(20 * 1000)
    # vault Creation
        New-azureRmRecoveryServicesVault -ResourceGroupName $vaultRg -Name $vaultName -Location $vaultLocation
        [Microsoft.Rest.ClientRuntime.Azure.TestFramework.TestUtilities]::Wait(20 * 1000)
        $Vault = Get-AzureRMRecoveryServicesVault -ResourceGroupName $vaultRg -Name $vaultName
        Set-ASRVaultContext -Vault $Vault
    # fabric Creation    
        ### AzureToAzure New paramset 
        $fabJob=  New-AzureRmRecoveryServicesAsrFabric -Azure -Name $primaryFabricName -Location $primaryLocation
        WaitForJobCompletion -JobId $fabJob.Name
        $fab = Get-AzureRmRecoveryServicesAsrFabric -Name $primaryFabricName
        Assert-true { $fab.name -eq $primaryFabricName }
        Assert-AreEqual $fab.FabricSpecificDetails.Location $primaryLocation
}

<#
.SYNOPSIS 
    Test GetAsrFabric new parametersets
#>
function Test-NewContainer {
    #variables
		$seed = 33;
		$primaryPolicyName = getPrimaryPolicy
		$recoveryPolicyName = getRecoveryPolicy
        
		$primaryContainerMappingName = getPrimaryContainerMapping
		$recoveryContainerMappingName = getRecoveryContainerMapping    
		$primaryContainerName = getPrimaryContainer
		$recoveryContainerName = getRecoveryContainer
		$vaultRgLocation = getVaultRgLocation
		$vaultName = getVaultName
		$vaultLocation = getVaultLocation
		$vaultRg = getVaultRg
		$primaryLocation = getPrimaryLocation
		$recoveryLocation = getRecoveryLocation
		$primaryFabricName = getPrimaryFabric
		$recoveryFabricName = getRecoveryFabric
		$RecoveryReplicaDiskAccountType = "Premium_LRS"
		$RecoveryTargetDiskAccountType = "Premium_LRS"

        New-AzureRmResourceGroup -name $vaultRg -location $vaultRgLocation -force
        [Microsoft.Rest.ClientRuntime.Azure.TestFramework.TestUtilities]::Wait(20 * 1000)
    # vault Creation
        New-azureRmRecoveryServicesVault -ResourceGroupName $vaultRg -Name $vaultName -Location $vaultLocation
        [Microsoft.Rest.ClientRuntime.Azure.TestFramework.TestUtilities]::Wait(20 * 1000)
        $Vault = Get-AzureRMRecoveryServicesVault -ResourceGroupName $vaultRg -Name $vaultName
        Set-ASRVaultContext -Vault $Vault
    # fabric Creation    
        ### AzureToAzure New paramset 
        $fabJob=  New-AzureRmRecoveryServicesAsrFabric -Azure -Name $primaryFabricName -Location $primaryLocation
        WaitForJobCompletion -JobId $fabJob.Name
        $fab = Get-AzureRmRecoveryServicesAsrFabric -Name $primaryFabricName
        Assert-true { $fab.name -eq $primaryFabricName }
        Assert-AreEqual $fab.FabricSpecificDetails.Location $primaryLocation

	#recovery fabric
        $fabJob=  New-AzureRmRecoveryServicesAsrFabric -Azure -Name $recoveryFabricName -Location $recoveryLocation
        WaitForJobCompletion -JobId $fabJob.Name
        $fab = Get-AzureRmRecoveryServicesAsrFabric -Name $recoveryFabricName
        Assert-true { $fab.name -eq $recoveryFabricName }
        Assert-AreEqual $fab.FabricSpecificDetails.Location $recoveryLocation
        $pf = get-asrFabric -Name $primaryFabricName
        $rf = get-asrFabric -Name $recoveryFabricName
        
        ### AzureToAzure (Default)
        $job = New-AzureRmRecoveryServicesAsrProtectionContainer -Name $primaryContainerName -Fabric $pf
        WaitForJobCompletion -JobId $Job.Name
        $pc = Get-asrProtectionContainer -name $primaryContainerName -Fabric $pf
        Assert-NotNull($pc)
        Assert-AreEqual $pc.Name $primaryContainerName
}

<#
.SYNOPSIS 
    Test-NewReplicationProtectedItem new parametersets
#>

function Test-NewReplicationProtectedItem{
	#variables
        $seed = 336;
        $primaryPolicyName = getPrimaryPolicy
        $recoveryPolicyName = getRecoveryPolicy
        
        $primaryContainerMappingName = getPrimaryContainerMapping
        $recoveryContainerMappingName = getRecoveryContainerMapping
        
        $primaryContainerName = getPrimaryContainer
        $recoveryContainerName = getRecoveryContainer
        $vaultRgLocation = getVaultRgLocation
        $vaultName = getVaultName
        $vaultLocation = getVaultLocation
        $vaultRg = getVaultRg
        $primaryLocation = getRecoveryLocation
        $recoveryLocation = getPrimaryLocation
        $primaryFabricName = getPrimaryFabric
        $recoveryFabricName = getRecoveryFabric
        $RecoveryReplicaDiskAccountType = "Premium_LRS"
        $RecoveryTargetDiskAccountType = "Premium_LRS"
		$policyName = getPrimaryPolicy
		$mappingName = getPrimaryContainerMapping
		$primaryNetMapping = getPrimaryNetworkMapping

		$logStg = "/subscriptions/509099b2-9d2c-4636-b43e-bd5cafb6be69/resourcegroups/pstest1/providers/Microsoft.Storage/storageAccounts/cachestgpstest1"
		$v2VmId ="/subscriptions/509099b2-9d2c-4636-b43e-bd5cafb6be69/resourceGroups/pstest1/providers/Microsoft.Compute/virtualMachines/linux-vm1"
		$PrimaryAzureNetworkId ="/subscriptions/509099b2-9d2c-4636-b43e-bd5cafb6be69/resourceGroups/pstest1/providers/Microsoft.Network/virtualNetworks/pstest1-vnet"
		$RecoveryAzureNetworkId ="/subscriptions/509099b2-9d2c-4636-b43e-bd5cafb6be69/resourceGroups/pstest1-asr/providers/Microsoft.Network/virtualNetworks/rec-pstests1"
		$recRg = "/subscriptions/509099b2-9d2c-4636-b43e-bd5cafb6be69/resourceGroups/pstest1-asr"
		$vmName ="linux-vm1"
		$vhdid ="/subscriptions/509099b2-9d2c-4636-b43e-bd5cafb6be69/resourceGroups/pstest1/providers/Microsoft.Compute/disks/linux-vm1_OsDisk_1_3d376317b0d6463bb344da9fab7d56f3"

        New-AzureRmResourceGroup -name $vaultRg -location $vaultRgLocation -force
        [Microsoft.Rest.ClientRuntime.Azure.TestFramework.TestUtilities]::Wait(20 * 1000)
	# vault Creation
        New-azureRmRecoveryServicesVault -ResourceGroupName $vaultRg -Name $vaultName -Location $vaultLocation
        [Microsoft.Rest.ClientRuntime.Azure.TestFramework.TestUtilities]::Wait(20 * 1000)
        $Vault = Get-AzureRMRecoveryServicesVault -ResourceGroupName $vaultRg -Name $vaultName
        Set-ASRVaultContext -Vault $Vault
    # fabric Creation    
        ### AzureToAzure New paramset 
        $fabJob=  New-AzureRmRecoveryServicesAsrFabric -Azure -Name $primaryFabricName -Location $primaryLocation
        WaitForJobCompletion -JobId $fabJob.Name
        $fab = Get-AzureRmRecoveryServicesAsrFabric -Name $primaryFabricName
        Assert-true { $fab.name -eq $primaryFabricName }
        Assert-AreEqual $fab.FabricSpecificDetails.Location $primaryLocation

        $fabJob=  New-AzureRmRecoveryServicesAsrFabric -Azure -Name $recoveryFabricName -Location $recoveryLocation
        WaitForJobCompletion -JobId $fabJob.Name
        $fab = Get-AzureRmRecoveryServicesAsrFabric -Name $recoveryFabricName
        Assert-true { $fab.name -eq $recoveryFabricName }
        Assert-AreEqual $fab.FabricSpecificDetails.Location $recoveryLocation
        $pf = get-asrFabric -Name $primaryFabricName
        $rf = get-asrFabric -Name $recoveryFabricName
        
    ### AzureToAzure (Default)
        $job = New-AzureRmRecoveryServicesAsrProtectionContainer -Name $primaryContainerName -Fabric $pf
        WaitForJobCompletion -JobId $Job.Name
        $pc = Get-asrProtectionContainer -name $primaryContainerName -Fabric $pf
		$job = New-AzureRmRecoveryServicesAsrProtectionContainer -Name $recoveryContainerName -Fabric $rf
        WaitForJobCompletion -JobId $Job.Name
        $rc = Get-asrProtectionContainer -name $recoveryContainerName -Fabric $rf
    #create policy
		$job = New-AzureRmRecoveryServicesAsrPolicy -Name $policyName  -RecoveryPointRetentionInHours 12  -AzureToAzure 
		WaitForJobCompletion -JobId $job.Name
		$policy = Get-AzureRmRecoveryServicesAsrPolicy  -Name $policyName
		$job = New-AzureRmRecoveryServicesAsrProtectionContainerMapping -Name $mappingName -Policy $policy -PrimaryProtectionContainer $pc -RecoveryProtectionContainer $rc
		WaitForJobCompletion -JobId $job.Name
		$mapping = Get-AzureRmRecoveryServicesAsrProtectionContainerMapping -Name $mappingName -ProtectionContainer $pc 
		
	#network mapping
		$job = New-AzureRmRecoveryServicesAsrNetworkMapping -AzureToAzure -Name $primaryNetMapping -PrimaryFabric $pf -PrimaryAzureNetworkId $PrimaryAzureNetworkId -RecoveryFabric $rf -RecoveryAzureNetworkId $RecoveryAzureNetworkId
        WaitForJobCompletion -JobId $job.Name
        
		$disk1=	New-AzureRmRecoveryServicesAsrAzureToAzureDiskReplicationConfig -DiskId $vhdid -LogStorageAccountId $logStg -ManagedDisk  -RecoveryReplicaDiskAccountType $RecoveryReplicaDiskAccountType -RecoveryResourceGroupId $recRg -RecoveryTargetDiskAccountType $RecoveryTargetDiskAccountType
        $enableDRjob = New-AzureRmRecoveryServicesAsrReplicationProtectedItem -AzureToAzure -AzureVmId $v2VmId -Name $vmName  -ProtectionContainerMapping $mapping -RecoveryResourceGroupId  $recrg -AzureToAzureDiskReplicationConfiguration $disk1
        WaitForJobCompletion -JobId $enableDRjob.Name
		WaitForIRCompletion -affectedObjectId $enableDRjob.TargetObjectId
}

<#
.SYNOPSIS 
    Test AddReplicationProtectedItemDisk new parametersets
#>

function Test-AddReplicationProtectedItemDisk{
	#variables
        $seed = 336;
        $vaultName = getVaultName
        $vaultLocation = getVaultLocation
        $vaultRg = getVaultRg
        $primaryLocation = getPrimaryLocation
        $recoveryLocation = getRecoveryLocation
        $RecoveryReplicaDiskAccountType = "Premium_LRS"
        $RecoveryTargetDiskAccountType = "Premium_LRS"

		$v2VmId ="/subscriptions/509099b2-9d2c-4636-b43e-bd5cafb6be69/resourceGroups/pstests/providers/Microsoft.Compute/virtualMachines/Vm1"
		$vmName ="vm1"
		$vmRg = "pstests"
		$diskName = "A2ADisk0"+ $seed
		
	# vault 
        $Vault = Get-AzureRMRecoveryServicesVault -ResourceGroupName $vaultRg -Name $vaultName
        Set-ASRVaultContext -Vault $Vault
    # fabric       
        $pf = Get-ASRFabric | where-object {$_.FabricSpecificDetails.Location -eq $primaryLocation}
		Assert-NotNull($pf)
        $rf = Get-ASRFabric | where-object {$_.FabricSpecificDetails.Location -eq $recoveryLocation}
	    Assert-NotNull($rf)
        
    ### AzureToAzure (Default)
        $pc = Get-asrProtectionContainer -name $primaryContainerName -Fabric $pf
        $rc = Get-asrProtectionContainer -name $recoveryContainerName -Fabric $rf
		Assert-NotNull($pc)
		Assert-NotNull($rc)

	#add diskId
		$pe = Get-AzureRmRecoveryServicesAsrReplicationProtectedItem -ProtectionContainer $pc -Name  $vmName
		Assert-NotNull($pe)

	#create disk and attach
	    $vm = get-azureRmVm -ResourceGroupName $vmRg -Name $vmName
		$newDiskConfig = New-AzurermDiskConfig -Location $vm.Location  -CreateOption Empty -DiskSizeGB 5
		$newDisk = New-AzurermDisk -ResourceGroupName $vm.ResourceGroupName -DiskName $diskName -Disk $newDiskConfig
		$vm = Add-AzureRmVMDataDisk -VM $vm -Name $diskName -CreateOption Attach -ManagedDiskId $newDisk.Id -Lun 5
		Update-azureRmVm -ResourceGroupName $vmRg -VM $vm

	#wait for the add-disk health warning to appear	
        Write-Host $("Waiting for Add-Disk health warning...") -ForegroundColor Yellow
		$HealthQueryWaitTimeInSeconds = 10
        do
        {
            $pe = Get-AzureRmRecoveryServicesAsrReplicationProtectedItem -ProtectionContainer $pc -Name  $vmName
			$healthError = $pe.ReplicationHealthErrors | where-object {$_.ErrorCode -eq 153039}

            if($healthError -eq $null)
            {
                 Write-Host $("Waiting for Add-Disk health warning...") -ForegroundColor Yellow
                Write-Host $("Waiting for: " + $HealthQueryWaitTimeInSeconds.ToString + " Seconds") -ForegroundColor Yellow
                [Microsoft.Rest.ClientRuntime.Azure.TestFramework.TestUtilities]::Wait($HealthQueryWaitTimeInSeconds * 1000)
            }
        }While($healthError -eq $null)

	 #add disks
		$disk2=	New-AzureRmRecoveryServicesAsrAzureToAzureDiskReplicationConfig -DiskId $newDisk.Id -LogStorageAccountId  $pe.ProviderSpecificDetails.A2ADiskDetails[0].PrimaryStagingAzureStorageAccountId -ManagedDisk  -RecoveryReplicaDiskAccountType $RecoveryReplicaDiskAccountType -RecoveryResourceGroupId $pe.ProviderSpecificDetails.A2ADiskDetails[0].RecoveryResourceGroupId -RecoveryTargetDiskAccountType $RecoveryTargetDiskAccountType
        $addDRjob = Add-AzureRmRecoveryServicesAsrReplicationProtectedItemDisk -ReplicationProtectedItem $pe -AzureToAzureDiskReplicationConfiguration $disk2
		WaitForJobCompletion -JobId $addDRjob.Name
		WaitForAddDisksIRCompletion  -affectedObjectId $addDRjob.TargetObjectId

		$pe = Get-AzureRmRecoveryServicesAsrReplicationProtectedItem -ProtectionContainer $pc -Name  $vmName
		Assert-NotNull($pe)
}

<#
.SYNOPSIS 
    Test AddReplicationProtectedItemDisk new parametersets
#>

function Test-RemoveReplicationProtectedItemDisk{
	#variables
        $seed = 336;
        $vaultName = getVaultName
        $vaultLocation = getVaultLocation
        $vaultRg = getVaultRg
        $primaryLocation = getPrimaryLocation
        $recoveryLocation = getRecoveryLocation
        $RecoveryReplicaDiskAccountType = "Premium_LRS"
        $RecoveryTargetDiskAccountType = "Premium_LRS"

		$v2VmId ="/subscriptions/509099b2-9d2c-4636-b43e-bd5cafb6be69/resourceGroups/pstests/providers/Microsoft.Compute/virtualMachines/Vm1"
		$vmName ="vm1"
		$vmRg = "pstests"
		$diskName = "A2ADisk0"+ $seed
		
	# vault 
        $Vault = Get-AzureRMRecoveryServicesVault -ResourceGroupName $vaultRg -Name $vaultName
        Set-ASRVaultContext -Vault $Vault
    # fabric       
        $pf = Get-ASRFabric | where-object {$_.FabricSpecificDetails.Location -eq $primaryLocation}
		Assert-NotNull($pf)
        $rf = Get-ASRFabric | where-object {$_.FabricSpecificDetails.Location -eq $recoveryLocation}
	    Assert-NotNull($rf)
        
    ### AzureToAzure (Default)
        $pc = Get-asrProtectionContainer -name $primaryContainerName -Fabric $pf
        $rc = Get-asrProtectionContainer -name $recoveryContainerName -Fabric $rf
		Assert-NotNull($pc)
		Assert-NotNull($rc)

	#add diskId
		$pe = Get-AzureRmRecoveryServicesAsrReplicationProtectedItem -ProtectionContainer $pc -Name  $vmName
		Assert-NotNull($pe)

	#get disk to deattach
	|   $removeDisk = $pe.ProviderSpecificDetails.A2ADiskDetails | where-object {$_.AllowedDiskLevelOperation.Count -eq 1}
	    Assert-NotNull($removeDisk)

		$vm = get-azureRmVm -ResourceGroupName $vmRg -Name $vmName
		$removeDiskId = $vm.StorageProfile.DataDisks | Where-Object {$_.ManagedDisk.Name -eq $removeDisk.DiskName}

		$removeDRjob = Remove-AzureRmRecoveryServicesAsrReplicationProtectedItemDisk -ReplicationProtectedItem $pe -DiskId $removeDiskId.ManagedDisk.Id
		WaitForJobCompletion -JobId $removeDRjob.Name
		
		$pe = Get-AzureRmRecoveryServicesAsrReplicationProtectedItem -ProtectionContainer $pc -Name  $vmName
		Assert-NotNull($pe)
}

<#
.SYNOPSIS 
    Test AddReplicationProtectedItemDisk new parametersets
#>

function Test-ResolveHealthError{
	#variables
        $seed = 336;
        $vaultName = getVaultName
        $vaultLocation = getVaultLocation
        $vaultRg = getVaultRg
        $primaryLocation = getPrimaryLocation
        $recoveryLocation = getRecoveryLocation
        $RecoveryReplicaDiskAccountType = "Premium_LRS"
        $RecoveryTargetDiskAccountType = "Premium_LRS"

		$v2VmId ="/subscriptions/509099b2-9d2c-4636-b43e-bd5cafb6be69/resourceGroups/pstests/providers/Microsoft.Compute/virtualMachines/Vm1"
		$vmName ="vm1"
		$vmRg = "pstests"
		$diskName = "A2ADisk0"+ $seed
		
	# vault 
        $Vault = Get-AzureRMRecoveryServicesVault -ResourceGroupName $vaultRg -Name $vaultName
        Set-ASRVaultContext -Vault $Vault
    # fabric       
        $pf = Get-ASRFabric | where-object {$_.FabricSpecificDetails.Location -eq $primaryLocation}
		Assert-NotNull($pf)
        $rf = Get-ASRFabric | where-object {$_.FabricSpecificDetails.Location -eq $recoveryLocation}
	    Assert-NotNull($rf)
        
    ### AzureToAzure (Default)
        $pc = Get-asrProtectionContainer -name $primaryContainerName -Fabric $pf
        $rc = Get-asrProtectionContainer -name $recoveryContainerName -Fabric $rf
		Assert-NotNull($pc)
		Assert-NotNull($rc)

	#add diskId
		$pe = Get-AzureRmRecoveryServicesAsrReplicationProtectedItem -ProtectionContainer $pc -Name  $vmName
		Assert-NotNull($pe)

	#create disk and attach
	    $vm = get-azureRmVm -ResourceGroupName $vmRg -Name $vmName
		$newDiskConfig = New-AzurermDiskConfig -Location $vm.Location  -CreateOption Empty -DiskSizeGB 5
		$newDisk = New-AzurermDisk -ResourceGroupName $vm.ResourceGroupName -DiskName $diskName -Disk $newDiskConfig
		$vm = Add-AzureRmVMDataDisk -VM $vm -Name $diskName -CreateOption Attach -ManagedDiskId $newDisk.Id -Lun 5
		Update-azureRmVm -ResourceGroupName $vmRg -VM $vm

	#wait for the add-disk health warning to appear	
        Write-Host $("Waiting for Add-Disk health warning...") -ForegroundColor Yellow
		$HealthQueryWaitTimeInSeconds = 10
        do
        {
            $pe = Get-AzureRmRecoveryServicesAsrReplicationProtectedItem -ProtectionContainer $pc -Name  $vmName
			$healthError = $pe.ReplicationHealthErrors | where-object {$_.ErrorCode -eq 153039}

            if($healthError -eq $null)
            {
                 Write-Host $("Waiting for Add-Disk health warning...") -ForegroundColor Yellow
                Write-Host $("Waiting for: " + $HealthQueryWaitTimeInSeconds.ToString + " Seconds") -ForegroundColor Yellow
                [Microsoft.Rest.ClientRuntime.Azure.TestFramework.TestUtilities]::Wait($HealthQueryWaitTimeInSeconds * 1000)
            }
        }While($healthError -eq $null)

    #resolve health error
		$addDRjob = Remove-AzureRmRecoveryServicesAsrReplicationProtectedItemHealthError -ReplicationProtectedItem $pe -ErrorIds $healthError.ErrorId
		WaitForJobCompletion -JobId $addDRjob.Name

}
