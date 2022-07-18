---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.datareplication/get-azdatareplicationvaultstatistics
schema: 2.0.0
---

# Get-AzDataReplicationVaultStatistics

## SYNOPSIS
Gets the statistics for the vault.

## SYNTAX

```
Get-AzDataReplicationVaultStatistics -ResourceGroupName <String> -VaultName <String>
 [-SubscriptionId <String[]>] [-DefaultProfile <PSObject>] [<CommonParameters>]
```

## DESCRIPTION
Gets the statistics for the vault.

## EXAMPLES

### Example 1
```powershell
Get-AzDataReplicationVaultStatistics -VaultName vijamijun15 -ResourceGroupName arpita-air | fl
```

```output
FabricStatisticsFabricError             : {}
JobStatisticsCategorizedCount           : Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.WorkflowStatisticsModelCategorizedCounts
JobStatisticsCount                      : 2
ProtectedItemStatisticsCategorizedCount : Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.ProtectedItemStatisticsModelCategorizedCounts
ProtectedItemStatisticsCount            : 4
ProtectedItemStatisticsHealthError      : {}
VaultError                              : {}
```

## PARAMETERS

### -DefaultProfile
The credentials, account, tenant, and subscription used for communication with Azure.

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

### -ResourceGroupName
Resource group name.

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
The Subscription ID.

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
Vault name.

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

### Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IVaultStatisticsModelProperties

## NOTES

ALIASES

## RELATED LINKS

