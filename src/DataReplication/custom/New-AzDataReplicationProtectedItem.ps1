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
    [CmdletBinding(DefaultParameterSetName = 'ByProperties', PositionalBinding = $false)]
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

        [Parameter(ParameterSetName = 'ByCustomPropertyObject', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IProtectedItemModelCustomProperties]
        # Specifies the custom property for protected item
        ${CustomProperty},

        [Parameter(ParameterSetName = 'ByProperties', Mandatory)]
        [ValidateSet("VMwareToAvs")]
        [ValidateNotNullOrEmpty()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies instance type
        ${InstanceType},

        [Parameter(ParameterSetName = 'ByProperties', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies id of target AVS cloud
        ${TargetAvsCloudId},

        [Parameter(ParameterSetName = 'ByProperties', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies name of target cluster on AVS
        ${TargetAvsClusterName},

        [Parameter(ParameterSetName = 'ByProperties', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the fabric discovery machine id
        ${FabricDiscoveryMachineId},

        [Parameter(ParameterSetName = 'ByProperties', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of log storage account
        ${LogStorageAccountId},

        [Parameter(ParameterSetName = 'ByProperties', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the type of disk to be used
        ${DiskType},

        [Parameter(ParameterSetName = 'ByProperties', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the name of the target VM
        ${TargetVMName},

        [Parameter(ParameterSetName = 'ByProperties', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the name of the target resource group
        ${TargetResourceGroupId},

        [Parameter(ParameterSetName = 'ByProperties', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the target vCenter
        ${TargetVCenterId},
        
        [Parameter(ParameterSetName = 'ByProperties', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the target datastore
        ${TargetDatastoreId},

        [Parameter(ParameterSetName = 'ByProperties', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the target network
        ${TargetNetworkId},

        [Parameter(ParameterSetName = 'ByProperties', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the target disk pool subnet
        ${TargetDiskPoolSubnetId},

        [Parameter(ParameterSetName = 'ByProperties', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the test network
        ${TestNetworkId},

        [Parameter(ParameterSetName = 'ByProperties', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the credentials
        ${RunAsAccountId},

        [Parameter(ParameterSetName = 'ByProperties', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the replication appliance
        ${ApplianceId},

        [Parameter(ParameterSetName = 'ByProperties')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.Array]
        # Specifies array of VMwareToAvsDiskInput object
        ${DisksToInclude},

        [Parameter(ParameterSetName = 'ByProperties')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.Int32]
        # Specifies target CPU cores
        ${TargetCPUCores},

        [Parameter(ParameterSetName = 'ByProperties')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.Int32]
        # Specifies target memory
        ${TargetMemoryInMB},

        [Parameter(ParameterSetName = 'ByProperties')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies name of multi VM group
        ${MultiVMGroupName},

        [Parameter(Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Gets or sets the policy name.
        ${PolicyName},
    
        [Parameter(Mandatory)]
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
        ${NoWait},
    
        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Runtime')]
        [System.Management.Automation.SwitchParameter]
        # Returns true when the command succeeds
        ${PassThru}
    )
    process {
        $parameterSet = $PSCmdlet.ParameterSetName
        $acceptedInstanceTypes = "VMwareToAvs"

        if ($null -ne $CustomProperty -and !$acceptedInstanceTypes.Contains($CustomProperty.InstanceType)) {
            throw "Instance type is not supported. Only VMwareToAvs is applicable"
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
            $CustomProperty.DisksToInclude = ($DisksToInclude) ? $DisksToInclude : $null
            $CustomProperty.TargetCPUCore = ($TargetCPUCores) ? $TargetCPUCores : $null
            $CustomProperty.TargetMemoryInMegaByte = ($TargetMemoryInMB) ? $TargetMemoryInMB : $null
            $CustomProperty.MultiVMGroupName = ($MultiVMGroupName) ? $MultiVMGroupName : $null
          
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
        } 
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
        $null = $PSBoundParameters.Add('NoWait', $true)
        
        $output = Az.DataReplication.internal\New-AzDataReplicationProtectedItem @PSBoundParameters
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
}