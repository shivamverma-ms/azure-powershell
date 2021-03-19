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
using Microsoft.Azure.Commands.RecoveryServices.SiteRecovery.Properties;
using Microsoft.Azure.Management.RecoveryServices.SiteRecovery.Models;

namespace Microsoft.Azure.Commands.RecoveryServices.SiteRecovery
{
    /// <summary>
    ///     Creates Azure Site Recovery VM NIC IP configuration for A2A replication.
    /// </summary>
    [Cmdlet("New", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "RecoveryServicesAsrVMNicIPConfig", DefaultParameterSetName = ASRParameterSets.AzureToAzure, SupportsShouldProcess = true)]
    [Alias("New-ASRVMNicIPConfig")]
    [OutputType(typeof(IPConfigInputDetails))]
    public class NewAzureRmAsrVmNicIPConfig : SiteRecoveryCmdletBase
    {
        #region Parameters
        /// <summary>
        ///    Gets or sets the NIC Id.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzure,
            Mandatory = true,
            HelpMessage = "Specify the ASR NIC GUID.")]
        public string NicId { get; set; }

        /// <summary>
        ///    Specify the ASR Replication Protected Item.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzure,
            Mandatory = true,
            HelpMessage = "Specify the ASR Replication Protected Item.")]
        public ASRReplicationProtectedItem ReplicationProtectedItem { get; set; }


        /// <summary>
        ///    Gets or sets the IP config name.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzure,
            Mandatory = true,
            HelpMessage = "Specify the IP config name.")]
        public string IpConfigName { get; set; }

        /// <summary>
        ///    Gets or sets the if the IP config is primary on NIC.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzure,
            Mandatory = false,
            HelpMessage = "Specify if the IP config is primary.")]
        public bool IsPrimary { get; set; }

        /// <summary>
        ///     Gets or sets whether an existing IP config is selected for tfo/failover.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzure,
            Mandatory = false,
            HelpMessage = "Specifies whether an existing IP config is selected for tfo/failover.")]
        [ValidateNotNullOrEmpty]
        public SwitchParameter IsSelectedForFailover { get; set; }

        /// <summary>
        ///     Gets or sets the name of the recovery subnet.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzure,
            Mandatory = false,
            HelpMessage = "Specifies the name of the recovery subnet.")]
        [ValidateNotNullOrEmpty]
        public string RecoverySubnetName { get; set; }

        /// <summary>
        ///     Gets or sets the static IP address that should be assigned to IP config on recovery.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzure,
            Mandatory = false,
            HelpMessage = "Specifies the IP address of the recovery IP config.")]
        [ValidateNotNull]
        public string RecoveryStaticIPAddress { get; set; }

        /// <summary>
        ///     Gets or sets the id of the public IP address resource associated with the recovery IP config.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzure,
            Mandatory = false,
            HelpMessage = "Specifies the ID of the public IP address associated with the recovery IP config.")]
        [ValidateNotNullOrEmpty]
        public string RecoveryPublicIPAddressId { get; set; }

        /// <summary>
        ///     Gets or sets the target backend address pools for the recovery IP config.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzure,
            Mandatory = false,
            HelpMessage = "Specifies the IDs of backend address pools for the recovery IP config.")]
        [ValidateNotNull]
        public string[] RecoveryLBBackendAddressPoolIds { get; set; }

        /// <summary>
        ///     Gets or sets the name of the test failover subnet.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzure,
            Mandatory = false,
            HelpMessage = "Specifies the name of the test failover subnet.")]
        [ValidateNotNullOrEmpty]
        public string TfoSubnetName { get; set; }

        /// <summary>
        ///     Gets or sets the static IP address that should be assigned to test failover IP config on recovery.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzure,
            Mandatory = false,
            HelpMessage = "Specifies the IP address of the test failover IP config.")]
        [ValidateNotNull]
        public string TfoStaticIPAddress { get; set; }

        /// <summary>
        ///     Gets or sets the id of the public IP address resource associated with the test failover IP config.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzure,
            Mandatory = false,
            HelpMessage = "Specifies the ID of the public IP address associated with the test failover IP config.")]
        [ValidateNotNullOrEmpty]
        public string TfoPublicIPAddressId { get; set; }

        /// <summary>
        ///     Gets or sets the target backend address pools for the test failover IP config.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzure,
            Mandatory = false,
            HelpMessage = "Specifies the IDs of backend address pools for the test failover IP config.")]
        [ValidateNotNull]
        public string[] TfoLBBackendAddressPoolIds { get; set; }



        #endregion Parameters

        /// <summary>
        ///     ProcessRecord of the command.
        /// </summary>
        public override void ExecuteSiteRecoveryCmdlet()
        {
            base.ExecuteSiteRecoveryCmdlet();
            IPConfigInputDetails ipConfig = null;

            if (string.IsNullOrEmpty(this.RecoverySubnetName) &&
                !string.IsNullOrEmpty(this.RecoveryStaticIPAddress))
            {
                this.WriteWarning(Resources.RecoverySubnetInformationMissing);
                return;
            }

            if (string.IsNullOrEmpty(this.TfoSubnetName) &&
                !string.IsNullOrEmpty(this.TfoStaticIPAddress))
            {
                this.WriteWarning(Resources.TfoSubnetInformationMissing);
                return;
            }

            switch (this.ParameterSetName)
            {
                case ASRParameterSets.AzureToAzure:

                    var providerSpecificDetails =
                        this.ReplicationProtectedItem.ProviderSpecificDetails;

                    if (!(providerSpecificDetails is ASRAzureToAzureSpecificRPIDetails))
                    {
                        this.WriteWarning(
                            Resources.UnsupportedReplicationProvidedForASRVMNicIPConfig);
                        return;
                    }

                    var vmNicDetailsList =
                        this.ReplicationProtectedItem.NicDetailsList ??
                        new List<ASRVMNicDetails>();

                    var vmNic =
                        vmNicDetailsList.FirstOrDefault(
                            nic => nic.NicId.Equals(
                                this.NicId, StringComparison.OrdinalIgnoreCase));

                    if (vmNic == null)
                    {
                        this.WriteWarning(string.Format(Resources.NicNotFoundInVM, this.NicId));
                        return;
                    }

                    var vmNicIPConfig = vmNic.IpConfigs.FirstOrDefault(
                        ip => ip.Name.Equals(
                            this.IpConfigName, StringComparison.OrdinalIgnoreCase));

                    if (vmNicIPConfig == null)
                    {
                        this.WriteWarning(string.Format(Resources.IPConfigNotFoundInVMNic, this.IpConfigName));
                        return;
                    }

                    if (!this.MyInvocation.BoundParameters.ContainsKey(
                            Utilities.GetMemberName(() => this.IsPrimary)))
                    {
                        this.IsPrimary = (bool)vmNicIPConfig.IsPrimary;
                    }

                    if (!this.MyInvocation.BoundParameters.ContainsKey(
                            Utilities.GetMemberName(() => this.RecoverySubnetName)))
                    {
                        this.RecoverySubnetName = vmNicIPConfig.RecoverySubnetName;
                    }

                    if (!this.MyInvocation.BoundParameters.ContainsKey(
                           Utilities.GetMemberName(() => this.RecoveryStaticIPAddress)))
                    {
                        this.RecoveryStaticIPAddress = vmNicIPConfig.RecoveryStaticIPAddress;
                    }

                    if (!this.MyInvocation.BoundParameters.ContainsKey(
                            Utilities.GetMemberName(() => this.RecoveryPublicIPAddressId)))
                    {
                        this.RecoveryPublicIPAddressId = vmNicIPConfig.RecoveryPublicIPAddressId;
                    }

                    if (!this.MyInvocation.BoundParameters.ContainsKey(
                            Utilities.GetMemberName(() => this.RecoveryLBBackendAddressPoolIds)))
                    {
                        this.RecoveryLBBackendAddressPoolIds =
                            vmNicIPConfig.RecoveryLBBackendAddressPoolIds?.ToArray();
                    }

                    if (!this.MyInvocation.BoundParameters.ContainsKey(
                            Utilities.GetMemberName(() => this.TfoSubnetName)))
                    {
                        this.TfoSubnetName = vmNicIPConfig.TfoSubnetName;
                    }

                    if (!this.MyInvocation.BoundParameters.ContainsKey(
                           Utilities.GetMemberName(() => this.TfoStaticIPAddress)))
                    {
                        this.TfoStaticIPAddress = vmNicIPConfig.TfoStaticIPAddress;
                    }

                    if (!this.MyInvocation.BoundParameters.ContainsKey(
                            Utilities.GetMemberName(() => this.TfoPublicIPAddressId)))
                    {
                        this.TfoPublicIPAddressId = vmNicIPConfig.TfoPublicIPAddressId;
                    }

                    if (!this.MyInvocation.BoundParameters.ContainsKey(
                            Utilities.GetMemberName(() => this.TfoLBBackendAddressPoolIds)))
                    {
                        this.TfoLBBackendAddressPoolIds =
                            vmNicIPConfig.TfoLBBackendAddressPoolIds?.ToArray();
                    }

                    ipConfig = new IPConfigInputDetails
                    {
                        IpConfigName = this.IpConfigName,
                        IsPrimary = this.IsPrimary,
                        IsSeletedForFailover = this.IsSelectedForFailover,
                        RecoverySubnetName = this.RecoverySubnetName,
                        RecoveryStaticIPAddress = this.RecoveryStaticIPAddress,
                        RecoveryPublicIPAddressId = this.RecoveryPublicIPAddressId,
                        RecoveryLBBackendAddressPoolIds = this.RecoveryLBBackendAddressPoolIds?.ToList() ??
                            new List<string>(),
                        TfoSubnetName = this.TfoSubnetName,
                        TfoStaticIPAddress = this.TfoStaticIPAddress,
                        TfoPublicIPAddressId = this.TfoPublicIPAddressId,
                        TfoLBBackendAddressPoolIds = this.TfoLBBackendAddressPoolIds?.ToList() ??
                            new List<string>()
                    };

                    break;
            }

            this.WriteObject(ipConfig);
        }
    }
}
