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
function Start-AzDataReplicationApplyRecoveryPoint{
    [CmdletBinding(DefaultParameterSetName='ByRecoveryPointName', PositionalBinding=$false)]
    param (   
        [Parameter(ParameterSetName='ByRecoveryPointName', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Path')]
        [System.String]
        # Specifies the resource group name.
        ${ResourceGroupName},

        [Parameter(ParameterSetName='ByRecoveryPointName', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Path')]
        [System.String]
        # Specifies the ProtectedItem Name.
        ${ProtectedItemName},

        [Parameter(ParameterSetName='ByRecoveryPointName', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Path')]
        [System.String]
        # Specifies the Vault Name
        ${VaultName},

        [Parameter(ParameterSetName='ByRecoveryPointName', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Path')]
        [System.String]
        # Specifies the Recovery Point Name
        ${RecoveryPointName},

        [Parameter(ParameterSetName='ByRecoveryPointId', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Path')]
        [System.String]
        # Specifies the recovery point id
        ${RecoveryPointId},

        [Parameter(ParameterSetName='ByInputObject', Mandatory)]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IChangeRecoveryPointModel]
        # Specifies Object of type ChangeRecoveryPointModel
        ${InputObject},

        [Parameter(ParameterSetName='ByRecoveryPointName')]
        [Parameter(ParameterSetName='ByRecoveryPointId')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Body')]
        [System.String]
        ${CustomPropertyInstanceType},
        # Specifies instance type

        [Parameter()]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Category('Path')]
        [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Runtime.DefaultInfo(Script='(Get-AzContext).Subscription.Id')]
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
    process{
       $parameterSet = $PSCmdlet.ParameterSetName
       if(($parameterSet -eq 'ByInputObject' ) -or ($parameterSet -eq 'ByRecoveryPointId')){
        if($null -ne $InputObject){
            $ProtectedItemId = $InputObject.Id
            $CustomPropertyInstanceType = $InputObject.CustomPropertyInstanceType
        }
        $MachineIdArray = $ProtectedItemId.Split("/")
        $ResourceGroupName = $MachineIdArray[4]
        $VaultName = $MachineIdArray[8]
        $ProtectedItemName = $MachineIdArray[10]
        $RecoveryPointName = $MachineIdArray[12]      
       }    
       $null = $PSBoundParameters.Remove('RecoveryPointId')
       $null = $PSBoundParameters.Remove('CustomPropertyInstanceType')
       $null = $PSBoundParameters.Remove('InputObject')
       $null = $PSBoundParameters.Remove('ResourceGroupName')
       $null = $PSBoundParameters.Remove('VaultName')
       $null = $PSBoundParameters.Remove('ProtectedItemName')
       $null = $PSBoundParameters.Remove('RecoveryPointName')

       $null = $PSBoundParameters.Add("ResourceGroupName", $ResourceGroupName)
       $null = $PSBoundParameters.Add("VaultName", $VaultName)
       $null = $PSBoundParameters.Add("ProtectedItemName", $ProtectedItemName)
       $null = $PSBoundParameters.Add("RecoveryPointName", $RecoveryPointName)
       $null = $PSBoundParameters.Add("CustomPropertyInstanceType", $CustomPropertyInstanceType)

       return Az.DataReplication.internal\Rename-ProtectedItemRecoveryPoint @PSBoundParameters
    }
} 