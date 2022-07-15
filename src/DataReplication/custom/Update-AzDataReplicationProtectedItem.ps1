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
function Update-AzDataReplicationProtectedItem {
    [CmdletBinding(DefaultParameterSetName = 'ByProtectedItemId', PositionalBinding = $false)]
    param (
        [Parameter(ParameterSetName = 'ByProtectedItemId', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Path')]
        [System.String]
        # Specifies the Protected Item Id
        ${ProtectedItemId},

        [Parameter(ParameterSetName = 'ByInputObject', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IProtectedItemModel]
        # Specifies Object of type ProtectedItemModel
        ${InputObject},

        [Parameter()]
        [ValidateSet("VMwareToAvs")]
        [ValidateNotNullOrEmpty()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies instance type
        ${InstanceType},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies id of target AVS cloud
        ${TargetAvsCloudId},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies name of target cluster on AVS
        ${TargetAvsClusterName},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the fabric discovery machine id
        ${FabricDiscoveryMachineId},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of log storage account
        ${LogStorageAccountId},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the type of disk to be used
        ${DiskType},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the name of the target VM
        ${TargetVMName},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the name of the target resource group
        ${TargetResourceGroupId},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the target vCenter
        ${TargetVCenterId},
        
        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the target datastore
        ${TargetDatastoreId},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the target network
        ${TargetNetworkId},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the target disk pool subnet
        ${TargetDiskPoolSubnetId},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the test network
        ${TestNetworkId},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the credentials
        ${RunAsAccountId},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the replication appliance
        ${ApplianceId},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.Array]
        # Specifies array of VMwareToAvsDiskInput object
        ${DisksToInclude},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.Int32]
        # Specifies target CPU cores
        ${TargetCPUCores},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.Int32]
        # Specifies target memory
        ${TargetMemoryInMB},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies name of multi VM group
        ${MultiVMGroupName},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Gets or sets the policy name.
        ${PolicyName},
    
        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Gets or sets the replication extension name.
        ${ReplicationExtensionName},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Path')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Runtime.DefaultInfo(Script = '(Get-AzContext).Subscription.Id')]
        [System.String]
        # Specifies the subscription id.
        ${SubscriptionId},

        [Parameter()]
        [Alias('AzureRMContext', 'AzureCredential')]
        [ValidateNotNull()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Azure')]
        [System.Management.Automation.PSObject]
        # The credentials, account, tenant, and subscription used for communication with Azure.
        ${DefaultProfile},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Runtime')]
        [System.Management.Automation.SwitchParameter]
        # Run the command as a job
        ${AsJob},
        
        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Runtime')]
        [System.Management.Automation.SwitchParameter]
        # Run the command asynchronously
        ${NoWait}
    )
    process {
        # All the providers supported should be added here
        $acceptedInstanceTypes = "VMwareToAvs"

        # Boolean variables defined to check whether user has entered the property or not
        $HasTargetAvsCloudId = $PSBoundParameters.ContainsKey('TargetAvsCloudId')
        $HasTargetAvsClusterName = $PSBoundParameters.ContainsKey('TargetAvsClusterName')
        $HasFabricDiscoveryMachineId = $PSBoundParameters.ContainsKey('FabricDiscoveryMachineId')
        $HasLogStorageAccountId = $PSBoundParameters.ContainsKey('LogStorageAccountId')
        $HasDiskType = $PSBoundParameters.ContainsKey('DiskType')
        $HasTargetVMName = $PSBoundParameters.ContainsKey('TargetVMName')
        $HasTargetResourceGroupId = $PSBoundParameters.ContainsKey('TargetResourceGroupId')
        $HasTargetVCenterId = $PSBoundParameters.ContainsKey('TargetVCenterId')
        $HasTargetDatastoreId = $PSBoundParameters.ContainsKey('TargetDatastoreId')
        $HasTargetNetworkId = $PSBoundParameters.ContainsKey('TargetNetworkId')
        $HasTargetDiskPoolSubnetId = $PSBoundParameters.ContainsKey('TargetDiskPoolSubnetId')
        $HasTestNetworkId = $PSBoundParameters.ContainsKey('TestNetworkId')
        $HasRunAsAccountId = $PSBoundParameters.ContainsKey('RunAsAccountId')
        $HasApplianceId = $PSBoundParameters.ContainsKey('ApplianceId')
        $HasDisksToInclude = $PSBoundParameters.ContainsKey('DisksToInclude')
        $HasTargetCPUCores = $PSBoundParameters.ContainsKey('TargetCPUCores')
        $HasMultiVMGroupName = $PSBoundParameters.ContainsKey('MultiVMGroupName')
        $HasTargetMemoryInMB = $PSBoundParameters.ContainsKey('TargetMemoryInMB')
        $HasPolicyName = $PSBoundParameters.ContainsKey('PolicyName')
        $HasReplicationExtensionName = $PSBoundParameters.ContainsKey('ReplicationExtensionName')

        $null = $PSBoundParameters.Remove('InstanceType')
        $null = $PSBoundParameters.Remove('TargetAvsCloudId')
        $null = $PSBoundParameters.Remove('TargetAvsClusterName')
        $null = $PSBoundParameters.Remove('FabricDiscoveryMachineId')
        $null = $PSBoundParameters.Remove('LogStorageAccountId')
        $null = $PSBoundParameters.Remove('DiskType')
        $null = $PSBoundParameters.Remove('TargetVMName')
        $null = $PSBoundParameters.Remove('TargetResourceGroupId')
        $null = $PSBoundParameters.Remove('TargetVCenterId')
        $null = $PSBoundParameters.Remove('TargetDatastoreId')
        $null = $PSBoundParameters.Remove('TargetNetworkId')
        $null = $PSBoundParameters.Remove('TargetDiskPoolSubnetId')
        $null = $PSBoundParameters.Remove('TestNetworkId')
        $null = $PSBoundParameters.Remove('RunAsAccountId')
        $null = $PSBoundParameters.Remove('ApplianceId')
        $null = $PSBoundParameters.Remove('DisksToInclude')
        $null = $PSBoundParameters.Remove('TargetCPUCores')
        $null = $PSBoundParameters.Remove('TargetMemoryInMB')
        $null = $PSBoundParameters.Remove('MultiVMGroupName')
        $null = $PSBoundParameters.Remove('PolicyName')
        $null = $PSBoundParameters.Remove('ReplicationExtensionName')
        
        $parameterSet = $PSCmdlet.ParameterSetName
        
        if ($parameterSet -eq 'ByInputObject') {
            $ProtectedItemId = $InputObject.Id
            $null = $PSBoundParameters.Remove('InputObject')
        }
        $MachineIdArray = $ProtectedItemId.Split("/")
        $ResourceGroupName = $MachineIdArray[4]
        $VaultName = $MachineIdArray[8]
        $Name = $MachineIdArray[10]

        $null = $PSBoundParameters.Remove('ProtectedItemId')
        $null = $PSBoundParameters.Add("Name", $Name)
        $null = $PSBoundParameters.Add("ResourceGroupName", $ResourceGroupName)
        $null = $PSBoundParameters.Add("VaultName", $VaultName)
        # Get the existing properties of the protected item
        $ProtectedItem =  Az.DataReplication.internal\Get-AzDataReplicationProtectedItem @PSBoundParameters
        
        # Check if protected item exists, no current job is going on and correct provider is given 
        if ($ProtectedItem) {
            if($acceptedInstanceTypes.Contains($ProtectedItem.CustomProperty.InstanceType)){
                if($null -eq $ProtectedItem.CurrentJobName){
                    $CustomProperty = [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.VMwareToAvsProtectedItemModelCustomProperties]::new()
                    $CustomProperty.InstanceType = $ProtectedItem.CustomProperty.InstanceType

                    # Adding the properties which are entered by user or else taking from existing protected item
                    $CustomProperty.TargetAvsCloudId = ($HasTargetAvsCloudId) ? $TargetAvsCloudId : $ProtectedItem.CustomProperty.TargetAvsCloudId
                    $CustomProperty.TargetAvsClusterName = ($HasTargetAvsClusterName) ? $TargetAvsClusterName : $ProtectedItem.CustomProperty.TargetAvsClusterName
                    $CustomProperty.FabricDiscoveryMachineId = ($HasFabricDiscoveryMachineId) ? $FabricDiscoveryMachineId : $ProtectedItem.CustomProperty.FabricDiscoveryMachineId
                    $CustomProperty.DisksDefault.logStorageAccountId = ($HasLogStorageAccountId) ? $LogStorageAccountId : $ProtectedItem.CustomProperty.DisksDefault.logStorageAccountId
                    $CustomProperty.DisksDefault.diskType = ($HasDiskType) ? $DiskType : $ProtectedItem.CustomProperty.DisksDefault.diskType
                    $CustomProperty.TargetVMName = ($HasTargetVMName) ? $TargetVMName : $ProtectedItem.CustomProperty.TargetVMName
                    $CustomProperty.TargetResourceGroupId = ($HasTargetResourceGroupId) ? $TargetResourceGroupId : $ProtectedItem.CustomProperty.TargetResourceGroupId
                    $CustomProperty.TargetVCenterId = ($HasTargetVCenterId) ? $TargetVCenterId : $ProtectedItem.CustomProperty.TargetVCenterId
                    $CustomProperty.TargetDatastoreId = ($HasTargetDatastoreId) ? $TargetDatastoreId : $ProtectedItem.CustomProperty.TargetDatastoreId
                    $CustomProperty.TargetNetworkId = ($HasTargetNetworkId) ? $TargetNetworkId : $ProtectedItem.CustomProperty.TargetNetworkId
                    $CustomProperty.TargetDiskPoolSubnetId = ($HasTargetDiskPoolSubnetId) ? $TargetDiskPoolSubnetId : $ProtectedItem.CustomProperty.TargetDiskPoolSubnetId
                    $CustomProperty.TestNetworkId = ($HasTestNetworkId) ? $TestNetworkId : $ProtectedItem.CustomProperty.TestNetworkId
                    $CustomProperty.RunAsAccountId = ($HasRunAsAccountId) ? $RunAsAccountId : $ProtectedItem.CustomProperty.RunAsAccountId
                    $CustomProperty.ApplianceId = ($HasApplianceId) ? $ApplianceId : $ProtectedItem.CustomProperty.ApplianceId
                    $CustomProperty.DisksToInclude = ($HasDisksToInclude) ? $DisksToInclude : $ProtectedItem.CustomProperty.DisksToInclude
                    $CustomProperty.TargetCPUCores = ($HasTargetCPUCores) ? $TargetCPUCores : $ProtectedItem.CustomProperty.TargetCPUCores
                    $CustomProperty.TargetMemoryInMB = ($HasTargetMemoryInMB) ? $TargetMemoryInMB : $ProtectedItem.CustomProperty.TargetMemoryInMB
                    $CustomProperty.MultiVMGroupName = ($HasMultiVMGroupName) ? $MultiVMGroupName : $ProtectedItem.CustomProperty.MultiVMGroupName
                    $PolicyName = ($HasPolicyName) ? $PolicyName : $ProtectedItem.PolicyName
                    $ReplicationExtensionName = ($HasReplicationExtensionName) ? $ReplicationExtensionName : $ProtectedItem.ReplicationExtensionName

                    $null = $PSBoundParameters.Add('CustomProperty', $CustomProperty)
                    $null = $PSBoundParameters.Add('PolicyName', $PolicyName)
                    $null = $PSBoundParameters.Add('ReplicationExtensionName', $ReplicationExtensionName)
                    $null = $PSBoundParameters.Add('NoWait', $true)

                    $output = Az.DataReplication.internal\Update-AzDataReplicationProtectedItem @PSBoundParameters
                    $JobName = $output.Target.Split("/")[14].Split("?")[0]
    
                    # Remove parameters which are not necessary for getting job
                    $null = $PSBoundParameters.Remove('Name')
                    $null = $PSBoundParameters.Remove('CustomProperty')
                    $null = $PSBoundParameters.Remove('PolicyName')
                    $null = $PSBoundParameters.Remove('ReplicationExtensionName')
                    $null = $PSBoundParameters.Remove('NoWait')
    
                    # Add the parameters which are required for getting Job
                    $null = $PSBoundParameters.Add('Name', $JobName)
                    $job = Get-AzDataReplicationJob @PSBoundParameters 
                    return $job
                }
                else{
                    throw "An operation is currently going running on this protected item"
                }
            }
            else{
                throw "The given provider is not supported"
            }
        }
        else{
        throw "This protected item does not exist"
       }
    }
}