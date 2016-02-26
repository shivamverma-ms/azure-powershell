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
using System.Management.Automation;
using Microsoft.Azure.Management.SiteRecovery.Models;

namespace Microsoft.Azure.Commands.SiteRecovery
{
    /// <summary>
    /// Retrieves Azure Site Recovery Network mappings.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "AzureRmSiteRecoveryNetworkMappingNM", DefaultParameterSetName = ASRParameterSets.Default)]
    [OutputType(typeof(IEnumerable<ASRNetworkMapping>))]
    public class GetAzureRmSiteRecoveryNetworkMappingNM : SiteRecoveryCmdletBase
    {
        /// <summary>
        /// holds Network Mappings
        /// </summary>
        private NetworkMappingsListResponse networkMappingsListResponse;

        /// <summary>
        /// 
        /// </summary>
        string primaryServerName = string.Empty;
        string recoveryServerName = string.Empty;

        #region Parameters
        /// <summary>
        /// Gets or sets Primary Fabric object.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToEnterprise, Mandatory = true)]
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToAzure, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public ASRFabric PrimaryFabric { get; set; }

        /// <summary>
        /// Gets or sets Recovery Fabric object.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToEnterprise, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public ASRFabric RecoveryFabric { get; set; }

        /// <summary>
        /// Gets or sets switch parameter. On passing, command sets target as Azure.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToAzure, Mandatory = true)]
        public SwitchParameter Azure { get; set; }

        #endregion Parameters

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteSiteRecoveryCmdlet()
        {
            base.ExecuteSiteRecoveryCmdlet();

            networkMappingsListResponse =
                    RecoveryServicesClient
                    .GetAzureSiteRecoveryNetworkMappings();

            switch (this.ParameterSetName)
            {
                case ASRParameterSets.EnterpriseToEnterprise:
                    this.FilterE2EMappings();
                    break;
                case ASRParameterSets.EnterpriseToAzure:
                    this.FilterE2AMappings();
                    break;
                case ASRParameterSets.Default:
                    WriteNetworkMappings(networkMappingsListResponse.NetworkMappingsList);
                    break;
            }
        }

        /// <summary>
        /// Filter Enterprise to Enterprise Network mappings
        /// </summary>
        private void FilterE2EMappings()
        {              
            foreach (NetworkMapping networkMapping in networkMappingsListResponse.NetworkMappingsList)
            {
                string primaryFabricName = Utilities.GetValueFromArmId(networkMapping.Id, ARMResourceTypeConstants.ReplicationFabrics);

                // Skip azure cases 
                if (!networkMapping.Properties.RecoveryNetworkId.ToLower().Contains(ARMResourceTypeConstants.ReplicationFabrics.ToLower()))
                    continue;

                string recoveryFabricName = Utilities.GetValueFromArmId(networkMapping.Properties.RecoveryNetworkId, ARMResourceTypeConstants.ReplicationFabrics);

                if (0 == string.Compare(this.PrimaryFabric.Name, primaryFabricName, true) &&
                    0 == string.Compare(this.RecoveryFabric.Name, recoveryFabricName, true))
                {
                    this.WriteNetworkMapping(networkMapping);
                }
            }
        }

        /// <summary>
        /// Filter Enterprise to Azure Network mappings
        /// </summary>
        private void FilterE2AMappings()
        {
            foreach (NetworkMapping networkMapping in networkMappingsListResponse.NetworkMappingsList)
            {
                string primaryFabricName = Utilities.GetValueFromArmId(networkMapping.Id, ARMResourceTypeConstants.ReplicationFabrics);

                if (0 == string.Compare(this.PrimaryFabric.Name, this.primaryServerName, true) &&
                    !networkMapping.Properties.RecoveryNetworkId.Contains(ARMResourceTypeConstants.ReplicationFabrics))
                {
                    this.WriteNetworkMapping(networkMapping);
                }
            }
        }

        /// <summary>
        /// Write Network mappings.
        /// </summary>
        /// <param name="networkMappings">List of Network mappings</param>
        private void WriteNetworkMappings(IList<NetworkMapping> networkMappings)
        {
            this.WriteObject(networkMappings.Select(nm => new ASRNetworkMapping(nm)), true);
        }

        /// <summary>
        /// Write Network mapping.
        /// </summary>
        /// <param name="networkMapping">Network mapping</param>
        private void WriteNetworkMapping(NetworkMapping networkMapping)
        {
            this.WriteObject(new ASRNetworkMapping(networkMapping));
        }
    }
}