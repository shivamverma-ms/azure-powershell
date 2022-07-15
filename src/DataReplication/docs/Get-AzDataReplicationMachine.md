---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.datareplication/get-azdatareplicationmachine
schema: 2.0.0
---

# Get-AzDataReplicationMachine

## SYNOPSIS
Method to get machine.

## SYNTAX

### List (Default)
```
Get-AzDataReplicationMachine -ResourceGroupName <String> -SiteName <String> [-SubscriptionId <String[]>]
 [-ContinuationToken <String>] [-Filter <String>] [-Top <Int32>] [-TotalRecordCount <Int32>]
 [-DefaultProfile <PSObject>] [<CommonParameters>]
```

### Get
```
Get-AzDataReplicationMachine -Name <String> -ResourceGroupName <String> -SiteName <String>
 [-SubscriptionId <String[]>] [-DefaultProfile <PSObject>] [<CommonParameters>]
```

## DESCRIPTION
Method to get machine.

## EXAMPLES

### Example 1: Get
```powershell
Get-AzDataReplicationMachine -ResourceGroupName arpita-air -SiteName avsmay23c242vmwaresite -Name avsvcenter_501570bd-ff46-4ac5-ff16-db65d03fc6f7 | fl
```

```output
AllocatedMemoryInMb               : 32768
AppAndRoleApplication             :
AppAndRoleBizTalkServer           :
AppAndRoleExchangeServer          :
AppAndRoleFeature                 :
AppAndRoleOtherDatabase           :
AppAndRoleSharePointServer        :
AppAndRoleSqlServer               :
AppAndRoleSystemCenter            :
AppAndRoleWebApplication          :
BiosGuid                          : 42157bd7-f566-08bb-ae41-65ada2f5bd74
BiosSerialNumber                  : 42157bd7-f566-08bb-ae41-65ada2f5bd74
ChangeTrackingEnabled             : False
ChangeTrackingSupported           : True
CreatedTimestamp                  : 2022-05-23T07:51:59.1275192Z
DataCenterScope                   : AVSTest
DependencyMapping                 : Disabled
DependencyMappingStartTime        :
Description                       : Microsoft Azure AVS DR Image on Windows Server 2016
Disk                              : {scsi0:0, scsi0:1}
DisplayName                       : AVSDRApplianceEUS
Error                             : {}
Firmware                          : bios
GuestDetailsDiscoveryTimestamp    :
GuestOSDetailOsname               :
GuestOSDetailOstype               :
GuestOSDetailOsversion            :
HostInMaintenanceMode             : False
HostName                          : 10.150.33.174
HostPowerState                    : poweredOn
HostVersion                       : 6.7.0
Id                                : /subscriptions/b364ed8d-4279-4bf8-8fd1-56f8fa0ae05c/resourceGroups/arpita-air/providers/Microsoft.OffAzure/VMwareSites/avsmay23c242vmwaresite/machines/avsvcenter_501570bd-ff46-4ac5-ff16-db65d03fc6f7
InstanceUuid                      : 501570bd-ff46-4ac5-ff16-db65d03fc6f7
IsDeleted                         : False
IsGuestDetailsDiscoveryInProgress : True
MaxSnapshot                       : -1
Name                              : avsvcenter_501570bd-ff46-4ac5-ff16-db65d03fc6f7
NetworkAdapter                    : {VM Network}
NumberOfApplication               : -1
NumberOfProcessorCore             : 8
OperatingSystemDetailOSName       : Microsoft Windows Server 2016 or later (64-bit)
OperatingSystemDetailOSType       : windowsguest
OperatingSystemDetailOSVersion    :
PowerStatus                       : OFF
Type                              : Microsoft.OffAzure/VMwareSites/machines
UpdatedTimestamp                  : 2022-07-06T12:03:51.7801856Z
VCenterFqdn                       : idclab-vcen67.fareast.corp.microsoft.com
VCenterId                         : /subscriptions/b364ed8d-4279-4bf8-8fd1-56f8fa0ae05c/resourceGroups/arpita-air/providers/Microsoft.OffAzure/VMwareSites/avsmay23c242vmwaresite/vcenters/avsvcenter
VMConfigurationFileLocation       : [datastore1] AVSDRApplianceEUS/AVSDRApplianceEUS.vmx
VMFqdn                            : WIN-3O3A0HLP47L
VMwareToolsStatus                 : NotRunning
```

## PARAMETERS

### -ContinuationToken
Optional parameter for continuation token.

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
Machine ARM name.

```yaml
Type: System.String
Parameter Sets: Get
Aliases: MachineName

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

### -Top
.

```yaml
Type: System.Int32
Parameter Sets: List
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TotalRecordCount
Total count of machines in the given site.

```yaml
Type: System.Int32
Parameter Sets: List
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

### Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api202001.IVMwareMachine

## NOTES

ALIASES

## RELATED LINKS

