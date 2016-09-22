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
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Azure.Commands.SiteRecovery
{
    /// <summary>
    /// Azure Site Recovery events.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Keeping all related objects together.")]
    /// <summary>
    /// Fabric Specific Details for VMWare.
    /// </summary>
    public class ASRVMWareSpecificDetails : ASRFabricSpecificDetails
    {
        /// <summary>
        /// Gets or sets the host name.
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// Gets or sets the IP address.
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the agent Version.
        /// </summary>
        public string AgentVersion { get; set; }

        /// <summary>
        /// Gets or sets the number of protected servers.
        /// </summary>
        public string ProtectedServers { get; set; }

        /// <summary>
        /// Gets or sets the last heartbeat received from CS server.
        /// </summary>
        public DateTime? LastHeartbeat { get; set; }

        /// <summary>
        /// Gets or sets the CS service status.
        /// </summary>
        public string CsServiceStatus { get; set; }

        /// <summary>
        /// Gets or sets the version status.
        /// </summary>
        public string VersionStatus { get; set; }

        /// <summary>
        /// Gets or sets List of Process Servers.
        /// </summary>
        public List<ASRProcessServer> ProcessServers { get; set; }

        /// <summary>
        /// Gets or sets List of Master Target Servers.
        /// </summary>
        public List<ASRMasterTargetServer> MasterTargetServers { get; set; }

        /// <summary>
        /// Gets or sets List of RunAsAccounts.
        /// </summary>
        public List<ASRRunAsAccount> RunAsAccounts { get; set; }
    }

    /// <summary>
    /// Details of the Process Server.
    /// </summary>
    public class ASRProcessServer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ASRProcessServer" /> class.
        /// </summary>
        public ASRProcessServer(ProcessServer processServer)
        {
            this.FriendlyName = processServer.FriendlyName;
            this.Id = processServer.Id;
            this.AgentVersion = processServer.AgentVersion;
            this.IpAddress = processServer.IpAddress;
            this.LastHeartbeat = processServer.LastHeartbeat;
            this.OsType = processServer.OsType;
            this.VersionStatus = processServer.VersionStatus;
            this.AvailableMemoryInBytes = processServer.AvailableMemoryInBytes;
            this.AvailableSpaceInBytes = processServer.AvailableSpaceInBytes;
            this.CpuLoad = processServer.CpuLoad;
            this.CpuLoadStatus = processServer.CpuLoadStatus;
            this.HostId = processServer.HostId;
            this.MemoryUsageStatus = processServer.MemoryUsageStatus;
            this.PsServiceStatus = processServer.PsServiceStatus;
            this.ReplicationPairCount = processServer.ReplicationPairCount;
            this.ServerCount = processServer.ServerCount;
            this.SpaceUsageStatus = processServer.SpaceUsageStatus;
            this.SystemLoad = processServer.SystemLoad;
            this.SystemLoadStatus = processServer.SystemLoadStatus;
            this.TotalMemoryInBytes = processServer.TotalMemoryInBytes;
            this.TotalSpaceInBytes = processServer.TotalSpaceInBytes;
            this.Updates = this.TranslateMobilityServiceUpdate(processServer.Updates);
        }

        /// <summary>
        /// Translate Mobility updates into Powershell object.
        /// </summary>
        /// <param name="mobilityUpdates">List of Mobility service update object.</param>
        /// <returns>List of Powershell mobility service objects.</returns>
        private IList<ASRMobilityServiceUpdate> TranslateMobilityServiceUpdate(
            IList<MobilityServiceUpdate> mobilityUpdates)
        {
            IList<ASRMobilityServiceUpdate> updates = new List<ASRMobilityServiceUpdate>();
            foreach (MobilityServiceUpdate update in mobilityUpdates)
            {
                updates.Add(new ASRMobilityServiceUpdate(update));
            }

            return updates;
        }

        /// <summary>
        /// Gets or sets the Process Server's friendly name.
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the Process Server Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the server.
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the OS type of the server.
        /// </summary>
        public string OsType { get; set; }

        /// <summary>
        /// Gets or sets the version of the scout component on the server.
        /// </summary>
        public string AgentVersion { get; set; }

        /// <summary>
        /// Gets or sets the last heartbeat received from the server.
        /// </summary>
        public DateTime? LastHeartbeat { get; set; }

        /// <summary>
        /// Gets or sets version status
        /// </summary>
        public string VersionStatus { get; set; }

        /// <summary>
        /// Gets or sets the list of the mobility service updates available on the
        /// Process Server.
        /// </summary>
        public IList<ASRMobilityServiceUpdate> Updates { get; set; }

        /// <summary>
        /// Gets or sets the agent generated Id.
        /// </summary>
        public string HostId { get; set; }

        /// <summary>
        /// Gets or sets the servers configured with this PS.
        /// </summary>
        public string ServerCount { get; set; }

        /// <summary>
        /// Gets or sets the number of replication pairs configured in this PS.
        /// </summary>
        public string ReplicationPairCount { get; set; }

        /// <summary>
        /// Gets or sets the percentage of the system load.
        /// </summary>
        public string SystemLoad { get; set; }

        /// <summary>
        /// Gets or sets the system load status.
        /// </summary>
        public string SystemLoadStatus { get; set; }

        /// <summary>
        /// Gets or sets the percentage of the CPU load.
        /// </summary>
        public string CpuLoad { get; set; }

        /// <summary>
        /// Gets or sets the CPU load status.
        /// </summary>
        public string CpuLoadStatus { get; set; }

        /// <summary>
        /// Gets or sets the total memory.
        /// </summary>
        public long TotalMemoryInBytes { get; set; }

        /// <summary>
        /// Gets or sets the available memory.
        /// </summary>
        public long AvailableMemoryInBytes { get; set; }

        /// <summary>
        /// Gets or sets the memory usage status.
        /// </summary>
        public string MemoryUsageStatus { get; set; }

        /// <summary>
        /// Gets or sets the total space.
        /// </summary>
        public long TotalSpaceInBytes { get; set; }

        /// <summary>
        /// Gets or sets the available space.
        /// </summary>
        public long AvailableSpaceInBytes { get; set; }

        /// <summary>
        /// Gets or sets the space usage status.
        /// </summary>
        public string SpaceUsageStatus { get; set; }

        /// <summary>
        /// Gets or sets the PS service status.
        /// </summary>
        public string PsServiceStatus { get; set; }
    }

    /// <summary>
    /// Details of a Master Target Server.
    /// </summary>
    public class ASRMasterTargetServer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ASRMasterTargetServer" /> class.
        /// </summary>
        public ASRMasterTargetServer(MasterTargetServer masterTargetDetails)
        {
            this.AgentVersion = masterTargetDetails.AgentVersion;
            this.Id = masterTargetDetails.Id;
            this.IpAddress = masterTargetDetails.IpAddress;
            this.LastHeartbeat = masterTargetDetails.LastHeartbeat;
            this.Name = masterTargetDetails.Name;
            this.OsType = masterTargetDetails.OsType;
            this.VersionStatus = masterTargetDetails.VersionStatus;
            this.RetentionVolumes =
                this.TranslateRetentionVolume(masterTargetDetails.RetentionVolumes);
            this.DataStores =
                this.TranslateDatastores(masterTargetDetails.DataStores);
        }

        /// <summary>
        /// Translate retention object to Powershell object.
        /// </summary>
        /// <param name="volumes">Rest API Retention volume object.</param>
        /// <returns></returns>
        private IList<ASRRetentionVolume> TranslateRetentionVolume(IList<RetentionVolume> volumes)
        {
            IList<ASRRetentionVolume> retentionVolumes = new List<ASRRetentionVolume>();
            foreach (RetentionVolume volume in volumes)
            {
                retentionVolumes.Add(new ASRRetentionVolume(volume));
            }

            return retentionVolumes;
        }

        /// <summary>
        /// Translate Datastore object to Powershell object.
        /// </summary>
        /// <param name="datastores">Rest API Datastore object.</param>
        /// <returns></returns>
        private IList<ASRDataStore> TranslateDatastores(IList<DataStore> datastores)
        {
            IList<ASRDataStore> asrDatastores = new List<ASRDataStore>();
            foreach (DataStore datastore in datastores)
            {
                asrDatastores.Add(new ASRDataStore(datastore));
            }

            return asrDatastores;
        }

        /// <summary>
        /// Gets or sets the server Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the server.
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the server name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the OS type of the server.
        /// </summary>
        public string OsType { get; set; }

        /// <summary>
        /// Gets or sets the version of the scout component on the server.
        /// </summary>
        public string AgentVersion { get; set; }

        /// <summary>
        /// Gets or sets the last heartbeat received from the server.
        /// </summary>
        public DateTime? LastHeartbeat { get; set; }

        /// <summary>
        /// Gets or sets version status
        /// </summary>
        public string VersionStatus { get; set; }

        /// <summary>
        /// Gets or sets the retention volumes of Master target Server.
        /// </summary>
        public IList<ASRRetentionVolume> RetentionVolumes { get; set; }

        /// <summary>
        /// Gets or sets the list of data stores in the fabric.
        /// </summary>
        public IList<ASRDataStore> DataStores { get; set; }
    }

    /// <summary>
    /// CS Accounts Details.
    /// </summary>
    public class ASRRunAsAccount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ASRRunAsAccount" /> class.
        /// </summary>
        /// <param name="runAsAccountDetails">Run as account object.</param>
        public ASRRunAsAccount(RunAsAccount runAsAccountDetails)
        {
            this.AccountId = runAsAccountDetails.AccountId;
            this.AccountName = runAsAccountDetails.AccountName;
        }

        /// <summary>
        /// Gets or sets the CS RunAs account Id.
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// Gets or sets the CS RunAs account name.
        /// </summary>
        public string AccountName { get; set; }
    }

    /// <summary>
    /// The retention details of the MT.
    /// </summary>
    public class ASRRetentionVolume
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ASRRetentionVolume" /> class.
        /// </summary>
        /// <param name="retention">Retention object.</param>
        public ASRRetentionVolume(RetentionVolume retention)
        {
            this.CapacityInBytes = retention.CapacityInBytes;
            this.FreeSpaceInBytes = retention.FreeSpaceInBytes;
            this.ThresholdPercentage = retention.ThresholdPercentage;
            this.VolumeName = retention.VolumeName;
        }

        /// <summary>
        /// Gets or sets the volume name.
        /// </summary>
        public string VolumeName { get; set; }

        /// <summary>
        /// Gets or sets the volume capacity.
        /// </summary>
        public long CapacityInBytes { get; set; }

        /// <summary>
        /// Gets or sets the free space available in this volume.
        /// </summary>
        public long FreeSpaceInBytes { get; set; }

        /// <summary>
        /// Gets or sets the threshold percentage.
        /// </summary>
        public int? ThresholdPercentage { get; set; }
    }

    /// <summary>
    /// The Datastore details of the MT.
    /// </summary>
    public class ASRDataStore
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ASRDataStore" /> class.
        /// </summary>
        /// <param name="datastore">Datastore object.</param>
        public ASRDataStore(DataStore datastore)
        {
            this.SymbolicName = datastore.SymbolicName;
            this.Uuid = datastore.Uuid;
            this.Capacity = datastore.Capacity;
            this.FreeSpace = datastore.FreeSpace;
        }

        /// <summary>
        /// Gets or sets the symbolic name of data store.
        /// </summary>
        public string SymbolicName { get; set; }

        /// <summary>
        /// Gets or sets the uuid of data store.
        /// </summary>
        public string Uuid { get; set; }

        /// <summary>
        /// Gets or sets the capacity of data store in GBs.
        /// </summary>
        public string Capacity { get; set; }

        /// <summary>
        /// Gets or sets the free space of data store in GBs.
        /// </summary>
        public string FreeSpace { get; set; }
    }

    /// <summary>
    /// The Mobility Service update details.
    /// </summary>
    public class ASRMobilityServiceUpdate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ASRMobilityServiceUpdate" /> class.
        /// </summary>
        /// <param name="mobilityServiceUpdateDetails">Mobility service update object.</param>
        public ASRMobilityServiceUpdate(MobilityServiceUpdate mobilityServiceUpdateDetails)
        {
            this.RebootStatus = mobilityServiceUpdateDetails.RebootStatus;
            this.OsType = mobilityServiceUpdateDetails.OsType;
            this.Version = mobilityServiceUpdateDetails.Version;
        }

        /// <summary>
        /// Gets or sets the version of the latest update.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the reboot status of the update - whether it is required or not.
        /// </summary>
        public string RebootStatus { get; set; }

        /// <summary>
        /// Gets or sets the OS type.
        /// </summary>
        public string OsType { get; set; }
    }
}
