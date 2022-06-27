---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.datareplication/new-azdatareplicationpolicy
schema: 2.0.0
---

# New-AzDataReplicationPolicy

## SYNOPSIS


## SYNTAX

### ByProperties (Default)
```
New-AzDataReplicationPolicy -Name <String> -ResourceGroupName <String> -VaultName <String>
 -AppConsistentFrequencyInMinute <Int32> -CrashConsistentFrequencyInMinute <Int32> -InstanceType <String>
 -RecoveryPointHistoryInMinute <Int32> [-SubscriptionId <String>] [<CommonParameters>]
```

### ByInputObject
```
New-AzDataReplicationPolicy -Name <String> -ResourceGroupName <String> -VaultName <String>
 -CustomProperty <IPolicyModelCustomProperties> [-SubscriptionId <String>] [<CommonParameters>]
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

### -AppConsistentFrequencyInMinute


```yaml
Type: System.Int32
Parameter Sets: ByProperties
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CrashConsistentFrequencyInMinute


```yaml
Type: System.Int32
Parameter Sets: ByProperties
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CustomProperty
To construct, see NOTES section for CUSTOMPROPERTY properties and create a hash table.

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IPolicyModelCustomProperties
Parameter Sets: ByInputObject
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
Parameter Sets: ByProperties
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name


```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RecoveryPointHistoryInMinute


```yaml
Type: System.Int32
Parameter Sets: ByProperties
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
Parameter Sets: (All)
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

### -VaultName


```yaml
Type: System.String
Parameter Sets: (All)
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


CUSTOMPROPERTY <IPolicyModelCustomProperties>: 
  - `InstanceType <String>`: Gets or sets the instance type.

## RELATED LINKS

