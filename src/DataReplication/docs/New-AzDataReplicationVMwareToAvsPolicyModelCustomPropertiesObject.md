---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.DataReplication/new-AzDataReplicationVMwareToAvsPolicyModelCustomPropertiesObject
schema: 2.0.0
---

# New-AzDataReplicationVMwareToAvsPolicyModelCustomPropertiesObject

## SYNOPSIS
Create an in-memory object for VMwareToAvsPolicyModelCustomProperties.

## SYNTAX

```
New-AzDataReplicationVMwareToAvsPolicyModelCustomPropertiesObject -InstanceType <String>
 [-AppConsistentFrequencyInMinute <Int32>] [-CrashConsistentFrequencyInMinute <Int32>]
 [-EnableMultiVMSync <Boolean>] [-RecoveryPointHistoryInMinute <Int32>] [<CommonParameters>]
```

## DESCRIPTION
Create an in-memory object for VMwareToAvsPolicyModelCustomProperties.

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
Gets or sets the app consistent snapshot frequency (in minutes).

```yaml
Type: System.Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CrashConsistentFrequencyInMinute
Gets or sets the crash consistent snapshot frequency (in minutes).

```yaml
Type: System.Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableMultiVMSync
Gets or sets a value indicating whether multi-VM sync has to be enabled.

```yaml
Type: System.Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InstanceType
Gets or sets the instance type.

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
Gets or sets the duration in minutes until which the recovery points need to be
        stored.

```yaml
Type: System.Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.VMwareToAvsPolicyModelCustomProperties

## NOTES

ALIASES

## RELATED LINKS

