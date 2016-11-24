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

using Microsoft.Azure.Management.SiteRecovery.Models;

namespace Microsoft.Azure.Commands.SiteRecovery.Models.ReplicationProtectedItem
{
    /// <summary>
    /// Replication provider specific entity details.
    /// </summary>
    public class ASRReplicationProviderSpecificSettings
    {
        /// <summary>
        /// Initializes a new instance of the<see cref="ASRAzureToAzureReplicationDetails" /> class.
        /// </summary>
        public ASRReplicationProviderSpecificSettings()
        {
        }

        /// <summary>
        /// Initializes a new instance of the<see cref="ASRAzureToAzureReplicationDetails" /> class.
        /// </summary>
        public ASRReplicationProviderSpecificSettings(ReplicationProviderSpecificSettings settings)
        {
            this.InstanceType = settings.InstanceType;
        }

        /// <summary>
        /// Gets or sets the Instance type name.
        /// </summary>
        public string InstanceType { get; set; }
    }
}
