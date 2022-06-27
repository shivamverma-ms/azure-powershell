---
external help file:
Module Name: Az.DataReplication
online version: https://docs.microsoft.com/powershell/module/az.datareplication/set-azdatareplicationdra
schema: 2.0.0
---

# Set-AzDataReplicationDra

## SYNOPSIS
Refreshes the fabric agent.

## SYNTAX

### Refresh (Default)
```
Set-AzDataReplicationDra -FabricName <String> -Name <String> -ResourceGroupName <String>
 [-SubscriptionId <String>] [-DefaultProfile <PSObject>] [-AsJob] [-NoWait] [-PassThru] [-Confirm] [-WhatIf]
 [<CommonParameters>]
```

### Update
```
Set-AzDataReplicationDra -FabricName <String> -Name <String> -ResourceGroupName <String> -Body <IDraModel>
 [-SubscriptionId <String>] [-DefaultProfile <PSObject>] [-AsJob] [-NoWait] [-Confirm] [-WhatIf]
 [<CommonParameters>]
```

### UpdateExpanded
```
Set-AzDataReplicationDra -FabricName <String> -Name <String> -ResourceGroupName <String>
 [-SubscriptionId <String>] [-AuthenticationIdentityAadAuthority <String>]
 [-AuthenticationIdentityApplicationId <String>] [-AuthenticationIdentityAudience <String>]
 [-AuthenticationIdentityObjectId <String>] [-AuthenticationIdentityTenantId <String>]
 [-CustomPropertyInstanceType <String>] [-MachineId <String>] [-MachineName <String>]
 [-ResourceAccessIdentityAadAuthority <String>] [-ResourceAccessIdentityApplicationId <String>]
 [-ResourceAccessIdentityAudience <String>] [-ResourceAccessIdentityObjectId <String>]
 [-ResourceAccessIdentityTenantId <String>] [-Tag <Hashtable>] [-DefaultProfile <PSObject>] [-AsJob] [-NoWait]
 [-Confirm] [-WhatIf] [<CommonParameters>]
```

## DESCRIPTION
Refreshes the fabric agent.

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

### -AsJob
Run the command as a job

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

### -AuthenticationIdentityAadAuthority
Gets or sets the authority of the SPN with which Dra communicates to service.

```yaml
Type: System.String
Parameter Sets: UpdateExpanded
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AuthenticationIdentityApplicationId
Gets or sets the client/application Id of the SPN with which Dra communicates toservice.

```yaml
Type: System.String
Parameter Sets: UpdateExpanded
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AuthenticationIdentityAudience
Gets or sets the audience of the SPN with which Dra communicates to service.

```yaml
Type: System.String
Parameter Sets: UpdateExpanded
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AuthenticationIdentityObjectId
Gets or sets the object Id of the SPN with which Dra communicates to service.

```yaml
Type: System.String
Parameter Sets: UpdateExpanded
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AuthenticationIdentityTenantId
Gets or sets the tenant Id of the SPN with which Dra communicates to service.

```yaml
Type: System.String
Parameter Sets: UpdateExpanded
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Body
Dra model.
To construct, see NOTES section for BODY properties and create a hash table.

```yaml
Type: Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IDraModel
Parameter Sets: Update
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -CustomPropertyInstanceType
Gets or sets the instance type.

```yaml
Type: System.String
Parameter Sets: UpdateExpanded
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

### -MachineId
Gets or sets the machine Id where Dra is running.

```yaml
Type: System.String
Parameter Sets: UpdateExpanded
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MachineName
Gets or sets the machine name where Dra is running.

```yaml
Type: System.String
Parameter Sets: UpdateExpanded
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
Dra name.

```yaml
Type: System.String
Parameter Sets: (All)
Aliases: DraName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoWait
Run the command asynchronously

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
Returns true when the command succeeds

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: Refresh
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceAccessIdentityAadAuthority
Gets or sets the authority of the SPN with which Dra communicates to service.

