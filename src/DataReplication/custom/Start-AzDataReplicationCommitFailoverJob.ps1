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
    [CmdletBinding(DefaultParameterSetName = 'ByProtectedItemId', PositionalBinding = $false, ConfirmImpact = 'Medium')]
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
        ${SubscriptionId}
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
 
        $null = $PSBoundParameters.Clear()

        $null = $PSBoundParameters.Add('ResourceGroupName', $ResourceGroupName)
        $null = $PSBoundParameters.Add('VaultName', $VaultName)
        $null = $PSBoundParameters.Add('ProtectedItemName', $ProtectedItemName)

        Az.DataReplication.internal\Invoke-AzDataReplicationCommitProtectedItemFailover @PSBoundParameters
    }


}


    