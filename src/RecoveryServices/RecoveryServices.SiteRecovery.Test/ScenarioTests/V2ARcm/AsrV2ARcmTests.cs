// ----------------------------------------------------------------------------------
// 
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using System.IO;
using Microsoft.Azure.ServiceManagement.Common.Models;
using Microsoft.WindowsAzure.Commands.ScenarioTest;
using Xunit;
using Xunit.Abstractions;

namespace RecoveryServices.SiteRecovery.Test
{
    public class AsrV2ARcmTests : AsrV2ARcmTestsBase
    {
        public XunitTracingInterceptor _logger;

        public AsrV2ARcmTests(
            ITestOutputHelper output)
        {
            _logger = new XunitTracingInterceptor(output);
            XunitTracingInterceptor.AddToContext(_logger);
            this.PowershellFile = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "ScenarioTests", "V2ARcm", "AsrV2ARcmTests.ps1");
            this.Initialize();
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestV2ARcmFabric()
        {
            this.RunPowerShellTest(_logger, Constants.NewModel, "Test-V2ARcmFabric");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestV2ARcmPolicy()
        {
            this.RunPowerShellTest(_logger, Constants.NewModel, "Test-V2ARcmPolicy");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestV2ARcmContainer()
        {
            this.RunPowerShellTest(_logger, Constants.NewModel, "Test-V2ARcmContainer");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestV2ARcmContainerMapping()
        {
            this.RunPowerShellTest(_logger, Constants.NewModel, "Test-V2ARcmContainerMapping");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestV2ARcmEnableDR()
        {
            this.RunPowerShellTest(_logger, Constants.NewModel, "Test-V2ARcmEnableDR");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestV2ARcmTestFailover()
        {
            this.RunPowerShellTest(_logger, Constants.NewModel, "Test-V2ARcmTestFailover");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestV2ARcmFailover()
        {
            this.RunPowerShellTest(_logger, Constants.NewModel, "Test-V2ARcmFailover");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestV2ARcmReprotect()
        {
            this.RunPowerShellTest(_logger, Constants.NewModel, "Test-V2ARcmReprotect");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestV2ARcmFailback()
        {
            this.RunPowerShellTest(_logger, Constants.NewModel, "Test-V2ARcmFailback");
        }
    }
}
