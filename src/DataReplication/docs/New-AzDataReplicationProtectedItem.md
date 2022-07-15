---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.datareplication/new-azdatareplicationprotecteditem
schema: 2.0.0
---

# New-AzDataReplicationProtectedItem

## SYNOPSIS
Enables replication for a VM on VMware.

## SYNTAX

### ByProperties (Default)

```
New-AzDataReplicationProtectedItem -Name <String> -ResourceGroupName <String> -VaultName <String>
 -ApplianceId <String> -DiskType <String> -FabricDiscoveryMachineId <String> -InstanceType <String>
 -LogStorageAccountId <String> -PolicyName <String> -ReplicationExtensionName <String>
 -RunAsAccountId <String> -TargetAvsCloudId <String> -TargetAvsClusterName <String>
 -TargetDatastoreId <String> -TargetDiskPoolSubnetId <String> -TargetNetworkId <String>
 -TargetResourceGroupId <String> -TargetVCenterId <String> -TargetVMName <String> -TestNetworkId <String>
 [-SubscriptionId <String>] [-DisksToInclude <Array>] [-MultiVMGroupName <String>] [-TargetCPUCores <Int32>]
 [-TargetMemoryInMB <Int32>] [-DefaultProfile <PSObject>] [-AsJob] [-NoWait] [-PassThru] [<CommonParameters>]
```

### ByCustomPropertyObject

```
New-AzDataReplicationProtectedItem -Name <String> -ResourceGroupName <String> -VaultName <String>
 -CustomProperty <IProtectedItemModelCustomProperties> -PolicyName <String> -ReplicationExtensionName <String>
 [-SubscriptionId <String>] [-DefaultProfile <PSObject>] [-AsJob] [-NoWait] [-PassThru] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### Example 1: "ByProperties"

```powershell
New-AzDataReplicationProtectedItem -ResourceGroupName arpita-air -Name "avsvcenter_5015355d-7426-885a-87e6-24ecf766eb6d" -VaultName vijamijun15 -PolicyName 24-hour-replication-policy -ReplicationExtensionName vmware-avs-replication-extension -InstanceType "VMwareToAvs" -TargetAvsCloudId "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air/providers/Microsoft.AVS/privateClouds/avs-dr-cloud-cnc" -TargetAvsClusterName "Cluster-1" -FabricDiscoveryMachineId "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air/providers/Microsoft.OffAzure/VMwareSites/vijamijun18b3evmwaresite/machines/avsvcenter_5015355d-7426-885a-87e6-24ecf766eb6d" -LogStorageAccountId "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air/providers/Microsoft.Storage/storageAccounts/avssa" -DiskType "Premium_LRS" -TargetVMName "vijamiwinvm1" -TargetResourceGroupId "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air" -TargetDatastoreId "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air/providers/Microsoft.ConnectedVMwarevSphere/Datastores/avs-dr-cnc-datastore" -TargetVCenterId "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-Air/providers/Microsoft.ConnectedVMwarevSphere/vcenters/avs-dr-cloud-cnc-vcenter" -TargetNetworkId "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air/providers/Microsoft.ConnectedVMwarevSphere/VirtualNetworks/TNT62-HCX-UPLINK" -TargetDiskPoolSubnetId "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air/providers/Microsoft.Network/virtualNetworks/avs-dr-cloud-cnc-vnet/subnets/default" -TestNetworkId "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air/providers/Microsoft.ConnectedVMwarevSphere/VirtualNetworks/TNT62-HCX-UPLINK" -RunAsAccountId "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air/providers/Microsoft.OffAzure/VMwareSites/vijamijun18b3evmwaresite/runasaccounts/a3e52036-0cbf-59f5-bf0b-0e82dd6c69e9" -ApplianceId "b4d21e44-772b-421b-8c83-1bb7e268d00a"
```

```output
{{ Add output here }}
```

{{ Add description here }}

### Example 2: ByCustomPropertyObject

```powershell
$vmSpecificProperties = [Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.VMwareToAvsProtectedItemModelCustomProperties]::new()
$vmSpecificProperties.InstanceType = "VMwareToAvs"
$vmSpecificProperties.TargetAvsCloudId = "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air/providers/Microsoft.AVS/privateClouds/avs-dr-cloud-cnc"
$vmSpecificProperties.TargetAvsClusterName = "Cluster-1"
$vmSpecificProperties.FabricDiscoveryMachineId = "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air/providers/Microsoft.OffAzure/VMwareSites/avsmay23c242vmwaresite/machines/avsvcenter_50153f67-367e-440b-69a6-20870cec95a5"
$vmSpecificProperties.DisksDefault.logStorageAccountId = "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air/providers/Microsoft.Storage/storageAccounts/avssa"
$vmSpecificProperties.DisksDefault.diskType = "Premium_LRS"
$vmSpecificProperties.TargetVMName = "vijamiWinVm3"
$vmSpecificProperties.TargetResourceGroupId = "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air"
$vmSpecificProperties.TargetVCenterId = "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-Air/providers/Microsoft.ConnectedVMwarevSphere/vcenters/avs-dr-cloud-cnc-vcenter"
$vmSpecificProperties.TargetDatastoreId = "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air/providers/Microsoft.ConnectedVMwarevSphere/Datastores/avs-dr-cnc-datastore"
$vmSpecificProperties.TargetNetworkId = "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air/providers/Microsoft.ConnectedVMwarevSphere/VirtualNetworks/TNT62-HCX-UPLINK"
$vmSpecificProperties.TargetDiskPoolSubnetId = "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air/providers/Microsoft.Network/virtualNetworks/avs-dr-cloud-cnc-vnet/subnets/default"
$vmSpecificProperties.TestNetworkId = "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air/providers/Microsoft.ConnectedVMwarevSphere/VirtualNetworks/TNT62-HCX-UPLINK"
$vmSpecificProperties.RunAsAccountId = "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/arpita-air/providers/Microsoft.OffAzure/VMwareSites/avsmay23c242vmwaresite/runasaccounts/48e8ddc4-4425-5401-9e1b-74429c02f967"
$vmSpecificProperties.ApplianceId = "e2d294c3-34da-45b1-b120-22217f6ca4b3"

