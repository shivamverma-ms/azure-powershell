---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.datareplication/get-azdatareplicationdra
schema: 2.0.0
---

# Get-AzDataReplicationDra

## SYNOPSIS
Gets the details of the fabric agent.

## SYNTAX

### List (Default)
```
Get-AzDataReplicationDra -FabricName <String> -ResourceGroupName <String> [-SubscriptionId <String[]>]
 [-DefaultProfile <PSObject>] [<CommonParameters>]
```

### Get
```
Get-AzDataReplicationDra -FabricName <String> -Name <String> -ResourceGroupName <String>
 [-SubscriptionId <String[]>] [-DefaultProfile <PSObject>] [<CommonParameters>]
```

## DESCRIPTION
Gets the details of the fabric agent.

## EXAMPLES

### Example 1: List
```powershell
Get-AzDataReplicationDra -FabricName "vijamijun15-vmwarefabric" -ResourceGroupName "arpita-air"
```

```output
AuthenticationIdentityAadAuthority  : https://login.windows.net/72f988bf-86f1-41af-91ab-2d7cd011db47
AuthenticationIdentityApplicationId : eea53622-cc14-4f30-ad67-835ac8bfcb83
AuthenticationIdentityAudience      : api://72f988bf-86f1-41af-91ab-2d7cd011db47/vijamijun18b3eagentauthaadapp
AuthenticationIdentityObjectId      : a68155cd-a470-43a7-8016-19c52badac4e
AuthenticationIdentityTenantId      : 72f988bf-86f1-41af-91ab-2d7cd011db47
CorrelationId                       : c56fafa4-303b-45b0-a4a7-21cffe877310
CustomPropertyInstanceType          : VMware
HealthError                         : {}
Id                                  : /subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/arpita-air/providers/Microsoft.DataReplication/replicationFabrics/vijamijun15-vmwarefabric/fabricAgents/appjun15fbd3dra
IsResponsive                        :
LastHeartbeat                       : 7/14/2022 7:22:54 AM
MachineId                           : b4d21e44-772b-421b-8c83-1bb7e268d00a
MachineName                         : appjun15
Name                                : appjun15fbd3dra
ProvisioningState                   : Succeeded
ResourceAccessIdentityAadAuthority  : https://login.windows.net/72f988bf-86f1-41af-91ab-2d7cd011db47
ResourceAccessIdentityApplicationId : 6116cace-91c4-4e4b-bb86-b7518270d07e
ResourceAccessIdentityAudience      : api://72f988bf-86f1-41af-91ab-2d7cd011db47/vijamijun18b3efailbackagentauthaadapp
ResourceAccessIdentityObjectId      : d25c40ea-2dde-44f3-b97b-b5c4f62b7f7f
ResourceAccessIdentityTenantId      : 72f988bf-86f1-41af-91ab-2d7cd011db47
SystemDataCreatedAt                 :
SystemDataCreatedBy                 :
SystemDataCreatedByType             :
SystemDataLastModifiedAt            :
SystemDataLastModifiedBy            :
SystemDataLastModifiedByType        :
Tag                                 : Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.DraModelTags
Type                                : Microsoft.DataReplication/replicationFabrics/fabricAgents
VersionNumber                       : 1.22.614.4
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

### -FabricName
Fabric name.

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

### -Name
Dra name.

```yaml
Type: System.String
Parameter Sets: Get
Aliases: DraName

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IDraModel

## NOTES

ALIASES

## RELATED LINKS

