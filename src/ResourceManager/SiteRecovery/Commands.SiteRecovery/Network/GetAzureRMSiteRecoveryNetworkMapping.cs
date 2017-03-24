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
    [Cmdlet(VerbsCommon.Get, "AzureRmSiteRecoveryNetworkMapping", DefaultParameterSetName = ASRParameterSets.Default)]
    [OutputType(typeof(IEnumerable<ASRNetworkMapping>))]
    public class GetAzureRmSiteRecoveryNetworkMapping : SiteRecoveryCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets name of the network mapping.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.ByNetworkObjectWithName, Mandatory = true)]
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzureWithName, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Primary Fabric object.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToEnterpriseLegacy, Mandatory = true, ValueFromPipeline = true)]
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToAzureLegacy, Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRFabric PrimaryFabric { get; set; }

        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzure, Mandatory = true, ValueFromPipeline = true)]
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzureWithName, Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRFabric PrimaryAzureFabric { get; set; }
        /// <summary>
        /// Gets or sets Recovery Fabric object.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToEnterpriseLegacy, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public ASRFabric RecoveryFabric { get; set; }

        /// <summary>
        /// Gets or sets switch parameter. On passing, command sets target as Azure.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToAzureLegacy, Mandatory = true)]
        public SwitchParameter Azure { get; set; }

        /// <summary>
        /// Gets or sets primary network object.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.ByNetworkObject, Mandatory = true, ValueFromPipeline = true)]
        [Parameter(ParameterSetName = ASRParameterSets.ByNetworkObjectWithName, Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRNetwork PrimaryNetwork { get; set; }

        #endregion Parameters

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteSiteRecoveryCmdlet()
        {
            base.ExecuteSiteRecoveryCmdlet();

            switch (this.ParameterSetName)
            {
                case ASRParameterSets.EnterpriseToEnterpriseLegacy:
                    this.GetEnterpriseToEnterpriseNetworkMappingLegacy();
                    break;
                case ASRParameterSets.EnterpriseToAzureLegacy:
                    this.GetEnterpriseToAzureNetworkMappingLegacy();
                    break;
                case ASRParameterSets.ByNetworkObject:
                    this.GetNetworkMappingsByPrimaryNetwork();
                    break;
                case ASRParameterSets.ByNetworkObjectWithName:
                    this.GetNetworkMappingsByPrimaryNetworkAndName();
                    break;
                case ASRParameterSets.AzureToAzure:
                    this.GetAzureToAzureNetworkMappings();
                    break;
                case ASRParameterSets.AzureToAzureWithName:
                    this.GetAzureToAzureNetworkMappingByName();
                    break;
                case ASRParameterSets.Default:
                    this.WriteWarningWithTimestamp(
                    string.Format(Properties.Resources.ParameterSetForScenarioWillBeModifiedSoon,
                        "{}",
                        "enumerating all network pairs",
                        "either of {PrimaryNetwork(mandatory), Name(optional)} or" +
                        " {PrimaryAzureFabric(mandatory), Name(optional)}"
                        ));
                    this.WriteNetworkMappings(GetAllNetworkMappings());
                    break;
            }
        }

        /// <summary>
        /// Filter Enterprise to Enterprise Network mappings.
        /// </summary>
        private void GetEnterpriseToEnterpriseNetworkMappingLegacy()
        {
            string upcomingParameterSet = "{PrimaryNetwork(mandatory), Name(optional)}";
            this.WriteWarningWithTimestamp(
                    string.Format(Properties.Resources.ParameterSetForScenarioWillBeModifiedSoon,
                        "{PrimaryFabric(mandatory), RecoveryFabric(mandatory)}",
                        "listing VMM networks paired with other VMM Networks",
                        upcomingParameterSet));

            foreach (NetworkMapping networkMapping in GetAllNetworkMappings())
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
        private void GetEnterpriseToAzureNetworkMappingLegacy()
        {
            string upcomingParameterSet = "{PrimaryNetwork(mandatory), Name(optional)";
            this.WriteWarningWithTimestamp(
                    string.Format(Properties.Resources.ParameterSetForScenarioWillBeModifiedSoon,
                        "{PrimaryFabric(mandatory), Azure(mandatory)}",
                        "listing VMM networks paired with azure networks",
                        upcomingParameterSet));

            foreach (NetworkMapping networkMapping in GetAllNetworkMappings())
            {
                string primaryFabricName = Utilities.GetValueFromArmId(networkMapping.Id, ARMResourceTypeConstants.ReplicationFabrics);

                if (0 == string.Compare(this.PrimaryFabric.Name, primaryFabricName, true) &&
                    !networkMapping.Properties.RecoveryNetworkId.Contains(ARMResourceTypeConstants.ReplicationFabrics))
                {
                    this.WriteNetworkMapping(networkMapping);
                }
            }
        }

        /// <summary>
        /// Get Enterprise to Enterprise and Enterprise to Azure network mappings.
        /// This method merely mirrors the actual ARM api in terms of input and offers no 
        /// additional filtering of result.
        /// </summary>
        private void GetNetworkMappingsByPrimaryNetworkAndName()
        {
            this.WriteNetworkMapping(
                GetNetworkMapping(
                    Utilities.GetValueFromArmId(
                        this.PrimaryNetwork.ID, ARMResourceTypeConstants.ReplicationFabrics),
                    this.PrimaryNetwork.ID,
                    this.Name));
        }

        /// <summary>
        /// Get Enterprise to Enterprise and Enterprise to Azure network mapping.
        /// This method merely mirrors the actual ARM api in terms of input and retrieves the 
        /// mapping by resource name.
        /// </summary>
        private void GetNetworkMappingsByPrimaryNetwork()
        {
            this.WriteNetworkMappings(
                ListNetworkMappings(
                    Utilities.GetValueFromArmId(
                        this.PrimaryNetwork.ID, ARMResourceTypeConstants.ReplicationFabrics),
                    this.PrimaryNetwork.ID));
        }

        /// <summary>
        /// Lists the Azure to Azure Network mappings for the specified fabric.
        /// </summary>
        private void GetAzureToAzureNetworkMappings()
        {
            // TODO (omnishan) Verify exposed parameter names from PMs.
            if (!string.Equals(
                this.PrimaryAzureFabric.Type,
                Constants.Azure,
                StringComparison.InvariantCultureIgnoreCase))
            {
                this.WriteExceptionError(
                    new InvalidOperationException("Fabric is not of type Azure."));
            }

            this.WriteNetworkMappings(
                ListNetworkMappings(
                    this.PrimaryAzureFabric.Name, ARMResourceTypeConstants.AzureNetwork));
        }

        /// <summary>
        /// Get Azure to Azure Network mapping.
        /// </summary>
        private void GetAzureToAzureNetworkMappingByName()
        {
            if (!string.Equals(
                this.PrimaryAzureFabric.Type,
                Constants.Azure,
                StringComparison.InvariantCultureIgnoreCase))
            {
                this.WriteExceptionError(
                    new InvalidOperationException("Fabric is not of type Azure."));
            }

            this.WriteNetworkMapping(
                GetNetworkMapping(
                    this.PrimaryAzureFabric.Name,
                    ARMResourceTypeConstants.AzureNetwork,
                    this.Name));
        }

        /// <summary>
        /// Enumerates all network mappings in the current vault.
        /// </summary>
        private IList<NetworkMapping> GetAllNetworkMappings()
        {
            NetworkMappingsListResponse networkMappingsListResponse =
                    RecoveryServicesClient
                    .GetAzureSiteRecoveryNetworkMappings();

            return networkMappingsListResponse.NetworkMappingsList;
        }

        /// <summary>
        /// Lists all network mappings for a given primary network.
        /// </summary>
        /// <param name="primaryFabricName">Primary fabric name.</param>
        /// <param name="primaryNetworkName">Primary network name.</param>
        private IList<NetworkMapping> ListNetworkMappings(string primaryFabricName, string primaryNetworkName)
        {
            NetworkMappingsListResponse networkMappingsListResponse =
                    RecoveryServicesClient.GetAzureSiteRecoveryNetworkMappings(
                        primaryFabricName,
                        primaryNetworkName);

            return networkMappingsListResponse.NetworkMappingsList;
        }

        /// <summary>
        /// Gets the network mapping for a given primary network and ARM resource name.
        /// </summary>
        /// <param name="primaryFabricName">Primary fabric name.</param>
        /// <param name="primaryNetworkName">Primary network name.</param>
        /// <param name="mappingName">ARM resource name of the mapping.</param>
        private NetworkMapping GetNetworkMapping(
            string primaryFabricName,
            string primaryNetworkName,
            string mappingName)
        {
            NetworkMappingResponse networkMapping =
                RecoveryServicesClient.GetAzureSiteRecoveryNetworkMappings(
                    primaryFabricName,
                    primaryNetworkName,
                    mappingName);
            return networkMapping.NetworkMapping;
        }

        /// <summary>
        /// Write Network mappings.
        /// </summary>
        /// <param name="networkMappings">List of Network mappings</param>
        private void WriteNetworkMappings(IList<NetworkMapping> networkMappings)
        {
            this.WriteObject(
                networkMappings.Select(mapping => new ASRNetworkMapping(mapping)), true);
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