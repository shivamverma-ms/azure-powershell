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
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Management.SiteRecovery.Models;

namespace Microsoft.Azure.Commands.SiteRecovery.Models.ReplicationProvider
{
    /// <summary>
    /// AzureToAzure replication provider specific entity details.
    /// </summary>
    public class ASRAzureToAzureReplicationDetails : ASRReplicationProviderSpecificSettings
    {
        /// <summary>
        /// Initializes a new instance of the<see cref="ASRAzureToAzureReplicationDetails" /> class.
        /// </summary>
        public ASRAzureToAzureReplicationDetails()
        {
            this.ProtectedDisks = new List<A2AProtectedDiskDetails>();
        }

        /// <summary>
        /// Initializes a new instance of the<see cref="ASRAzureToAzureReplicationDetails" /> class.
        /// </summary>
        public ASRAzureToAzureReplicationDetails(A2AReplicationDetails details)
            : base((ReplicationProviderSpecificSettings)details)
        {
            this.FabricObjectId = details.FabricObjectId;
            this.MultiVmGroupId = details.MultiVmGroupId;
            this.MultiVmGroupName = details.MultiVmGroupName;
            this.OSType = details.OSType;
            this.PrimaryFabricLocation = details.PrimaryFabricLocation;
            this.ProtectedDisks =
                details.ProtectedDisks.ToList()
                .ConvertAll(disk => new A2AProtectedDiskDetails(disk));
            this.RecoveryAzureResourceGroupId = details.RecoveryAzureResourceGroupId;
            this.RecoveryAzureCloudService = details.RecoveryCloudService;
            this.RecoveryAzureVMName = details.RecoveryAzureVMName;
            this.RecoveryAzureVMSize = details.RecoveryAzureVMSize;
            this.RecoveryFabricLocation = details.RecoveryFabricLocation;
            this.SelectedRecoveryAzureNetworkId = details.SelectedRecoveryAzureNetworkId;
            this.RecoveryAvailabilitySet = details.RecoveryAvailabilitySet;
            if (details.VmSyncedConfigDetails != null)
            {
                this.VmSyncedConfigDetails =
                    new ASRAzureToAzureVmSyncedConfigDetails(details.VmSyncedConfigDetails);
            }
            this.MonitoringJobType = details.MonitoringJobType;
            this.MonitoringPercentageCompletion = details.MonitoringPercentageCompletion;
            if (details.LastHeartbeat != null)
            {
                this.LastHeartbeat = details.LastHeartbeat.Value.ToLocalTime();
            }
        }

        /// <summary>
        //     Optional. Fabric object ARM Id.
        /// </summary>
        public string FabricObjectId { get; set; }

        /// <summary>
        /// Multi vm group Id.
        /// </summary>
        public string MultiVmGroupId { get; set; }

        /// <summary>
        /// Multi vm group name.
        /// </summary>
        public string MultiVmGroupName { get; set; }
        /// </summary>

        /// <summary>
        /// Operating system type.
        /// </summary>
        public string OSType { get; set; }

        /// <summary>
        /// Primary fabric location.
        /// </summary>
        public string PrimaryFabricLocation { get; set; }

        /// <summary>
        /// List of disk specific details.
        /// </summary>
        public List<A2AProtectedDiskDetails> ProtectedDisks { get; set; }

        /// <summary>
        /// Recovery azure resource group id.
        /// </summary>
        public string RecoveryAzureResourceGroupId { get; set; }

        /// <summary>
        /// Recovery azure cloud service.
        /// </summary>
        public string RecoveryAzureCloudService { get; set; }

        /// <summary>
        /// Recovery azure vm name.
        /// </summary>
        public string RecoveryAzureVMName { get; set; }

        /// <summary>
        /// Recovery azure vm size.
        /// </summary>
        public string RecoveryAzureVMSize { get; set; }

        /// <summary>
        /// Recovery fabric location.
        /// </summary>
        public string RecoveryFabricLocation { get; set; }

        /// <summary>
        /// Selected recovery azure network id.
        /// </summary>
        public string SelectedRecoveryAzureNetworkId { get; set; }

        /// <summary>
        /// Recovery availability set.
        /// </summary>
        public string RecoveryAvailabilitySet { get; set; }

        /// <summary>
        /// Synced configuration details of the virtual machine.
        /// </summary>
        public ASRAzureToAzureVmSyncedConfigDetails VmSyncedConfigDetails { get; set; }

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
        /// Gets or sets the last heartbeat received from the source server.
        /// </summary>
        public DateTime? LastHeartbeat { get; set; }
    }
}
