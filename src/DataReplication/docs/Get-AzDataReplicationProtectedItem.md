---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.datareplication/get-azdatareplicationprotecteditem
schema: 2.0.0
---

# Get-AzDataReplicationProtectedItem

## SYNOPSIS


## SYNTAX

### List (Default)
```
Get-AzDataReplicationProtectedItem -ResourceGroupName <String> -VaultName <String>
 [-SubscriptionId <String[]>] [-DefaultProfile <PSObject>] [-AsJob] [-NoWait] [-PassThru] [<CommonParameters>]
```

### GetByInputObject
```
Get-AzDataReplicationProtectedItem -InputObject <IProtectedItemModel> [-SubscriptionId <String[]>]
 [-DefaultProfile <PSObject>] [-AsJob] [-NoWait] [-PassThru] [<CommonParameters>]
```

### GetByProtectedItemId
```
Get-AzDataReplicationProtectedItem -ProtectedItemId <String> [-SubscriptionId <String[]>]
 [-DefaultProfile <PSObject>] [-AsJob] [-NoWait] [-PassThru] [<CommonParameters>]
```

### GetByProtectedItemName
```
Get-AzDataReplicationProtectedItem -ProtectedItemName <String> -ResourceGroupName <String> -VaultName <String>
 [-SubscriptionId <String[]>] [-DefaultProfile <PSObject>] [-AsJob] [-NoWait] [-PassThru] [<CommonParameters>]
```

## DESCRIPTION


## EXAMPLES

### Example 1: GetByProtectedItemName
```powershell
 Get-AzDataReplicationProtectedItem -ResourceGroupName arpita-air -VaultName vijamijun15 -ProtectedItemName "avsvcenter_5015355d-7426-885a-87e6-24ecf766eb6d" | fl
```

```output
AllowedJob                                : {Reprotect, DisableProtection}
CorrelationId                             : 717d62cb-12c7-46de-8bb3-1f070179aa8a
CurrentJobDisplayName                     :
CurrentJobEndTime                         :
CurrentJobId                              :
CurrentJobName                            :
CurrentJobScenarioName                    :
CurrentJobStartTime                       :
CurrentJobState                           :
CustomProperty                            : Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.VMwareToAvsProtectedItemModelCustomProperties
DraId                                     : b4d21e44-772b-421b-8c83-1bb7e268d00a
FabricId                                  : b6f5a785-0c89-4831-a950-b7d07416a1c7
FabricObjectId                            : 94eb5410-718c-55e3-981b-ac09a9bffe6b
FabricObjectName                          : vijamiwinvm1
HealthError                               : {}
Id                                        : /subscriptions/b364ed8d-4279-4bf8-8fd1-56f8fa0ae05c/resourceGroups/arpita-air/providers/Microsoft.DataReplication/replicationVaults/vijamijun15/protec
tedItems/avsvcenter_5015355d-7426-885a-87e6-24ecf766eb6d
LastFailedEnableProtectionJobDisplayName  :
LastFailedEnableProtectionJobEndTime      :
LastFailedEnableProtectionJobId           :
LastFailedEnableProtectionJobName         :
LastFailedEnableProtectionJobScenarioName :
LastFailedEnableProtectionJobStartTime    :
LastFailedEnableProtectionJobState        :
LastSuccessfulPlannedFailoverTime         :
LastSuccessfulTestFailoverTime            : 7/7/2022 7:24:37 AM
LastSuccessfulUnplannedFailoverTime       : 7/8/2022 10:11:08 AM
LastTestFailoverJobDisplayName            : Test failover
LastTestFailoverJobEndTime                : 7/7/2022 7:54:12 AM
LastTestFailoverJobId                     : /subscriptions/b364ed8d-4279-4bf8-8fd1-56f8fa0ae05c/resourceGroups/arpita-air/providers/Microsoft.DataReplication/replicationVaults/vijamijun15/jobs/8
2adb394-73f2-410b-9823-81f482a91983
LastTestFailoverJobName                   : 82adb394-73f2-410b-9823-81f482a91983
LastTestFailoverJobScenarioName           : TestFailover
LastTestFailoverJobStartTime              : 7/7/2022 7:24:37 AM
LastTestFailoverJobState                  : Succeeded
Name                                      : avsvcenter_5015355d-7426-885a-87e6-24ecf766eb6d
PolicyName                                : 24-hour-replication-policy
ProtectionState                           : CommitFailoverCompleted
ProtectionStateDescription                : Failover committed
ProvisioningState                         : Succeeded
ReplicationExtensionName                  : vmware-avs-replication-extension
ReplicationHealth                         : Normal
ResyncRequired                            :
ResynchronizationState                    : None
SourceFabricProviderId                    : a22bd266-b3eb-49d5-af13-c72780831154
SystemDataCreatedAt                       :
SystemDataCreatedBy                       :
SystemDataCreatedByType                   :
SystemDataLastModifiedAt                  :
SystemDataLastModifiedBy                  :
SystemDataLastModifiedByType              :
Tag                                       : Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Previe
                                            w.ProtectedItemModelTags
TargetDraId                               :
TargetFabricId                            :
TargetFabricProviderId                    : 6aeb30a5-9dee-498b-a575-5360e7798286
TestFailoverState                         : MarkedForDeletion
TestFailoverStateDescription              : Test failover cleanup operation has ended
Type                                      : Microsoft.DataReplication/replicationVaults/protectedItems
```

