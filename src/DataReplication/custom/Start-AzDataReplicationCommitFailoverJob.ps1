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
function Start-AzDataReplicationCommitFailoverJob {
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
        if (($parameterSet -eq 'ByInputObject' ) -or ($parameterSet -eq 'ByProtectedItemId')) {
            if ($null -ne $InputObject) {
                $ProtectedItemId = $InputObject.Id
            }
            $MachineIdArray = $ProtectedItemId.Split("/")
            $ResourceGroupName = $MachineIdArray[4]
            $VaultName = $MachineIdArray[8]
            $ProtectedItemName = $MachineIdArray[10]
        }
        $null = $PSBoundParameters.Remove('ProtectedItemId')
        $null = $PSBoundParameters.Remove('InputObject')
        $null = $PSBoundParameters.Add('ResourceGroupName', $ResourceGroupName)
        $null = $PSBoundParameters.Add('VaultName', $VaultName)
        $null = $PSBoundParameters.Add('ProtectedItemName', $ProtectedItemName)
        $null = $PSBoundParameters.Add('NoWait', $true)

        $output = Az.DataReplication.internal\Invoke-AzDataReplicationCommitProtectedItemFailover @PSBoundParameters
        $JobName = $output.Target.Split("/")[14].Split("?")[0]

        # Remove parameters which are not necessary for getting job
        $null = $PSBoundParameters.Remove('ProtectedItemName')
        $null = $PSBoundParameters.Remove('CustomProperty')
        $null = $PSBoundParameters.Remove('NoWait')

        # Add the parameters which are required for getting Job
        $null = $PSBoundParameters.Add('Name', $JobName)
        $job = Get-AzDataReplicationJob @PSBoundParameters 
        return $job
    }
}   