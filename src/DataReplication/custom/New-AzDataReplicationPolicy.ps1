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
function New-AzDataReplicationPolicy{
    [CmdletBinding(DefaultParameterSetName='ByProperties', PositionalBinding=$false, ConfirmImpact='Medium')]
    param (
        
        [Parameter(Mandatory)]    
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Path')]
        [System.String]
        # Specifies the resource group name.
        ${ResourceGroupName},

        [Parameter(Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Path')]
        [System.String]
        # Specifies the policy name.
        ${Name},

        [Parameter(Mandatory)] 
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Path')]
        [System.String]
        # Specifies the vault name
        ${VaultName},

        [Parameter(ParameterSetName='ByInputObject', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IPolicyModelCustomProperties]
        # Specifies custom properties for policy
        ${CustomProperty},

        [Parameter(ParameterSetName='ByProperties', Mandatory)]
        [ValidateSet("VMwareToAvs", "VMwareToAvsFailback")]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        # Specifies instance type
        ${InstanceType},

        [Parameter(ParameterSetName='ByProperties', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.Int32]
        # Specifies app consistent frequency
        ${AppConsistentFrequencyInMinute},

        [Parameter(ParameterSetName='ByProperties', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.Int32]
        # Specifies recovery point history
        ${RecoveryPointHistoryInMinute},

        [Parameter(ParameterSetName='ByProperties', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.Int32]
        # Specifies crash consistent frequency
        ${CrashConsistentFrequencyInMinute},

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Path')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Runtime.DefaultInfo(Script='(Get-AzContext).Subscription.Id')]
        [System.String]
        # Specifies the subscription id.
        ${SubscriptionId}
    )

    process{

        $parameterSet = $PSCmdlet.ParameterSetName
        $acceptedInstanceTypes = "VMwareToAvs", "VMwareToAvsFailback"

        if ($null -ne $CustomProperty -and !$acceptedInstanceTypes.Contains($CustomProperty.InstanceType)) {
            throw "Instance type is not supported. Only VMwareToAvs and VMwareToAvsFailback are applicable"
        }
        
        if($parameterSet -eq 'ByProperties'){
         $null = $PSBoundParameters.Remove('CustomProperty')
         
         $policyCustomProperty = [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.VMwareToAvsPolicyModelCustomProperties]::new()
         $policyCustomProperty.InstanceType = $InstanceType
         $policyCustomProperty.AppConsistentFrequencyInMinute = $AppConsistentFrequencyInMinute
         $policyCustomProperty.RecoveryPointHistoryInMinute = $RecoveryPointHistoryInMinute
         $policyCustomProperty.CrashConsistentFrequencyInMinute = $CrashConsistentFrequencyInMinute
         $CustomProperty = $policyCustomProperty   
        }

        $null = $PSBoundParameters.Clear();
 
        $null = $PSBoundParameters.Add('Name', $Name)
        $null = $PSBoundParameters.Add('CustomProperty', $CustomProperty)
        $null = $PSBoundParameters.Add('ResourceGroupName', $ResourceGroupName)
        $null = $PSBoundParameters.Add('VaultName', $VaultName)
 
        Az.DataReplication.internal\New-AzDataReplicationPolicy @PSBoundParameters

    }

}


    