## PARAMETERS

### -AsJob


```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DefaultProfile


```yaml
Type: System.Management.Automation.PSObject
Parameter Sets: (All)
Aliases: AzureRMContext, AzureCredential

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject
To construct, see NOTES section for INPUTOBJECT properties and create a hash table.

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IProtectedItemModel
Parameter Sets: GetByInputObject
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoWait


```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PassThru


```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ProtectedItemId


```yaml
Type: System.String
Parameter Sets: GetByProtectedItemId
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ProtectedItemName


```yaml
Type: System.String
Parameter Sets: GetByProtectedItemName
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceGroupName


```yaml
Type: System.String
Parameter Sets: GetByProtectedItemName, List
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SubscriptionId


```yaml
Type: System.String[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: (Get-AzContext).Subscription.Id
Accept pipeline input: False
Accept wildcard characters: False
```

### -VaultName


```yaml
Type: System.String
Parameter Sets: GetByProtectedItemName, List
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### System.Object

## NOTES

ALIASES

COMPLEX PARAMETER PROPERTIES

To create the parameters described below, construct a hash table containing the appropriate properties. For information on hash tables, run Get-Help about_Hash_Tables.


INPUTOBJECT <IProtectedItemModel>: 
  - `[CustomProperty <IProtectedItemModelCustomProperties>]`: Protected item model custom properties.
    - `InstanceType <String>`: Gets or sets the instance type.
  - `[PolicyName <String>]`: Gets or sets the policy name.
  - `[ReplicationExtensionName <String>]`: Gets or sets the replication extension name.
  - `[SystemDataCreatedAt <DateTime?>]`: Gets or sets the timestamp of resource creation (UTC).
  - `[SystemDataCreatedBy <String>]`: Gets or sets identity that created the resource.
  - `[SystemDataCreatedByType <String>]`: Gets or sets the type of identity that created the resource: user, application,         managedIdentity.
  - `[SystemDataLastModifiedAt <DateTime?>]`: Gets or sets the timestamp of resource last modification (UTC).
  - `[SystemDataLastModifiedBy <String>]`: Gets or sets the identity that last modified the resource.
  - `[SystemDataLastModifiedByType <String>]`: Gets or sets the type of identity that last modified the resource: user, application,         managedIdentity.
  - `[Tag <IProtectedItemModelTags>]`: Gets or sets the resource tags.
    - `[(Any) <String>]`: This indicates any property can be added to this object.

## RELATED LINKS

