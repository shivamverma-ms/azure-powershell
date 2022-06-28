@{
  GUID = '44399abf-8480-4ed2-a38b-65e3e90c1fee'
  RootModule = './Az.DataReplication.psm1'
  ModuleVersion = '0.1.0'
  CompatiblePSEditions = 'Core', 'Desktop'
  Author = 'Microsoft Corporation'
  CompanyName = 'Microsoft Corporation'
  Copyright = 'Microsoft Corporation. All rights reserved.'
  Description = 'Microsoft Azure PowerShell: DataReplication cmdlets'
  PowerShellVersion = '5.1'
  DotNetFrameworkVersion = '4.7.2'
  RequiredAssemblies = './bin/Az.DataReplication.private.dll'
  FormatsToProcess = './Az.DataReplication.format.ps1xml'
  FunctionsToExport = 'Get-AzDataReplicationDra', 'Get-AzDataReplicationEvent', 'Get-AzDataReplicationExtension', 'Get-AzDataReplicationFabric', 'Get-AzDataReplicationJob', 'Get-AzDataReplicationMachine', 'Get-AzDataReplicationPolicy', 'Get-AzDataReplicationProtectedItem', 'Get-AzDataReplicationRecoveryPoint', 'Get-AzDataReplicationRunAsAccount', 'Get-AzDataReplicationSite', 'Get-AzDataReplicationVault', 'Get-AzDataReplicationVaultStatistics', 'Get-AzDataReplicationVCenter', 'New-AzDataReplicationPolicy', 'New-AzDataReplicationProtectedItem', 'New-AzDataReplicationVault', 'New-AzDataReplicationVMwareToAvsFailbackPolicyModelCustomPropertiesObject', 'New-AzDataReplicationVMwareToAvsFailbackProtectedItemModelCustomPropertiesObject', 'New-AzDataReplicationVMwareToAvsPolicyModelCustomPropertiesObject', 'Remove-AzDataReplicationPolicy', 'Remove-AzDataReplicationProtectedItem', 'Remove-AzDataReplicationVault', 'Start-AzDataReplicationApplyRecoveryPoint', 'Start-AzDataReplicationCommitFailoverJob', 'Start-AzDataReplicationPlannedFailoverJob', 'Start-AzDataReplicationReprotectionJob', 'Start-AzDataReplicationResynchronizeJob', 'Start-AzDataReplicationTestFailoverCleanupJob', 'Start-AzDataReplicationTestFailoverJob', 'Start-AzDataReplicationUnplannedFailoverJob', 'Stop-AzDataReplicationProtectedItemFailover', 'Update-AzDataReplicationPolicy', 'Update-AzDataReplicationProtectedItem', 'Update-AzDataReplicationVault', '*'
  AliasesToExport = '*'
  PrivateData = @{
    PSData = @{
      Tags = 'Azure', 'ResourceManager', 'ARM', 'PSModule', 'DataReplication'
      LicenseUri = 'https://aka.ms/azps-license'
      ProjectUri = 'https://github.com/Azure/azure-powershell'
      ReleaseNotes = ''
    }
  }
}
