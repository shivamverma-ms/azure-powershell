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
function Get-AzDataReplicationProtectedItem {
    [CmdletBinding(DefaultParameterSetName = 'List', PositionalBinding = $false, ConfirmImpact = 'Medium')]
    param (
        
        [Parameter(ParameterSetName = 'List', Mandatory)]    
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Path')]
        [System.String]
        # Specifies the resource group name.
        ${ResourceGroupName},

        [Parameter(ParameterSetName = 'List', Mandatory)] 
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Path')]
        [System.String]
        # Specifies the vault name
        ${VaultName},

        [Parameter(ParameterSetName = 'GetByProtectedItemId', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Path')]
        [System.String]
        # Specifies the Protected Item Id
        ${ProtectedItemId},

        [Parameter(ParameterSetName = 'GetByInputObject', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IProtectedItemModel]
        # Specifies Object of type ProtectedItemModel
        ${InputObject},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Path')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Runtime.DefaultInfo(Script = '(Get-AzContext).Subscription.Id')]
        [System.String[]]
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
        if (($parameterSet -eq 'GetByInputObject' ) -or ($parameterSet -eq 'GetByProtectedItemId')) {
            if ($null -ne $InputObject) {
                $ProtectedItemId = $InputObject.Id
                $null = $PSBoundParameters.Remove('InputObject')
            }
            $MachineIdArray = $ProtectedItemId.Split("/")
            $ResourceGroupName = $MachineIdArray[4]
            $VaultName = $MachineIdArray[8]
            $ProtectedItemName = $MachineIdArray[10]
        }
        $null = $PSBoundParameters.Remove('ProtectedItemId')
        $null = $PSBoundParameters.Remove('ResourceGroupName')
        $null = $PSBoundParameters.Remove('VaultName')
        $null = $PSBoundParameters.Remove('Name')
        $null = $PSBoundParameters.Remove('InputObject')

        if ($null -ne $ProtectedItemName) {
            $null = $PSBoundParameters.Add("Name", $ProtectedItemName)
        }   
        $null = $PSBoundParameters.Add("ResourceGroupName", $ResourceGroupName)
        $null = $PSBoundParameters.Add("VaultName", $VaultName)

        return Az.DataReplication.internal\Get-AzDataReplicationProtectedItem @PSBoundParameters
    }
} 