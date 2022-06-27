if(($null -eq $TestName) -or ($TestName -contains 'Get-AzDataReplicationProtectedItem'))
{
  $loadEnvPath = Join-Path $PSScriptRoot 'loadEnv.ps1'
  if (-Not (Test-Path -Path $loadEnvPath)) {
      $loadEnvPath = Join-Path $PSScriptRoot '..\loadEnv.ps1'
  }
  . ($loadEnvPath)
  $TestRecordingFile = Join-Path $PSScriptRoot 'Get-AzDataReplicationProtectedItem.Recording.json'
  $currentPath = $PSScriptRoot
  while(-not $mockingPath) {
      $mockingPath = Get-ChildItem -Path $currentPath -Recurse -Include 'HttpPipelineMocking.ps1' -File
      $currentPath = Split-Path -Path $currentPath -Parent
  }
  . ($mockingPath | Select-Object -First 1).FullName
}

Describe 'Get-AzDataReplicationProtectedItem' {
    It 'List' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }

    It 'GetByProtectedItemName' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }

    It 'GetByProtectedItemId' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }

    It 'GetByInputObject' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }
}
