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

using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Management.SiteRecovery.Models;

namespace Microsoft.Azure.Commands.SiteRecovery.Models.ReplicationProtectedItem
{
    /// <summary>
    /// Azure to Azure VM synced configuration details.
    /// </summary>
    public class ASRAzureToAzureVmSyncedConfigDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ASRAzureToAzureVmSyncedConfigDetails" />
        /// class.
        /// </summary>
        public ASRAzureToAzureVmSyncedConfigDetails()
        {
            this.Tags = new Dictionary<string, string>();
            this.RoleAssignments = new List<ASRRoleAssignment>();
            this.InputEndpoints = new List<ASRInputEndpoint>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ASRAzureToAzureVmSyncedConfigDetails" />
        /// class.
        /// </summary>
        public ASRAzureToAzureVmSyncedConfigDetails(AzureToAzureVmSyncedConfigDetails details)
        {
            this.Tags = new Dictionary<string, string>(details.Tags);
            this.RoleAssignments =
                details.RoleAssignments.ToList()
                .ConvertAll(role => new ASRRoleAssignment(role));
            this.InputEndpoints =
                details.InputEndpoints.ToList()
                .ConvertAll(endpoint => new ASRInputEndpoint(endpoint));
        }

        /// <summary>
        /// Gets or sets the Azure VM tags.
        /// </summary>
        public Dictionary<string, string> Tags { get; set; }

        /// <summary>
        /// Gets or sets the Azure role assignments.
        /// </summary>
        public List<ASRRoleAssignment> RoleAssignments { get; set; }

        /// <summary>
        /// Gets or sets the Azure VM input endpoints.
        /// </summary>
        public List<ASRInputEndpoint> InputEndpoints { get; set; }
    }
}
