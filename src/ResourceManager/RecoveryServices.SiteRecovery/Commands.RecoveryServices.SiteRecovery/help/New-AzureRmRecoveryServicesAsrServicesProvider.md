---
external help file: Microsoft.Azure.Commands.RecoveryServices.SiteRecovery.dll-Help.xml
online version: 
schema: 2.0.0
---

# New-AzureRmRecoveryServicesAsrServicesProvider

## SYNOPSIS
Creates an Azure Site Recovery Services Provider.

## SYNTAX

```
New-AzureRmRecoveryServicesAsrServicesProvider -Name <String> -Fabric <ASRFabric> [-CertValue <String>]
 [-WhatIf] [-Confirm]
```

## DESCRIPTION
The **New-AzureRmRecoveryServicesAsrServicesProvider** cmdlet creates an Azure Site Recovery Services Provider under the specified fabric.

## EXAMPLES

### Example 1
```
PS C:\> $currentJob = New-AzureRmRecoveryServicesAsrServicesProvider -Name $providerNameName -Fabric $fabric
```

Starts the recovery services provider creation with passed name and returns the ASR job used to track the creation operation.


## PARAMETERS

### -CertValue
The value of the "asymmetric" credential type.
It represents the base 64 encoded certificate.

```yaml
Type: String
Parameter Sets: (All)
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
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Fabric
The fabric under which the recovery services provider is to be created.

```yaml
Type: ASRFabric
Parameter Sets: (All)
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
Name of the recovery services provider to be created

```yaml
Type: String
Parameter Sets: (All)
Aliases: 

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs.
The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## INPUTS

### None


## OUTPUTS

### Microsoft.Azure.Commands.RecoveryServices.SiteRecovery.ASRJob


## NOTES

## RELATED LINKS

