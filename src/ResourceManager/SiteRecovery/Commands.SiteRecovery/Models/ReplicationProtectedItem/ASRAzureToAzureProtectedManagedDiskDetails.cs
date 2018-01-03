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

namespace Microsoft.Azure.Commands.SiteRecovery.Models.ReplicationProtectedItem
{
    /// <summary>
    /// AzureToAzure replication provider specific protected disk details.
    /// </summary>
    public class ASRAzureToAzureProtectedManagedDiskDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ASRAzureToAzureProtectedManagedDiskDetails" />
        /// class.
        /// </summary>
        public ASRAzureToAzureProtectedManagedDiskDetails()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ASRAzureToAzureProtectedManagedDiskDetails" />
        /// class.
        /// </summary>
        public ASRAzureToAzureProtectedManagedDiskDetails(SDK.A2AProtectedManagedDiskDetails disk)
        {
            if (disk == null)
            {
                return;
            }

            this.DiskId = disk.DiskId;
            this.PrimaryDiskAzureStorageAccountId = disk.PrimaryStagingAzureStorageAccountId;
            this.PrimaryStagingAzureStorageAccountId = disk.PrimaryStagingAzureStorageAccountId;
            this.RecoveryResourceGroupId = disk.RecoveryResourceGroupId;
            this.RecoveryDiskId = disk.RecoveryDiskId;
            this.RecoveryReplicaDiskAccountType = disk.RecoveryReplicaDiskAccountType;
            this.RecoveryTargetDiskAccountType = disk.RecoveryTargetDiskAccountType;
            this.ResyncRequired = disk.ResyncRequired;
            this.MonitoringPercentageCompletion = disk.MonitoringPercentageCompletion;
            this.MonitoringJobType = disk.MonitoringJobType;
            this.DataPendingInStagingStorageAccountInMB = disk.DataPendingInStagingStorageAccountInMB;
            this.DataPendingAtSourceAgentInMB = disk.DataPendingAtSourceAgentInMB;
        }

        /// <summary>
        /// Gets or sets the disk uri.
        /// </summary>
        public string DiskId { get; set; }

        /// <summary>
        /// Gets or sets the primary disk storage account. 
        /// </summary>
        public string PrimaryDiskAzureStorageAccountId { get; set; }

        /// <summary>
        /// Gets or sets the primary staging storage account.
        /// </summary>
        public string PrimaryStagingAzureStorageAccountId { get; set; }

        /// <summary>
        /// Gets or sets the recovery resource group Id. 
        /// </summary>
        public string RecoveryResourceGroupId { get; set; }

        /// <summary>
        /// Gets or sets recovery disk uri.
        /// </summary>
        public string RecoveryDiskId { get; set; }

        /// <summary>
        /// Gets or sets recovery replica disk account type.
        /// </summary>
        public string RecoveryReplicaDiskAccountType { get; set; }

        /// <summary>
        /// Gets or sets recovery target disk account type.
        /// </summary>
        public string RecoveryTargetDiskAccountType { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether resync is required for this disk.
        /// </summary>
        public bool ResyncRequired { get; set; }

        /// <summary>
        /// Gets or sets the type of the monitoring job. The progress is contained in
        /// MonitoringPercentageCompletion property.
        /// </summary>
        public string MonitoringJobType { get; set; }

        /// <summary>
        /// Gets or sets the percentage of the monitoring job. The type of the monitoring job
        /// is defined by MonitoringJobType property.
        /// </summary>
        public int? MonitoringPercentageCompletion { get; set; }

        /// <summary>
        /// Gets or sets the data pending for replication in MB at staging account.
        /// </summary>
        public double DataPendingInStagingStorageAccountInMB { get; set; }

        /// <summary>
        /// Gets or sets the data pending at source virtual machine in MB.
        /// </summary>
        public double DataPendingAtSourceAgentInMB { get; set; }
    }
}
