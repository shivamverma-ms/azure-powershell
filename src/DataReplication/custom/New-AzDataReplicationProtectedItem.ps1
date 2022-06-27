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
function New-AzDataReplicationProtectedItem {
    [CmdletBinding(DefaultParameterSetName = 'ByProperties', PositionalBinding = $false, ConfirmImpact = 'Medium')]
    param (
        
        [Parameter(Mandatory)]    
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Path')]
        [System.String]
        # Specifies the resource group name.
        ${ResourceGroupName},

        [Parameter(Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Path')]
        [System.String]
        # Specifies the protected item name.
        ${Name},

        [Parameter(Mandatory)] 
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Path')]
        [System.String]
        # Specifies the vault name
        ${VaultName},

        [Parameter(ParameterSetName = 'ByInputObject', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IProtectedItemModelCustomProperties]
        # Specifies the custom property for protected item
        ${CustomProperty},

        [Parameter(ParameterSetName = 'ByProperties', Mandatory)]
        [ValidateSet("VMwareToAvs", "VMwareToAvsFailback")]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies instance type
        ${InstanceType},

        [Parameter(ParameterSetName = 'ByProperties')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies id of target AVS cloud
        ${TargetAvsCloudId},

        [Parameter(ParameterSetName = 'ByProperties')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies name of target cluster on AVS
        ${TargetAvsClusterName},

        [Parameter(ParameterSetName = 'ByProperties')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the fabric discovery machine id
        ${FabricDiscoveryMachineId},

        [Parameter(ParameterSetName = 'ByProperties')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of log storage account
        ${LogStorageAccountId},

        [Parameter(ParameterSetName = 'ByProperties')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the type of disk to be used
        ${DiskType},

        [Parameter(ParameterSetName = 'ByProperties')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the name of the target VM
        ${TargetVMName},

        [Parameter(ParameterSetName = 'ByProperties')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the name of the target resource group
        ${TargetResourceGroupId},

        [Parameter(ParameterSetName = 'ByProperties')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the target vCenter
        ${TargetVCenterId},
        
        [Parameter(ParameterSetName = 'ByProperties')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the target datastore
        ${TargetDatastoreId},

        [Parameter(ParameterSetName = 'ByProperties')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the target network
        ${TargetNetworkId},

        [Parameter(ParameterSetName = 'ByProperties')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the target disk pool subnet
        ${TargetDiskPoolSubnetId},

        [Parameter(ParameterSetName = 'ByProperties')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the test network
        ${TestNetworkId},

        [Parameter(ParameterSetName = 'ByProperties')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the credentials
        ${RunAsAccountId},

        [Parameter(ParameterSetName = 'ByProperties')]
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
        ${SubscriptionId}
    )

    process {

        $parameterSet = $PSCmdlet.ParameterSetName
        $acceptedInstanceTypes = "VMwareToAvs", "VMwareToAvsFailback"

        if ($null -ne $CustomProperty -and !$acceptedInstanceTypes.Contains($CustomProperty.InstanceType)) {
            throw "Instance type is not supported. Only VMwareToAvs and VMwareToAvsFailback are applicable"
        }
        
        if ($parameterSet -eq 'ByProperties') {
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
          
        }
 
        $PSBoundParameters.Clear()
        $null = $PSBoundParameters.Add('Name', $Name)
        $null = $PSBoundParameters.Add('CustomProperty', $CustomProperty)
        $null = $PSBoundParameters.Add('ResourceGroupName', $ResourceGroupName)
        $null = $PSBoundParameters.Add('VaultName', $VaultName)
        $null = $PSBoundParameters.Add('PolicyName', $PolicyName)
        $null = $PSBoundParameters.Add('ReplicationExtensionName', $ReplicationExtensionName)
        
 
        Az.DataReplication.internal\New-AzDataReplicationProtectedItem @PSBoundParameters

    }


}

