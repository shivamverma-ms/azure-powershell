---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.datareplication/get-azdatareplicationvcenter
schema: 2.0.0
---

# Get-AzDataReplicationVCenter

## SYNOPSIS
Method to get a vCenter.

## SYNTAX

### List (Default)
```
Get-AzDataReplicationVCenter -ResourceGroupName <String> -SiteName <String> [-SubscriptionId <String[]>]
 [-Filter <String>] [-DefaultProfile <PSObject>] [<CommonParameters>]
```

### Get
```
Get-AzDataReplicationVCenter -Name <String> -ResourceGroupName <String> -SiteName <String>
 [-SubscriptionId <String[]>] [-DefaultProfile <PSObject>] [<CommonParameters>]
```

## DESCRIPTION
Method to get a vCenter.

## EXAMPLES

### Example 1: List
```powershell
Get-AzDataReplicationVCenter -SiteName avsmay23c242vmwaresite -ResourceGroupName arpita-air | fl
```

```output
CreatedTimestamp    : 2022-05-23T07:50:36.9230225Z
Error               : {}
Fqdn                : idclab-vcen67.fareast.corp.microsoft.com
Id                  : /subscriptions/b364ed8d-4279-4bf8-8fd1-56f8fa0ae05c/resourceGroups/arpita-air/providers/Microsoft.OffAzure/VMwareSites/avsmay23c242vmwaresite/vcenters/avsvcenter
InstanceUuid        : db73f8f2-624c-4a0f-905b-8c6f34442cbc
Name                : avsvcenter
PerfStatisticsLevel : [{"Level":1,"Name":"past day","Enabled":true,"SamplingPeriod":300,"TimeLength":86400},{"Level":1,"Name":"past
                      week","Enabled":true,"SamplingPeriod":1800,"TimeLength":604800},{"Level":1,"Name":"past month","Enabled":true,"SamplingPeriod":7200,"TimeLength":2592000},{"Level":1,"Name":"past
                      year","Enabled":true,"SamplingPeriod":86400,"TimeLength":7776000}]
Port                : 443
RunAsAccountId      : /subscriptions/b364ed8d-4279-4bf8-8fd1-56f8fa0ae05c/resourceGroups/arpita-air/providers/Microsoft.OffAzure/VMwareSites/avsmay23c242vmwaresite/runasaccounts/9866f3c4-d074-5027-8582-defc4f1e
                      f4a8
Type                : Microsoft.OffAzure/VMwareSites/vcenters
UpdatedTimestamp    : 2022-07-07T14:18:44.9097668Z
Version             : 6.7.0
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

### -Filter
.

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
VCenter ARM name.

```yaml
Type: System.String
Parameter Sets: Get
Aliases: VcenterName

Required: True
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

### Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api202001.IVCenter

## NOTES

ALIASES

## RELATED LINKS

