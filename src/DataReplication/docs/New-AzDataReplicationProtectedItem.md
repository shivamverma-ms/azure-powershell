---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.datareplication/new-azdatareplicationprotecteditem
schema: 2.0.0
---

# New-AzDataReplicationProtectedItem

## SYNOPSIS


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

### Example 1: {{ Add title here }}
```powershell
{{ Add code here }}
```

```output
{{ Add output here }}
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

