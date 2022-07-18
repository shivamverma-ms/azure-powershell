---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.datareplication/get-azdatareplicationjob
schema: 2.0.0
---

# Get-AzDataReplicationJob

## SYNOPSIS
Gets the details of the job.

## SYNTAX

### List (Default)
```
Get-AzDataReplicationJob -ResourceGroupName <String> -VaultName <String> [-SubscriptionId <String[]>]
 [-ContinuationToken <String>] [-Filter <String>] [-DefaultProfile <PSObject>] [<CommonParameters>]
```

### Get
```
Get-AzDataReplicationJob -Name <String> -ResourceGroupName <String> -VaultName <String>
 [-SubscriptionId <String[]>] [-DefaultProfile <PSObject>] [<CommonParameters>]
```

## DESCRIPTION
Gets the details of the job.

## EXAMPLES

### Example 1: Get
```powershell
Get-AzDataReplicationJob -ResourceGroupName arpita-air -VaultName vijamijun15 -Name b380d4a5-b1bd-44c9-a64e-27ef6476f01f | fl
```

```output
ActivityId                         :  ActivityId: 00000000-0000-0000-0000-000000000000
AllowedAction                      : {}
CustomPropertyAffectedObjectDetail : Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.WorkflowModelCustomPropertiesAffectedObjectDetails
CustomPropertyInstanceType         : WorkflowDetails
DisplayName                        : Create or update replication policy
EndTime                            : 7/7/2022 10:42:24 AM
Error                              : {}
Id                                 : /subscriptions/b364ed8d-4279-4bf8-8fd1-56f8fa0ae05c/resourceGroups/arpita-air/providers/Microsoft.DataReplication/replicationVaults/vijamijun15/jobs/b380d4a5-b1bd-44c9-a64e-27ef6476f01f
Name                               : b380d4a5-b1bd-44c9-a64e-27ef6476f01f
ObjectId                           : /subscriptions/b364ed8d-4279-4bf8-8fd1-56f8fa0ae05c/resourceGroups/arpita-air/providers/Microsoft.DataReplication/replicationVaults/vijamijun15/replicationPolicies/test-policy
ObjectInternalId                   : test-policy
ObjectInternalName                 : test-policy
ObjectName                         : test-policy
ObjectType                         : Policy
ReplicationProviderId              : a77470e3-0ef4-4fbf-a5a4-18b47e7048b3
SourceFabricProviderId             :
StartTime                          : 7/7/2022 10:42:22 AM
State                              : Succeeded
SystemDataCreatedAt                :
SystemDataCreatedBy                :
SystemDataCreatedByType            :
SystemDataLastModifiedAt           :
SystemDataLastModifiedBy           :
SystemDataLastModifiedByType       :
Tag                                : Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.WorkflowModelTags
TargetFabricProviderId             :
Task                               : {Creating or updating the replication policy}
Type                               : Microsoft.DataReplication/replicationVaults/jobs
```

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
Workflow name.

```yaml
Type: System.String
Parameter Sets: Get
Aliases: WorkflowName

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

### Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IWorkflowModel

## NOTES

ALIASES

## RELATED LINKS

