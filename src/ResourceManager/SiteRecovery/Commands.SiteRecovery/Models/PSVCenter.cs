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
using Microsoft.Azure.Management.SiteRecovery.Models;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Azure.Commands.SiteRecovery
{
    /// <summary>
    /// Azure Site Recovery vCenter server.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Keeping all related objects together.")]
    public class ASRVCenter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ASRVCenter" /> class.
        /// </summary>
        public ASRVCenter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ASRVCenter" /> class with required 
        /// parameters.
        /// </summary>
        /// <param name="server">vCenter server object</param>
        public ASRVCenter(VCenter server)
        {
            this.ID = server.Id;
            this.Name = server.Name;
            this.FriendlyName = server.Properties.FriendlyName;
            this.Server = server.Properties.IpAddress;
            this.Port = server.Properties.Port;
            this.FabricArmResourceName = server.Properties.FabricArmResourceName;
            this.ProcessServerId = server.Properties.ProcessServerId;
            this.AccountId = server.Properties.RunAsAccountId;
            this.DiscoveryStatus = server.Properties.DiscoveryStatus;
            this.LastHeartbeat = server.Properties.LastHeartbeat;
        }

        #region Properties
        /// <summary>
        /// Gets or sets Name of the Server.
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the IP address.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the Port number.
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// Gets name of the Server.
        /// </summary>
        public string Name { get;}

        /// <summary>
        /// Gets Server ID.
        /// </summary>
        public string ID { get;}

        /// <summary>
        /// Gets the Fabric arm resoure name.
        /// </summary>
        public string FabricArmResourceName { get;}

        /// <summary>
        /// Gets or sets Process Server ID.
        /// </summary>
        public string ProcessServerId { get; set; }

        /// <summary>
        /// Gets or sets run as account id.
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// vCenter discovery status.
        /// </summary>
        public string DiscoveryStatus { get; set; }

        /// <summary>
        /// Last time vCenter synced with the service.
        /// </summary>
        public DateTime? LastHeartbeat { get; set; }
        #endregion
    }
}