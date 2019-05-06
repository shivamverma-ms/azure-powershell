---
external help file: Microsoft.Azure.PowerShell.Cmdlets.RecoveryServices.dll-help.xml
Module Name: Az.RecoveryServices
online version: https://docs.microsoft.com/en-us/powershell/module/az.recoveryservices/set-azrecoveryservicesbackupproperties
schema: 2.0.0
---

# Set-AzRecoveryServicesBackupProperties

## SYNOPSIS
Sets the properties for backup management.

## SYNTAX

## DESCRIPTION
The Set-AzRecoveryServicesBackupProperties cmdlet sets backup storage properties for a Recovery Services vault.

## EXAMPLES

### Example 1: Set GeoRedundant storage for a vault
```
PS C:\> $Vault01 = Get-AzRecoveryServicesVault -Name "TestVault"
PS C:\> Set-AzRecoveryServicesBackupProperties -Vault $Vault01 -BackupStorageRedundancy GeoRedundant
```

The first command gets the vault named TestVault, and then stores it in the $Vault01 variable.
The second command sets the backup storage redundancy for $Vault01 to GeoRedundant.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Microsoft.Azure.Commands.RecoveryServices.ARSVault
## OUTPUTS

### System.Void
## NOTES

## RELATED LINKS

[Get-AzRecoveryServicesBackupProperties]()

[Get-AzRecoveryServicesVault]()

