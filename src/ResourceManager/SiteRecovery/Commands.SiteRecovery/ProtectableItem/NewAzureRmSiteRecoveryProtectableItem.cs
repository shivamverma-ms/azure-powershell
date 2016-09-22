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
using System.Management.Automation;

namespace Microsoft.Azure.Commands.SiteRecovery
{
    /// <summary>
    /// Discovers Protectable Items.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "AzureRmSiteRecoveryProtectableItem", DefaultParameterSetName = ASRParameterSets.Default)]
    [OutputType(typeof(ASRJob))]
    public class NewAzureRmSiteRecoveryProtectableItem : SiteRecoveryCmdletBase
    {
        #region Local Parameters

        /// <summary>
        /// Gets or sets Name of the Fabric.
        /// </summary>
        public string fabricName;

        #endregion Local Parameters

        #region Parameters

        /// <summary>
        /// Gets or sets Protection Container.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.Default, Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRProtectionContainer ProtectionContainer { get; set; }

        /// <summary>
        /// Gets or sets Friendly Name of the Protectable Item.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets IPAddress of the Protectable Item.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string IPAddress { get; set; }

        /// <summary>
        /// Gets or sets OS Type of the Protectable Item.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [ValidateSet(
            Constants.OSWindows,
            Constants.OSLinux)]
        public string OSType { get; set; }

        #endregion Parameters

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteSiteRecoveryCmdlet()
        {
            base.ExecuteSiteRecoveryCmdlet();

            // Validate the Fabric Type for VMWare.
            if (this.ProtectionContainer.FabricType != Constants.VMware)
            {
                throw new InvalidOperationException(
                    string.Format(
                        Properties.Resources.UnsupportedFabricTypeForDiscoverVirtualMachines,
                        this.ProtectionContainer.FabricType));
            }

            // Set the Fabric Name.
            this.fabricName = Utilities.GetValueFromArmId(
                this.ProtectionContainer.ID,
                ARMResourceTypeConstants.ReplicationFabrics);

            // Create the Discover Protectable Item input request.
            DiscoverProtectableItemRequest input = new DiscoverProtectableItemRequest()
            {
                Properties = new DiscoverProtectableItemRequestProperties()
                {
                    FriendlyName = this.Name,
                    IpAddress = this.IPAddress,
                    OsType = this.OSType
                }
            };

            // Discover the Protectable Item.
            LongRunningOperationResponse response =
                RecoveryServicesClient.NewAzureSiteRecoveryProtectableItem(
                    fabricName,
                    this.ProtectionContainer.Name,
                    input);

            JobResponse jobResponse =
                RecoveryServicesClient.GetAzureSiteRecoveryJobDetails(
                    PSRecoveryServicesClient.GetJobIdFromReponseLocation(
                        response.Location));

            this.WriteObject(new ASRJob(jobResponse.Job));
        }
    }
}