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
        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Path')]
        [System.String]
        # Specifies the Protected Item Id
        ${ProtectedItemId},

        [Parameter(ParameterSetName = 'ByProtectedItemId')]
        [Parameter(ParameterSetName = 'ByInputObject')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IProtectedItemModelCustomProperties]
        ${CustomProperty},

        [Parameter(ParameterSetName = 'ByInputObject', Mandatory)]
        [Parameter(ParameterSetName = 'ByInputObjectExpanded', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IProtectedItemModel]
        # Specifies Object of type ProtectedItemModel
        ${InputObject},

        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded')]
        [Parameter(ParameterSetName = 'ByInputObjectExpanded')]
        [ValidateSet("VMwareToAvs", "VMwareToAvsFailback")]
        [ValidateNotNullOrEmpty()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies instance type
        ${InstanceType},

        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded')]
        [Parameter(ParameterSetName = 'ByInputObjectExpanded')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies id of target AVS cloud
        ${TargetAvsCloudId},

        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded')]
        [Parameter(ParameterSetName = 'ByInputObjectExpanded')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies name of target cluster on AVS
        ${TargetAvsClusterName},

        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded')]
        [Parameter(ParameterSetName = 'ByInputObjectExpanded')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the fabric discovery machine id
        ${FabricDiscoveryMachineId},

        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded')]
        [Parameter(ParameterSetName = 'ByInputObjectExpanded')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of log storage account
        ${LogStorageAccountId},

        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded')]
        [Parameter(ParameterSetName = 'ByInputObjectExpanded')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the type of disk to be used
        ${DiskType},

        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded')]
        [Parameter(ParameterSetName = 'ByInputObjectExpanded')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the name of the target VM
        ${TargetVMName},

        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded')]
        [Parameter(ParameterSetName = 'ByInputObjectExpanded')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the name of the target resource group
        ${TargetResourceGroupId},

        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded')]
        [Parameter(ParameterSetName = 'ByInputObjectExpanded')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the target vCenter
        ${TargetVCenterId},
        
        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded')]
        [Parameter(ParameterSetName = 'ByInputObjectExpanded')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the target datastore
        ${TargetDatastoreId},

        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded')]
        [Parameter(ParameterSetName = 'ByInputObjectExpanded')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the target network
        ${TargetNetworkId},

        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded')]
        [Parameter(ParameterSetName = 'ByInputObjectExpanded')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the target disk pool subnet
        ${TargetDiskPoolSubnetId},

        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded')]
        [Parameter(ParameterSetName = 'ByInputObjectExpanded')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the test network
        ${TestNetworkId},

        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded')]
        [Parameter(ParameterSetName = 'ByInputObjectExpanded')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the credentials
        ${RunAsAccountId},

        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded')]
        [Parameter(ParameterSetName = 'ByInputObjectExpanded')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the replication appliance
        ${ApplianceId},

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
        $parameterSet = $PSCmdlet.ParameterSetName
        $acceptedInstanceTypes = "VMwareToAvs", "VMwareToAvsFailback"
        if ($null -ne $CustomProperty -and !$acceptedInstanceTypes.Contains($CustomProperty.InstanceType)) {
            throw "Instance type is not supported. Only VMwareToAvs and VMwareToAvsFailback are applicable"
        }
        if ($parameterSet -eq 'ByProtectedItemIdExpanded' -or $parameterSet -eq 'ByInputObjectExpanded') {
            $CustomProperty = [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.VMwareToAvsProtectedItemModelCustomProperties]::new()
            $CustomProperty.InstanceType = $InstanceType
            $CustomProperty.TargetAvsCloudId = $TargetAvsCloudId
            $CustomProperty.TargetAvsClusterName = $TargetAvsClusterName
            $CustomProperty.FabricDiscoveryMachineId = $FabricDiscoveryMachineId
            $CustomProperty.DisksDefault.logStorageAccountId = $LogStorageAccountId
            $CustomProperty.DisksDefault.diskType = $DiskType
            $CustomProperty.TargetVMName = $TargetVMName
            $CustomProperty.TargetResourceGroupId = $TargetResourceGroupId
            $CustomProperty.TargetVCenterId = $TargetVCenterId
            $CustomProperty.TargetDatastoreId = $TargetDatastoreId
            $CustomProperty.TargetNetworkId = $TargetNetworkId
            $CustomProperty.TargetDiskPoolSubnetId = $TargetDiskPoolSubnetId
            $CustomProperty.TestNetworkId = $TestNetworkId
            $CustomProperty.RunAsAccountId = $RunAsAccountId
            $CustomProperty.ApplianceId = $ApplianceId

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
        }
        if ($null -ne $InputObject) {
            $ProtectedItemId = $InputObject.Id
        }
        $MachineIdArray = $ProtectedItemId.Split("/")
        $ResourceGroupName = $MachineIdArray[4]
        $VaultName = $MachineIdArray[8]
        $Name = $MachineIdArray[10]

        $null = $PSBoundParameters.Remove('ProtectedItemId')
        $null = $PSBoundParameters.Remove('InputObject')
        $null = $PSBoundParameters.Remove('Name')
        $null = $PSBoundParameters.Remove('CustomProperty')
        $null = $PSBoundParameters.Remove('ResourceGroupName')
        $null = $PSBoundParameters.Remove('VaultName')
        $null = $PSBoundParameters.Remove('PolicyName')
        $null = $PSBoundParameters.Remove('ReplicationExtensionName')

        $null = $PSBoundParameters.Add('Name', $Name)
        $null = $PSBoundParameters.Add('CustomProperty', $CustomProperty)
        $null = $PSBoundParameters.Add('ResourceGroupName', $ResourceGroupName)
        $null = $PSBoundParameters.Add('VaultName', $VaultName)
        $null = $PSBoundParameters.Add('PolicyName', $PolicyName)
        $null = $PSBoundParameters.Add('ReplicationExtensionName', $ReplicationExtensionName)

        return Az.DataReplication.internal\Update-AzDataReplicationProtectedItem @PSBoundParameters
    }
}