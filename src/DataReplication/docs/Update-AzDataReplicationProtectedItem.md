---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.datareplication/update-azdatareplicationprotecteditem
schema: 2.0.0
---

# Update-AzDataReplicationProtectedItem

## SYNOPSIS


## SYNTAX

### ByProtectedItemId (Default)
```
Update-AzDataReplicationProtectedItem -ProtectedItemId <String> [-SubscriptionId <String>]
 [-ApplianceId <String>] [-DisksToInclude <Array>] [-DiskType <String>] [-FabricDiscoveryMachineId <String>]
 [-InstanceType <String>] [-LogStorageAccountId <String>] [-MultiVMGroupName <String>] [-PolicyName <String>]
 [-ReplicationExtensionName <String>] [-RunAsAccountId <String>] [-TargetAvsCloudId <String>]
 [-TargetAvsClusterName <String>] [-TargetCPUCores <Int32>] [-TargetDatastoreId <String>]
 [-TargetDiskPoolSubnetId <String>] [-TargetMemoryInMB <Int32>] [-TargetNetworkId <String>]
 [-TargetResourceGroupId <String>] [-TargetVCenterId <String>] [-TargetVMName <String>]
 [-TestNetworkId <String>] [-DefaultProfile <PSObject>] [-AsJob] [-NoWait] [<CommonParameters>]
```

### ByInputObject
```
Update-AzDataReplicationProtectedItem -InputObject <IProtectedItemModel> [-SubscriptionId <String>]
 [-ApplianceId <String>] [-DisksToInclude <Array>] [-DiskType <String>] [-FabricDiscoveryMachineId <String>]
 [-InstanceType <String>] [-LogStorageAccountId <String>] [-MultiVMGroupName <String>] [-PolicyName <String>]
 [-ReplicationExtensionName <String>] [-RunAsAccountId <String>] [-TargetAvsCloudId <String>]
 [-TargetAvsClusterName <String>] [-TargetCPUCores <Int32>] [-TargetDatastoreId <String>]
 [-TargetDiskPoolSubnetId <String>] [-TargetMemoryInMB <Int32>] [-TargetNetworkId <String>]
 [-TargetResourceGroupId <String>] [-TargetVCenterId <String>] [-TargetVMName <String>]
 [-TestNetworkId <String>] [-DefaultProfile <PSObject>] [-AsJob] [-NoWait] [<CommonParameters>]
```

## DESCRIPTION


## EXAMPLES

### Example 1: "ByProtectedItemID"

```powershell
New-AzDataReplicationProtectedItem -ProtectedItemId "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air/providers/Microsoft.DataReplication/replicationVaults/protectedItems/avsvcenter_50153f67-367e-440b-69a6-20870cec95a5" -PolicyName 24-hour-replication-policy -ReplicationExtensionName vmware-avs-replication-extension -InstanceType "VMwareToAvs" -TargetAvsCloudId "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air/providers/Microsoft.AVS/privateClouds/avs-dr-cloud-cnc" -TargetAvsClusterName "Cluster-1" -FabricDiscoveryMachineId "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air/providers/Microsoft.OffAzure/VMwareSites/vijamijun18b3evmwaresite/machines/avsvcenter_5015355d-7426-885a-87e6-24ecf766eb6d" -LogStorageAccountId "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air/providers/Microsoft.Storage/storageAccounts/avssa" -DiskType "Premium_LRS" -TargetVMName "vijamiwinvm1" -TargetResourceGroupId "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air" -TargetDatastoreId "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-air/providers/Microsoft.ConnectedVMwarevSphere/Datastores/avs-dr-cnc-datastore" -TargetVCenterId "/subscriptions/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/resourceGroups/Arpita-Air/providers/Microsoft.ConnectedVMwarevSphere/vcenters/avs-dr-cloud-cnc-vcenter"
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

New-AzDataReplicationProtectedItem -PolicyName "24-hour-replication-policy" -ReplicationExtensionName "vmware-avs-replication-extension" -CustomProperty $vmSpecificProperties -InputObject $InputObject
```

```output
{{ Add output here }}
```

{{ Add description here }}

## PARAMETERS

### -ApplianceId


```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
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
Parameter Sets: (All)
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
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FabricDiscoveryMachineId


```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject
To construct, see NOTES section for INPUTOBJECT properties and create a hash table.

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IProtectedItemModel
Parameter Sets: ByInputObject
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
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LogStorageAccountId


```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MultiVMGroupName


```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
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

### -PolicyName


```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ProtectedItemId


```yaml
Type: System.String
Parameter Sets: ByProtectedItemId
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

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RunAsAccountId


```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
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
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetAvsClusterName


```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetCPUCores


```yaml
Type: System.Int32
Parameter Sets: (All)
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
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetDiskPoolSubnetId


```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetMemoryInMB


```yaml
Type: System.Int32
Parameter Sets: (All)
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
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetResourceGroupId


```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetVCenterId


```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetVMName


```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TestNetworkId


```yaml
Type: System.String
Parameter Sets: (All)
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

### System.Object

## NOTES

ALIASES

COMPLEX PARAMETER PROPERTIES

To create the parameters described below, construct a hash table containing the appropriate properties. For information on hash tables, run Get-Help about_Hash_Tables.


INPUTOBJECT <IProtectedItemModel>: 
  - `[CustomProperty <IProtectedItemModelCustomProperties>]`: Protected item model custom properties.
    - `InstanceType <String>`: Gets or sets the instance type.
  - `[PolicyName <String>]`: Gets or sets the policy name.
  - `[ReplicationExtensionName <String>]`: Gets or sets the replication extension name.
  - `[SystemDataCreatedAt <DateTime?>]`: Gets or sets the timestamp of resource creation (UTC).
  - `[SystemDataCreatedBy <String>]`: Gets or sets identity that created the resource.
  - `[SystemDataCreatedByType <String>]`: Gets or sets the type of identity that created the resource: user, application,         managedIdentity.
  - `[SystemDataLastModifiedAt <DateTime?>]`: Gets or sets the timestamp of resource last modification (UTC).
  - `[SystemDataLastModifiedBy <String>]`: Gets or sets the identity that last modified the resource.
  - `[SystemDataLastModifiedByType <String>]`: Gets or sets the type of identity that last modified the resource: user, application,         managedIdentity.
  - `[Tag <IProtectedItemModelTags>]`: Gets or sets the resource tags.
    - `[(Any) <String>]`: This indicates any property can be added to this object.

## RELATED LINKS

