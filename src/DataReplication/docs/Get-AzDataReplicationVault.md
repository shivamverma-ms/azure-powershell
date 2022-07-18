---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.datareplication/get-azdatareplicationvault
schema: 2.0.0
---

# Get-AzDataReplicationVault

## SYNOPSIS
Gets the details of the vault.

## SYNTAX

### List (Default)
```
Get-AzDataReplicationVault [-SubscriptionId <String[]>] [-ContinuationToken <String>]
 [-DefaultProfile <PSObject>] [<CommonParameters>]
```

### Get
```
Get-AzDataReplicationVault -Name <String> -ResourceGroupName <String> [-SubscriptionId <String[]>]
 [-DefaultProfile <PSObject>] [<CommonParameters>]
```

### ListByResourceGroup
```
Get-AzDataReplicationVault -ResourceGroupName <String> [-SubscriptionId <String[]>]
 [-ContinuationToken <String>] [-DefaultProfile <PSObject>] [<CommonParameters>]
```

## DESCRIPTION
Gets the details of the vault.

## EXAMPLES

### Example 1: Get
```powershell
Get-AzDataReplicationVault -Name vijamijun15 -ResourceGroupName arpita-air | fl
```

```output
Id                           : /subscriptions/b364ed8d-4279-4bf8-8fd1-56f8fa0ae05c/resourceGroups/Arpita-air/providers/
                               Microsoft.DataReplication/replicationVaults/vijamijun15
Location                     : centraluseuap
Name                         : vijamijun15
ProvisioningState            : Succeeded
ServiceResourceId            : 89d56ec8-08f3-49b4-9ffb-922923578e0a
SystemDataCreatedAt          : 6/15/2022 3:06:17 PM
SystemDataCreatedBy          : vijami@microsoft.com
SystemDataCreatedByType      : User
SystemDataLastModifiedAt     : 6/15/2022 3:06:17 PM
SystemDataLastModifiedBy     : vijami@microsoft.com
SystemDataLastModifiedByType : User
Tag                          : Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.VaultModelT
                               ags
Type                         : Microsoft.DataReplication/replicationVaults
```

{{ Add description here }}

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
Vault name.

```yaml
Type: System.String
Parameter Sets: Get
Aliases: VaultName

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

### Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IVaultModel

## NOTES

ALIASES

## RELATED LINKS