New-AzDataReplicationProtectedItem -PolicyName "24-hour-replication-policy" -ReplicationExtensionName "vmware-avs-replication-extension" -CustomProperty $vmSpecificProperties -Name vijamiWinVm3 -ResourceGroupName arpita-air -VaultName avsmay23
```

```output
{{ Add output here }}
```

{{ Add description here }}

## PARAMETERS

### -ApplianceId

```yaml
Type: System.String
Parameter Sets: ByProperties
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -CustomProperty

To construct, see NOTES section for CUSTOMPROPERTY properties and create a hash table.

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IProtectedItemModelCustomProperties
Parameter Sets: ByCustomPropertyObject
Aliases:

Required: True
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

### -DisksToInclude

```yaml
Type: System.Array
Parameter Sets: ByProperties
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DiskType

```yaml
Type: System.String
Parameter Sets: ByProperties
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FabricDiscoveryMachineId

```yaml
Type: System.String
Parameter Sets: ByProperties
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InstanceType

```yaml
Type: System.String
Parameter Sets: ByProperties
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LogStorageAccountId

```yaml
Type: System.String
Parameter Sets: ByProperties
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MultiVMGroupName

```yaml
Type: System.String
Parameter Sets: ByProperties
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name

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

### -PolicyName

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

### -ReplicationExtensionName

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

### -ResourceGroupName

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

### -RunAsAccountId

```yaml
Type: System.String
Parameter Sets: ByProperties
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

### -TargetAvsCloudId

```yaml
Type: System.String
Parameter Sets: ByProperties
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetAvsClusterName

```yaml
Type: System.String
Parameter Sets: ByProperties
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetCPUCores

```yaml
Type: System.Int32
Parameter Sets: ByProperties
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetDatastoreId

```yaml
Type: System.String
Parameter Sets: ByProperties
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetDiskPoolSubnetId

```yaml
Type: System.String
Parameter Sets: ByProperties
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetMemoryInMB

```yaml
Type: System.Int32
Parameter Sets: ByProperties
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetNetworkId

```yaml
Type: System.String
Parameter Sets: ByProperties
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetResourceGroupId

```yaml
Type: System.String
Parameter Sets: ByProperties
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetVCenterId

```yaml
Type: System.String
Parameter Sets: ByProperties
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetVMName

```yaml
Type: System.String
Parameter Sets: ByProperties
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TestNetworkId

```yaml
Type: System.String
Parameter Sets: ByProperties
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -VaultName

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

### System.Object

## NOTES

ALIASES

COMPLEX PARAMETER PROPERTIES

To create the parameters described below, construct a hash table containing the appropriate properties. For information on hash tables, run Get-Help about_Hash_Tables.

CUSTOMPROPERTY <IProtectedItemModelCustomProperties>:

- `InstanceType <String>`: Gets or sets the instance type.

## RELATED LINKS
