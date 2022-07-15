---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.datareplication/get-azdatareplicationpolicy
schema: 2.0.0
---

# Get-AzDataReplicationPolicy

## SYNOPSIS
Gets the details of the policy.

## SYNTAX

### List (Default)
```
Get-AzDataReplicationPolicy -ResourceGroupName <String> -VaultName <String> [-SubscriptionId <String[]>]
 [-DefaultProfile <PSObject>] [<CommonParameters>]
```

### Get
```
Get-AzDataReplicationPolicy -Name <String> -ResourceGroupName <String> -VaultName <String>
 [-SubscriptionId <String[]>] [-DefaultProfile <PSObject>] [<CommonParameters>]
```

## DESCRIPTION
Gets the details of the policy.

## EXAMPLES

### Example 1: List
```powershell
Get-AzDataReplicationPolicy -ResourceGroupName arpita-air -VaultName vijamijun15
```

```output
Name                                        Type
----                                        ----
24-hour-replication-policy                  Microsoft.DataReplication/replicationVaults/replicationPolicies
appconsistency-vmware-avs-policy-failback   Microsoft.DataReplication/replicationVaults/replicationPolicies
crashconsistency-vmware-avs-policy-failback Microsoft.DataReplication/replicationVaults/replicationPolicies
test-policy                                 Microsoft.DataReplication/replicationVaults/replicationPolicies
```

{{ Add description here }}

### Example 2: Get
```powershell
Get-AzDataReplicationPolicy -Name 24-hour-replication-policy -ResourceGroupName arpita-air -VaultName vijamijun15 | fl
```

```output
CustomProperty               : Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.PolicyModelCustomProperties
Id                           : /subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/arpita-air/providers/Microsoft.DataReplication/replicationVaults/vijamijun15/replicationPolicies/24-hour-replication-policy
Name                         : 24-hour-replication-policy
ProvisioningState            : Succeeded
SystemDataCreatedAt          :
SystemDataCreatedBy          :
SystemDataCreatedByType      :
SystemDataLastModifiedAt     :
SystemDataLastModifiedBy     :
SystemDataLastModifiedByType :
Tag                          : Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.PolicyModelTags
Type                         : Microsoft.DataReplication/replicationVaults/replicationPolicies
```

{{ Add description here }}

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

### -Name
Policy name.

```yaml
Type: System.String
Parameter Sets: Get
Aliases: PolicyName

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

### Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IPolicyModel

## NOTES

ALIASES

## RELATED LINKS

