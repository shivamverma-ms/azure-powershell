---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.datareplication/start-azdatareplicationunplannedfailoverjob
schema: 2.0.0
---

# Start-AzDataReplicationUnplannedFailoverJob

## SYNOPSIS


## SYNTAX

### ByProtectedItemId (Default)
```
Start-AzDataReplicationUnplannedFailoverJob -ProtectedItemId <String>
 -CustomProperty <IUnplannedFailoverModelCustomProperties> [-SubscriptionId <String>] [<CommonParameters>]
```

### ByInputObject
```
Start-AzDataReplicationUnplannedFailoverJob -CustomProperty <IUnplannedFailoverModelCustomProperties>
 -InputObject <IProtectedItemModel> [-SubscriptionId <String>] [<CommonParameters>]
```

### ByInputObjectExpanded
```
Start-AzDataReplicationUnplannedFailoverJob -InputObject <IProtectedItemModel> -InstanceType <String>
 [-SubscriptionId <String>] [-PerformShutdown <Boolean>] [-RecoveryPointName <String>] [<CommonParameters>]
```

### ByProtectedItemIdExpanded
```
Start-AzDataReplicationUnplannedFailoverJob -ProtectedItemId <String> -InstanceType <String>
 [-SubscriptionId <String>] [-PerformShutdown <Boolean>] [-RecoveryPointName <String>] [<CommonParameters>]
```

## DESCRIPTION


## EXAMPLES

### Example 1: {{ Add title here }}
```powershell
{{ Add code here }}
```

```output
{{ Add output here }}
```

{{ Add description here }}

### Example 2: {{ Add title here }}
```powershell
{{ Add code here }}
```

```output
{{ Add output here }}
```

{{ Add description here }}

## PARAMETERS

### -CustomProperty
To construct, see NOTES section for CUSTOMPROPERTY properties and create a hash table.

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IUnplannedFailoverModelCustomProperties
Parameter Sets: ByInputObject, ByProtectedItemId
Aliases:

Required: True
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

### -PerformShutdown


```yaml
Type: System.Boolean
Parameter Sets: ByInputObjectExpanded, ByProtectedItemIdExpanded
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

### -RecoveryPointName


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


CUSTOMPROPERTY <IUnplannedFailoverModelCustomProperties>: 
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

