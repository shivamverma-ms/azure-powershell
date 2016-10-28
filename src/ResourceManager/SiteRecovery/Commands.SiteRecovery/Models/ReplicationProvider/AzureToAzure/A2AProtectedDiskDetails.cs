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

using SDK = Microsoft.Azure.Management.SiteRecovery.Models;

namespace Microsoft.Azure.Commands.SiteRecovery.Models.ReplicationProvider
{
    /// <summary>
    /// AzureToAzure replication provider specific protected disk details.
    /// </summary>
    public class A2AProtectedDiskDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="A2AProtectedDiskDetails" />
        /// class.
        /// </summary>
        public A2AProtectedDiskDetails()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="A2AProtectedDiskDetails" />
        /// class.
        /// </summary>
        public A2AProtectedDiskDetails(SDK.A2AProtectedDiskDetails disk)
        {
            this.DiskUri = disk.DiskUri;
            this.PrimaryDiskAzureStorageAccountId = disk.PrimaryDiskAzureStorageAccountId;
            this.PrimaryStagingAzureStorageAccountId = disk.PrimaryStagingAzureStorageAccountId;
            this.RecoveryAzureStorageAccountId = disk.RecoveryAzureStorageAccountId;
            this.RecoveryDiskUri = disk.RecoveryDiskUri;
        }

        /// <summary>
        /// Gets or sets the disk uri.
        /// </summary>
        public string DiskUri { get; set; }

        /// <summary>
        /// Gets or sets the primary disk storage account. 
        /// </summary>
        public string PrimaryDiskAzureStorageAccountId { get; set; }

        /// <summary>
        /// Gets or sets the primary staging storage account.
        /// </summary>
        public string PrimaryStagingAzureStorageAccountId { get; set; }

        /// <summary>
        /// Gets or sets the recovery disk storage account. 
        /// </summary>
        public string RecoveryAzureStorageAccountId { get; set; }

        /// <summary>
        /// Gets or sets recovery disk uri.
        /// </summary>
        public string RecoveryDiskUri { get; set; }
    }
}
