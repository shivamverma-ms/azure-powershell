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
function Start-AzDataReplicationTestFailoverJob {
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
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.ITestFailoverModelCustomProperties]
        # Specifies Custom Properties for Test Failover job
        ${CustomProperty},

        [Parameter(ParameterSetName = 'ByInputObjectExpanded', Mandatory)]
        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded', Mandatory)]
        [ValidateSet("VMwareToAvs", "VMwareToAvsFailback")]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies instance type
        ${InstanceType},

        [Parameter(ParameterSetName = 'ByInputObjectExpanded')]
        [Parameter(ParameterSetName = 'ByProtectedItemIdExpanded')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies the id of the network
        ${NetworkId},

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
        ${SubscriptionId}
    )

    process {

        $parameterSet = $PSCmdlet.ParameterSetName
        $acceptedInstanceTypes = "VMwareToAvs", "VMwareToAvsFailback"

        if ($null -ne $CustomProperty -and !$acceptedInstanceTypes.Contains($CustomProperty.InstanceType)) {
            throw "Instance type is not supported. Only VMwareToAvs and VMwareToAvsFailback are applicable"
        }

        if ($null -ne $InputObject) {
            $ProtectedItemId = $InputObject.Id
        }
    
    
        $MachineIdArray = $ProtectedItemId.Split("/")
        $ResourceGroupName = $MachineIdArray[4]
        $VaultName = $MachineIdArray[8]
        $ProtectedItemName = $MachineIdArray[10]

        if ($parameterSet -eq "ByProtectedItemIdExpanded" -or $parameterSet -eq "ByInputObjectExpanded") {
            $CustomProperty = [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.VMwareToAvsTestFailoverModelCustomProperties]::new()
            $CustomProperty.InstanceType = $InstanceType
            $CustomProperty.NetworkId = $NetworkId
            $CustomProperty.RecoveryPointName = $RecoveryPointName
        }
       
        $null = $PSBoundParameters.Clear()
       
        $null = $PSBoundParameters.Add('ResourceGroupName', $ResourceGroupName)
        $null = $PSBoundParameters.Add('VaultName', $VaultName)
        $null = $PSBoundParameters.Add('ProtectedItemName', $ProtectedItemName)
        $null = $PSBoundParameters.Add('CustomProperty', $CustomProperty)

        Az.DataReplication.internal\Test-AzDataReplicationProtectedItemFailover @PSBoundParameters
        
    }

}


    