```yaml
Type: System.String
Parameter Sets: UpdateExpanded
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceAccessIdentityApplicationId
Gets or sets the client/application Id of the SPN with which Dra communicates toservice.

```yaml
Type: System.String
Parameter Sets: UpdateExpanded
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceAccessIdentityAudience
Gets or sets the audience of the SPN with which Dra communicates to service.

```yaml
Type: System.String
Parameter Sets: UpdateExpanded
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceAccessIdentityObjectId
Gets or sets the object Id of the SPN with which Dra communicates to service.

```yaml
Type: System.String
Parameter Sets: UpdateExpanded
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceAccessIdentityTenantId
Gets or sets the tenant Id of the SPN with which Dra communicates to service.

```yaml
Type: System.String
Parameter Sets: UpdateExpanded
Aliases:

Required: False
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
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: (Get-AzContext).Subscription.Id
Accept pipeline input: False
Accept wildcard characters: False
```

### -Tag
Gets or sets the resource tags.

```yaml
Type: System.Collections.Hashtable
Parameter Sets: UpdateExpanded
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs.
The cmdlet is not run.

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IDraModel

## OUTPUTS

### Microsoft.Azure.PowerShell.Cmdlets.DataReplication.Models.Api20210216Preview.IDraModel

### System.Boolean

## NOTES

ALIASES

COMPLEX PARAMETER PROPERTIES

To create the parameters described below, construct a hash table containing the appropriate properties. For information on hash tables, run Get-Help about_Hash_Tables.


BODY <IDraModel>: Dra model.
  - `[AuthenticationIdentityAadAuthority <String>]`: Gets or sets the authority of the SPN with which Dra communicates to service.
  - `[AuthenticationIdentityApplicationId <String>]`: Gets or sets the client/application Id of the SPN with which Dra communicates to         service.
  - `[AuthenticationIdentityAudience <String>]`: Gets or sets the audience of the SPN with which Dra communicates to service.
  - `[AuthenticationIdentityObjectId <String>]`: Gets or sets the object Id of the SPN with which Dra communicates to service.
  - `[AuthenticationIdentityTenantId <String>]`: Gets or sets the tenant Id of the SPN with which Dra communicates to service.
  - `[CustomPropertyInstanceType <String>]`: Gets or sets the instance type.
  - `[MachineId <String>]`: Gets or sets the machine Id where Dra is running.
  - `[MachineName <String>]`: Gets or sets the machine name where Dra is running.
  - `[ResourceAccessIdentityAadAuthority <String>]`: Gets or sets the authority of the SPN with which Dra communicates to service.
  - `[ResourceAccessIdentityApplicationId <String>]`: Gets or sets the client/application Id of the SPN with which Dra communicates to         service.
  - `[ResourceAccessIdentityAudience <String>]`: Gets or sets the audience of the SPN with which Dra communicates to service.
  - `[ResourceAccessIdentityObjectId <String>]`: Gets or sets the object Id of the SPN with which Dra communicates to service.
  - `[ResourceAccessIdentityTenantId <String>]`: Gets or sets the tenant Id of the SPN with which Dra communicates to service.
  - `[SystemDataCreatedAt <DateTime?>]`: Gets or sets the timestamp of resource creation (UTC).
  - `[SystemDataCreatedBy <String>]`: Gets or sets identity that created the resource.
  - `[SystemDataCreatedByType <String>]`: Gets or sets the type of identity that created the resource: user, application,         managedIdentity.
  - `[SystemDataLastModifiedAt <DateTime?>]`: Gets or sets the timestamp of resource last modification (UTC).
  - `[SystemDataLastModifiedBy <String>]`: Gets or sets the identity that last modified the resource.
  - `[SystemDataLastModifiedByType <String>]`: Gets or sets the type of identity that last modified the resource: user, application,         managedIdentity.
  - `[Tag <IDraModelTags>]`: Gets or sets the resource tags.
    - `[(Any) <String>]`: This indicates any property can be added to this object.

## RELATED LINKS

