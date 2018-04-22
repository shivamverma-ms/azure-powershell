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

using Xunit;
using Xunit.Abstractions;
using Microsoft.Azure.ServiceManagemenet.Common.Models;
using Microsoft.WindowsAzure.Commands.ScenarioTest;


namespace RecoveryServices.SiteRecovery.Test
{
    public class AsrA2ATests
    {
        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestNewA2ADiskReplicationConfig()
        {
            AsrA2ATestController.NewInstance.RunPsTest("Test-NewA2ADiskReplicationConfiguration");
        }


        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestNewA2AManagedDiskReplicationConfig()
        {
            AsrA2ATestController.NewInstance.RunPsTest("Test-NewA2AManagedDiskReplicationConfiguration");
        }
        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void A2ANewAsrFabric()
        {
            AsrA2ATestController.NewInstance.RunPsTest("Test-NewAsrFabric");
        }


        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void A2ATestNewContainer()
        {
            AsrA2ATestController.NewInstance.RunPsTest("Test-NewContainer");
        }


        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void A2ATestNetworkMapping()
        {
            AsrA2ATestController.NewInstance.RunPsTest("Test-NetworkMapping");
        }

    }
}