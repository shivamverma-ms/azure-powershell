---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.datareplication/start-azdatareplicationresynchronizejob
schema: 2.0.0
---

# Start-AzDataReplicationResynchronizeJob

## SYNOPSIS


## SYNTAX

### ByProtectedItemId (Default)
```
Start-AzDataReplicationResynchronizeJob -ProtectedItemId <String> [-SubscriptionId <String>]
 [<CommonParameters>]
```

### ByInputObject
```
Start-AzDataReplicationResynchronizeJob -InputObject <IProtectedItemModel> [-SubscriptionId <String>]
 [<CommonParameters>]
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

### -InputObject
To construct, see NOTES section for INPUTOBJECT properties and create a hash table.

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IProtectedItemModel
Parameter Sets: ByInputObject
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ProtectedItemId


```yaml
Type: System.String
Parameter Sets: ByProtectedItemId
Aliases:

Required: True
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

