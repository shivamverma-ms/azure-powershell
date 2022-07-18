---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.datareplication/get-azdatareplicationevent
schema: 2.0.0
---

# Get-AzDataReplicationEvent

## SYNOPSIS
Gets the details of the event.

## SYNTAX

### List (Default)
```
Get-AzDataReplicationEvent -ResourceGroupName <String> -VaultName <String> [-SubscriptionId <String[]>]
 [-ContinuationToken <String>] [-Filter <String>] [-DefaultProfile <PSObject>] [<CommonParameters>]
```

### Get
```
Get-AzDataReplicationEvent -Name <String> -ResourceGroupName <String> -VaultName <String>
 [-SubscriptionId <String[]>] [-DefaultProfile <PSObject>] [<CommonParameters>]
```

## DESCRIPTION
Gets the details of the event.

## EXAMPLES

### Example 1: Get
```powershell
Get-AzDataReplicationEvent -ResourceGroupName arpita-air -VaultName vijamijun15 -Name ProtectedItem_avsvcenter_501531a0-6693-5861-a755-9c69897a51f9_95f89711-9b27-42d5-8a1c-864f93b0b879_ProtectedItemHealth_ProtectedItemHealthChanged_637933556299381531 | fl
```

```output
CorrelationId                : 97ae2bff-2715-4189-aed7-f25b7e099900
CustomProperty               : Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.EventModelCustomProperties
Description                  : Protected item health changed to Warning.
EventName                    : ProtectedItemHealthChanged
EventType                    : ProtectedItemHealth
HealthError                  : {}
Id                           : /subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/arpita-air/providers/Microsoft.DataReplication/replicationVaults/vijamijun15/events/ProtectedItem_avsvcenter_501531a0-6693-5861-a755-9c69897a51f9_95f89711-9b27-42d5-8a1c-864f93b0b879_ProtectedItemHealth_ProtectedItemHealthChanged_637933556299381531
Name                         : ProtectedItem_avsvcenter_501531a0-6693-5861-a755-9c69897a51f9_95f89711-9b27-42d5-8a1c-864f93b0b879_ProtectedItemHealth_ProtectedItemHealthChanged_637933556299381531
ResourceName                 : avsvcenter_501531a0-6693-5861-a755-9c69897a51f9
ResourceType                 : ProtectedItem
Severity                     : Warning
SystemDataCreatedAt          :
SystemDataCreatedBy          :
SystemDataCreatedByType      :
SystemDataLastModifiedAt     :
SystemDataLastModifiedBy     :
SystemDataLastModifiedByType :
Tag                          : Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.EventModelTags
TimeOfOccurrence             : 7/14/2022 12:33:49 AM
Type                         : Microsoft.DataReplication/replicationVaults/events
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

### -ContinuationToken
Continuation token.

```yaml
Type: System.String
Parameter Sets: List
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

### -Filter
Filter string.

```yaml
Type: System.String
Parameter Sets: List
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
Event name.

```yaml
Type: System.String
Parameter Sets: Get
Aliases: EventName

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

### Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IEventModel

## NOTES

ALIASES

## RELATED LINKS

