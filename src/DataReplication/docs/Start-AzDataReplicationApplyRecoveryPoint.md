---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.datareplication/start-azdatareplicationapplyrecoverypoint
schema: 2.0.0
---

# Start-AzDataReplicationApplyRecoveryPoint

## SYNOPSIS


## SYNTAX

### ByRecoveryPointName (Default)
```
Start-AzDataReplicationApplyRecoveryPoint -ProtectedItemName <String> -RecoveryPointName <String>
 -ResourceGroupName <String> -VaultName <String> [-SubscriptionId <String>]
 [-CustomPropertyInstanceType <String>] [-DefaultProfile <PSObject>] [-AsJob] [-NoWait] [-PassThru]
 [<CommonParameters>]
```

### ByInputObject
```
Start-AzDataReplicationApplyRecoveryPoint -InputObject <IChangeRecoveryPointModel> [-SubscriptionId <String>]
 [-DefaultProfile <PSObject>] [-AsJob] [-NoWait] [-PassThru] [<CommonParameters>]
```

### ByRecoveryPointId
```
Start-AzDataReplicationApplyRecoveryPoint -RecoveryPointId <String> [-SubscriptionId <String>]
 [-CustomPropertyInstanceType <String>] [-DefaultProfile <PSObject>] [-AsJob] [-NoWait] [-PassThru]
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

### -CustomPropertyInstanceType


```yaml
Type: System.String
Parameter Sets: ByRecoveryPointId, ByRecoveryPointName
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
Type: Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IChangeRecoveryPointModel
Parameter Sets: ByInputObject
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

### -ProtectedItemName


```yaml
Type: System.String
Parameter Sets: ByRecoveryPointName
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RecoveryPointId


```yaml
Type: System.String
Parameter Sets: ByRecoveryPointId
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
Parameter Sets: ByRecoveryPointName
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
Parameter Sets: ByRecoveryPointName
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
Parameter Sets: ByRecoveryPointName
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


INPUTOBJECT <IChangeRecoveryPointModel>: 
  - `[CustomPropertyInstanceType <String>]`: Gets or sets the instance type.
  - `[RecoveryPointName <String>]`: Gets or sets the name of the recovery point.

## RELATED LINKS

