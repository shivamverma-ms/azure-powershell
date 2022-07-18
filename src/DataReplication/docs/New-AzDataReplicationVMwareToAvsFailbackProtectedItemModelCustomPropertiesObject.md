---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.DataReplication/new-AzDataReplicationVMwareToAvsFailbackProtectedItemModelCustomPropertiesObject
schema: 2.0.0
---

# New-AzDataReplicationVMwareToAvsFailbackProtectedItemModelCustomPropertiesObject

## SYNOPSIS
Create an in-memory object for VMwareToAvsFailbackProtectedItemModelCustomProperties.

## SYNTAX

```
New-AzDataReplicationVMwareToAvsFailbackProtectedItemModelCustomPropertiesObject -InstanceType <String>
 [-ProtectedDisk <IVMwareToAvsFailbackProtectedDiskProperties[]>]
 [-VMNic <IVMwareToAvsFailbackNicProperties[]>] [<CommonParameters>]
```

## DESCRIPTION
Create an in-memory object for VMwareToAvsFailbackProtectedItemModelCustomProperties.

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

### -ProtectedDisk
Gets or sets the list of protected disks.

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IVMwareToAvsFailbackProtectedDiskProperties[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -VMNic
Gets or sets the network details.

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IVMwareToAvsFailbackNicProperties[]
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

### Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.VMwareToAvsFailbackProtectedItemModelCustomProperties

## NOTES

ALIASES

## RELATED LINKS

