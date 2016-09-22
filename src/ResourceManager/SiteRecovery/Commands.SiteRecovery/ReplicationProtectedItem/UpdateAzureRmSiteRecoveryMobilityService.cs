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
    /// Used to update mobility service.
    /// </summary>
    [Cmdlet(VerbsData.Update, "AzureRmSiteRecoveryMobilityService", DefaultParameterSetName = ASRParameterSets.Default)]
    [OutputType(typeof(ASRJob))]
    public class UpdateAzureRmSiteRecoveryMobilityService : SiteRecoveryCmdletBase
    {
        #region Local Parameters

        /// <summary>
        /// Gets or sets Name of the Fabric.
        /// </summary>
        public string fabricName;

        /// <summary>
        /// Gets or sets Name of the Protection Container.
        /// </summary>
        public string protectionContainerName;

        /// <summary>
        /// Gets or sets Name of the Protectable Item.
        /// </summary>
        public string protectableItemName;

        #endregion Local Parameters

        #region Parameters

        /// <summary>
        /// Gets or sets Replication Protected Item.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.Default, Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRReplicationProtectedItem ReplicationProtectedItem { get; set; }

        /// <summary>
        /// Gets or sets RunAsAccount.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.Default, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public RunAsAccount Account { get; set; }

        #endregion Parameters

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteSiteRecoveryCmdlet()
        {
            base.ExecuteSiteRecoveryCmdlet();

            // Validate the Replication Provider for InMageAzureV2 / InMage.
            if ((string.Compare(
                    this.ReplicationProtectedItem.ReplicationProvider,
                    Constants.InMageAzureV2,
                    StringComparison.OrdinalIgnoreCase) != 0) &&
                (string.Compare(
                    this.ReplicationProtectedItem.ReplicationProvider,
                    Constants.InMage,
                    StringComparison.OrdinalIgnoreCase) != 0))
            {
                throw new InvalidOperationException(
                    string.Format(
                        Properties.Resources.UnsupportedReplicationProviderForUpdateMobilityService.ToString(),
                        this.ReplicationProtectedItem.ReplicationProvider));
            }

            // Set the Fabric Name and Protection Container Name.
            this.fabricName =
                Utilities.GetValueFromArmId(
                    this.ReplicationProtectedItem.ID,
                    ARMResourceTypeConstants.ReplicationFabrics);
            this.protectionContainerName =
                Utilities.GetValueFromArmId(
                    this.ReplicationProtectedItem.ID,
                    ARMResourceTypeConstants.ReplicationProtectionContainers);
            this.protectableItemName =
                Utilities.GetValueFromArmId(
                    this.ReplicationProtectedItem.ProtectableItemId,
                    ARMResourceTypeConstants.ProtectableItems);

            // Create the Update Mobility Service input request.
            UpdateMobilityServiceRequest input = new UpdateMobilityServiceRequest()
            {
                Properties = new UpdateMobilityServiceRequestProperties()
                {
                    RunAsAccountId = this.Account.AccountId
                }
            };

            // Update the Mobility Service.
            LongRunningOperationResponse response =
                RecoveryServicesClient.UpdateAzureSiteRecoveryMobilityService(
                    this.fabricName,
                    this.protectionContainerName,
                    this.protectableItemName,
                    input);

            JobResponse jobResponse =
                RecoveryServicesClient.GetAzureSiteRecoveryJobDetails(
                    PSRecoveryServicesClient.GetJobIdFromReponseLocation(response.Location));

            WriteObject(new ASRJob(jobResponse.Job));
        }
    }
}