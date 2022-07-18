---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.datareplication/start-azdatareplicationplannedfailoverjob
schema: 2.0.0
---

# Start-AzDataReplicationPlannedFailoverJob

## SYNOPSIS


## SYNTAX

### ByProtectedItemId (Default)
```
Start-AzDataReplicationPlannedFailoverJob -ProtectedItemId <String>
 -CustomProperty <IPlannedFailoverModelCustomProperties> [-SubscriptionId <String>]
 [-DefaultProfile <PSObject>] [-AsJob] [-NoWait] [-PassThru] [<CommonParameters>]
```

### ByInputObject
```
Start-AzDataReplicationPlannedFailoverJob -CustomProperty <IPlannedFailoverModelCustomProperties>
 -InputObject <IProtectedItemModel> [-SubscriptionId <String>] [-DefaultProfile <PSObject>] [-AsJob] [-NoWait]
 [-PassThru] [<CommonParameters>]
```

### ByInputObjectExpanded
```
Start-AzDataReplicationPlannedFailoverJob -InputObject <IProtectedItemModel> -InstanceType <String>
 [-SubscriptionId <String>] [-RecoveryPointType <String>] [-DefaultProfile <PSObject>] [-AsJob] [-NoWait]
 [-PassThru] [<CommonParameters>]
```

### ByProtectedItemIdExpanded
```
Start-AzDataReplicationPlannedFailoverJob -ProtectedItemId <String> -InstanceType <String>
 [-SubscriptionId <String>] [-RecoveryPointType <String>] [-DefaultProfile <PSObject>] [-AsJob] [-NoWait]
 [-PassThru] [<CommonParameters>]
```

## DESCRIPTION


## EXAMPLES

### Example 1: ByInputObject
```powershell
$CustomProperty = [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.VMwareToAvsFailbackPlannedFailoverModelCustomProperties]::new()
$CustomProperty.InstanceType = "VMwareToAvsFailback"
$CustomProperty.RecoveryPointType = "CrashConsistent"

Start-AzDataReplicationPlannedFailoverJob -CustomProperty $CustomProperty -InputObject $protectedItem
```

```output
{{ Add output here }}
```

{{ Add description here }}

### Example 2: ByProtectedItemIdExpanded
```powershell
Start-AzDataReplicationPlannedFailoverJob -ProtectedItemId "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air/providers/Microsoft.DataReplication/replicationVaults/protectedItems/avsvcenter_50153f67-367e-440b-69a6-20870cec95a5" -InstanceType "VMwareToAvsFailback" -RecoveryPointType "CrashConsistent"
```

```output
{{ Add output here }}
```

{{ Add description here }}

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

### -CustomProperty
To construct, see NOTES section for CUSTOMPROPERTY properties and create a hash table.

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IPlannedFailoverModelCustomProperties
Parameter Sets: ByInputObject, ByProtectedItemId
Aliases:

Required: True
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
Parameter Sets: ByInputObject, ByInputObjectExpanded
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InstanceType


```yaml
Type: System.String
Parameter Sets: ByInputObjectExpanded, ByProtectedItemIdExpanded
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
Parameter Sets: ByProtectedItemId, ByProtectedItemIdExpanded
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RecoveryPointType


```yaml
Type: System.String
Parameter Sets: ByInputObjectExpanded, ByProtectedItemIdExpanded
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SubscriptionId


```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: (Get-AzContext).Subscription.Id
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


CUSTOMPROPERTY <IPlannedFailoverModelCustomProperties>: 
  - `InstanceType <String>`: Gets or sets the instance type.

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

