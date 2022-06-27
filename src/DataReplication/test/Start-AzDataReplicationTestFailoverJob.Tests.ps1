if(($null -eq $TestName) -or ($TestName -contains 'Start-AzDataReplicationTestFailoverJob'))
{
  $loadEnvPath = Join-Path $PSScriptRoot 'loadEnv.ps1'
  if (-Not (Test-Path -Path $loadEnvPath)) {
      $loadEnvPath = Join-Path $PSScriptRoot '..\loadEnv.ps1'
  }
  . ($loadEnvPath)
  $TestRecordingFile = Join-Path $PSScriptRoot 'Start-AzDataReplicationTestFailoverJob.Recording.json'
  $currentPath = $PSScriptRoot
  while(-not $mockingPath) {
      $mockingPath = Get-ChildItem -Path $currentPath -Recurse -Include 'HttpPipelineMocking.ps1' -File
      $currentPath = Split-Path -Path $currentPath -Parent
  }
  . ($mockingPath | Select-Object -First 1).FullName
}

Describe 'Start-AzDataReplicationTestFailoverJob' {
    It 'ByProtectedItemId' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }

    It 'ByProtectedItemIdExpanded' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }

    It 'ByInputObject' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }

    It 'ByInputObjectExpanded' -skip {
        { throw [System.NotImplementedException] } | Should -Not -Throw
    }
}
