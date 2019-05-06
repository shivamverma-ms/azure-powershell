---
external help file: Microsoft.Azure.PowerShell.Cmdlets.RecoveryServices.Backup.dll-help.xml
Module Name: Az.RecoveryServices
online version: https://docs.microsoft.com/en-us/powershell/module/az.recoveryservices/get-azrecoveryservicesbackupjobdetails
schema: 2.0.0
---

# Get-AzRecoveryServicesBackupJobDetails

## SYNOPSIS
Gets details for a Backup job.

## SYNTAX

## DESCRIPTION
The Get-AzRecoveryServicesBackupJobDetails cmdlet gets Azure Backup job details for a specified job.
Set the vault context by using the Set-AzRecoveryServicesVaultContext cmdlet before you use the current cmdlet.

## EXAMPLES

### Example 1: Get Backup job details for failed jobs
```
PS C:\>$Jobs = Get-AzRecoveryServicesBackupJob -Status Failed
PS C:\> $JobDetails = Get-AzRecoveryServicesBackupJobDetails -Job $Jobs[0]
PS C:\> $JobDetails.ErrorDetails
```

The first command gets an array of failed jobs in the vault, and then stores them in the $Jobs array.
The second command gets the job details for the failed jobs in $Jobs, and then stores them in the $JobDetails variable.
The final command displays error details for the failed jobs.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String
## OUTPUTS

### Microsoft.Azure.Commands.RecoveryServices.Backup.Cmdlets.Models.JobBase
## NOTES

## RELATED LINKS

[Get-AzRecoveryServicesBackupJob]()

