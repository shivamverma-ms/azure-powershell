---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.datareplication/get-azdatareplicationfabric
schema: 2.0.0
---

# Get-AzDataReplicationFabric

## SYNOPSIS
Gets the details of the fabric.

## SYNTAX

### List (Default)
```
Get-AzDataReplicationFabric [-SubscriptionId <String[]>] [-ContinuationToken <String>]
 [-DefaultProfile <PSObject>] [<CommonParameters>]
```

### Get
```
Get-AzDataReplicationFabric -Name <String> -ResourceGroupName <String> [-SubscriptionId <String[]>]
 [-DefaultProfile <PSObject>] [<CommonParameters>]
```

### ListByResourceGroup
```
Get-AzDataReplicationFabric -ResourceGroupName <String> [-SubscriptionId <String[]>]
 [-ContinuationToken <String>] [-DefaultProfile <PSObject>] [<CommonParameters>]
```

## DESCRIPTION
Gets the details of the fabric.

## EXAMPLES

### Example 1: ListByResourceGroup
```powershell
Get-AzDataReplicationFabric -ResourceGroupName arpita-air
```

```output
Location      Name                             Type
--------      ----                             ----
westeurope    AVS-31Mar-Signoff-vmwarefabric   Microsoft.DataReplication/replicationFabrics
centraluseuap Arpita-Jan10-vmwarefabric        Microsoft.DataReplication/replicationFabrics
centraluseuap arpita-Jan19-vmwarefabric        Microsoft.DataReplication/replicationFabrics
centraluseuap anbhatAVSVault22Jan-vmwarefabric Microsoft.DataReplication/replicationFabrics
centraluseuap anbhat9FebVault-vmwarefabric     Microsoft.DataReplication/replicationFabrics
centraluseuap AVSmarch02-vmwarefabric          Microsoft.DataReplication/replicationFabrics
centraluseuap vijamiavsmay12-vmwarefabric      Microsoft.DataReplication/replicationFabrics
centraluseuap avsmay23-vmwarefabric            Microsoft.DataReplication/replicationFabrics
centraluseuap aryan-vault-vmwarefabric         Microsoft.DataReplication/replicationFabrics
centraluseuap vijamijun15-vmwarefabric         Microsoft.DataReplication/replicationFabrics
```

## PARAMETERS

### -ContinuationToken
Continuation token from the previous call.

```yaml
Type: System.String
Parameter Sets: List, ListByResourceGroup
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -Name
Fabric name.

```yaml
Type: System.String
Parameter Sets: Get
Aliases: FabricName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceGroupName
Resource group name.

```yaml
Type: System.String
Parameter Sets: Get, ListByResourceGroup
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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IFabricModel

## NOTES

ALIASES

## RELATED LINKS

