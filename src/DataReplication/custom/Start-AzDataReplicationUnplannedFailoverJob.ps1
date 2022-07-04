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
function Start-AzDataReplicationUnplannedFailoverJob {
    [CmdletBinding(DefaultParameterSetName = 'ByProtectedItemId', PositionalBinding = $false, ConfirmImpact = 'Medium')]
    param (
        [Parameter(ParameterSetName = 'ByProtectedItemId', Mandatory)]
        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Path')]
        [System.String]
        # Specifies the Protected Item Id
        ${ProtectedItemId},

        [Parameter(ParameterSetName = 'ByInputObject', Mandatory)]
        [Parameter(ParameterSetName = 'ByInputObjectExpanded', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IProtectedItemModel]
        # Specifies Object of type ProtectedItemModel
        ${InputObject},

        [Parameter(ParameterSetName = 'ByInputObject', Mandatory)]
        [Parameter(ParameterSetName = 'ByProtectedItemId', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IUnplannedFailoverModelCustomProperties]
        ${CustomProperty},

        [Parameter(ParameterSetName = 'ByInputObjectExpanded', Mandatory)]
        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded', Mandatory)]
        [ValidateSet("VMwareToAvs")]
        [ValidateNotNullOrEmpty()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies instance type
        ${InstanceType},

        [Parameter(ParameterSetName = 'ByInputObjectExpanded')]
        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.Boolean]
        # Specifies whether to shut down the VM after failover
        ${PerformShutdown},

        [Parameter(ParameterSetName = 'ByInputObjectExpanded')]
        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies name of recovery point
        ${RecoveryPointName},

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
 
        if ($null -ne $InputObject) {
            $ProtectedItemId = $InputObject.Id
        }

        $MachineIdArray = $ProtectedItemId.Split("/")
        $ResourceGroupName = $MachineIdArray[4]
        $VaultName = $MachineIdArray[8]
        $ProtectedItemName = $MachineIdArray[10]

        if ($parameterSet -eq "ByProtectedItemIdExpanded" -or $parameterSet -eq "ByInputObjectExpanded") {
            $CustomProperty = [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.VMwareToAvsUnplannedFailoverModelCustomProperties]::new()
            $CustomProperty.InstanceType = $InstanceType
            $CustomProperty.PerformShutdown = $PerformShutdown
            $CustomProperty.RecoveryPointName = $RecoveryPointName
        }
        $null = $PSBoundParameters.Remove('InstanceType')
        $null = $PSBoundParameters.Remove('PerformShutdown')
        $null = $PSBoundParameters.Remove('RecoveryPointName')
        $null = $PSBoundParameters.Remove('ProtectedItemId')
        $null = $PSBoundParameters.Remove('InputObject')
        $null = $PSBoundParameters.Remove('CustomProperty')

        $null = $PSBoundParameters.Add('ResourceGroupName', $ResourceGroupName)
        $null = $PSBoundParameters.Add('VaultName', $VaultName)
        $null = $PSBoundParameters.Add('ProtectedItemName', $ProtectedItemName)
        $null = $PSBoundParameters.Add('CustomProperty', $CustomProperty)
        
        return Az.DataReplication.internal\Invoke-AzDataReplicationUnplannedProtectedItemFailover @PSBoundParameters
    }
}  