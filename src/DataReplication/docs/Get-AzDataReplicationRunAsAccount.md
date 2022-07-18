---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.datareplication/get-azdatareplicationrunasaccount
schema: 2.0.0
---

# Get-AzDataReplicationRunAsAccount

## SYNOPSIS
Method to get run as account.

## SYNTAX

### List (Default)
```
Get-AzDataReplicationRunAsAccount -ResourceGroupName <String> -SiteName <String> [-SubscriptionId <String[]>]
 [-DefaultProfile <PSObject>] [<CommonParameters>]
```

### Get
```
Get-AzDataReplicationRunAsAccount -AccountName <String> -ResourceGroupName <String> -SiteName <String>
 [-SubscriptionId <String[]>] [-DefaultProfile <PSObject>] [<CommonParameters>]
```

## DESCRIPTION
Method to get run as account.

## EXAMPLES

### Example 1: List
```powershell
Get-AzDataReplicationRunAsAccount -ResourceGroupName arpita-air -SiteName avsmay23c242vmwaresite
```

```output
Name                                 Type
----                                 ----
3da4030e-fa0b-51d6-b743-102911271eb3 Microsoft.OffAzure/VMwareSites/runasaccounts
5d018b30-06a5-502f-a35e-37495831840a Microsoft.OffAzure/VMwareSites/runasaccounts
9866f3c4-d074-5027-8582-defc4f1ef4a8 Microsoft.OffAzure/VMwareSites/runasaccounts
17878c3c-919f-5aba-9b36-455be7166322 Microsoft.OffAzure/VMwareSites/runasaccounts
48e8ddc4-4425-5401-9e1b-74429c02f967 Microsoft.OffAzure/VMwareSites/runasaccounts
```

{{ Add description here }}

### Example 2: Get
```powershell
Get-AzDataReplicationRunAsAccount -ResourceGroupName arpita-air -SiteName avsmay23c242vmwaresite -AccountName 48e8ddc4-4425-5401-9e1b-74429c02f967 | fl
```

```output
CreatedTimestamp : 2022-05-27T05:29:00.1666927Z
CredentialType   : WindowsGuest
DisplayName      : wincredsnew
Id               : /subscriptions/b364ed8d-4279-4bf8-8fd1-56f8fa0ae05c/resourceGroups/arpita-air/providers/Microsoft.Of
                   fAzure/VMwareSites/avsmay23c242vmwaresite/runasaccounts/48e8ddc4-4425-5401-9e1b-74429c02f967
Name             : 48e8ddc4-4425-5401-9e1b-74429c02f967
Type             : Microsoft.OffAzure/VMwareSites/runasaccounts
UpdatedTimestamp : 2022-07-06T12:03:49.9207350Z
```

{{ Add description here }}

## PARAMETERS

### -AccountName
Run as account ARM name.

```yaml
Type: System.String
Parameter Sets: Get
Aliases:

Required: True
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

### -ResourceGroupName
The name of the resource group.
The name is case insensitive.

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

### -SiteName
Site name.

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
The ID of the target subscription.

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

### Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api202001.IVMwareRunAsAccount

## NOTES

ALIASES

## RELATED LINKS

