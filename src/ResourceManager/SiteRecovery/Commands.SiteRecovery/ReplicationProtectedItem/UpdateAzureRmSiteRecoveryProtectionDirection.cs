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
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Portal.RecoveryServices.Models.Common;
using Microsoft.Azure.Management.SiteRecovery.Models;
using System.Collections.Generic;

namespace Microsoft.Azure.Commands.SiteRecovery
{
    /// <summary>
    /// Used to initiate a recovery protection operation.
    /// </summary>
    [Cmdlet(VerbsData.Update, "AzureRmSiteRecoveryProtectionDirection", DefaultParameterSetName = ASRParameterSets.ByPEObject)]
    [OutputType(typeof(ASRJob))]
    public class UpdateAzureRmSiteRecoveryProtection : SiteRecoveryCmdletBase
    {
        /// <summary>
        /// Gets or sets Name of the Protection Container.
        /// </summary>
        public string protectionContainerName;

        /// <summary>
        /// Gets or sets Name of the Fabric.
        /// </summary>
        public string fabricName;

        #region Parameters

        /// <summary>
        /// Gets or sets Recovery Plan object.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.ByRPObject, Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRRecoveryPlan RecoveryPlan { get; set; }

        /// <summary>
        /// Gets or sets Replication Protected Item.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.ByPEObject, Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRReplicationProtectedItem ReplicationProtectedItem { get; set; }

        /// <summary>
        /// Gets or sets Failover direction for the recovery plan.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.ByRPObject, Mandatory = true, ValueFromPipeline = true)]
        [Parameter(ParameterSetName = ASRParameterSets.ByPEObject, Mandatory = true, ValueFromPipeline = false)]
        [ValidateSet(
            Constants.PrimaryToRecovery,
            Constants.RecoveryToPrimary)]
        public string Direction { get; set; }

        #endregion Parameters

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteSiteRecoveryCmdlet()
        {
            base.ExecuteSiteRecoveryCmdlet();

            switch (this.ParameterSetName)
            {
                case ASRParameterSets.ByPEObject:
                    this.protectionContainerName = 
                        Utilities.GetValueFromArmId(this.ReplicationProtectedItem.ID, ARMResourceTypeConstants.ReplicationProtectionContainers);
                    this.fabricName = Utilities.GetValueFromArmId(this.ReplicationProtectedItem.ID, ARMResourceTypeConstants.ReplicationFabrics);
                    this.SetPEReprotect();
                    break;
                case ASRParameterSets.ByRPObject:
                    this.SetRPReprotect();
                    break;
            }
        }

        /// <summary>
        /// PE Reprotect.
        /// </summary>
        private void SetPEReprotect()
        {
            ReverseReplicationInputProperties plannedFailoverInputProperties = new ReverseReplicationInputProperties()
            {
                FailoverDirection = this.Direction,
                ProviderSpecificDetails = new ReverseReplicationProviderSpecificInput()
            };

            ReverseReplicationInput input = new ReverseReplicationInput()
            {
                Properties = plannedFailoverInputProperties
            };

            // fetch the latest Protectable item objects
            ReplicationProtectedItemResponse replicationProtectedItemResponse =
                        RecoveryServicesClient.GetAzureSiteRecoveryReplicationProtectedItem(this.fabricName,
                        this.protectionContainerName, this.ReplicationProtectedItem.Name);

            ProtectableItemResponse protectableItemResponse =
                        RecoveryServicesClient.GetAzureSiteRecoveryProtectableItem(this.fabricName, this.protectionContainerName,
                        Utilities.GetValueFromArmId(replicationProtectedItemResponse.ReplicationProtectedItem.Properties.ProtectableItemId, 
                        ARMResourceTypeConstants.ProtectableItems));

            var aSRProtectableItem = new ASRProtectableItem(protectableItemResponse.ProtectableItem);

            if (0 == string.Compare(
                this.ReplicationProtectedItem.ReplicationProvider,
                Constants.HyperVReplicaAzure,
                StringComparison.OrdinalIgnoreCase))
            {
                if (this.Direction == Constants.PrimaryToRecovery)
                {
                    HyperVReplicaAzureReprotectInput reprotectInput = new HyperVReplicaAzureReprotectInput()
                    {
                        HvHostVmId = aSRProtectableItem.FabricObjectId,
                        VmName = aSRProtectableItem.FriendlyName,
                        OSType = ((string.Compare(aSRProtectableItem.OS, "Windows") == 0) || 
                                    (string.Compare(aSRProtectableItem.OS, "Linux") == 0)) ? aSRProtectableItem.OS : "Windows",
                        VHDId = aSRProtectableItem.OSDiskId
                    };

                    HyperVReplicaAzureReplicationDetails providerSpecificDetails =
                           (HyperVReplicaAzureReplicationDetails)replicationProtectedItemResponse.ReplicationProtectedItem.Properties.ProviderSpecificDetails;

                    reprotectInput.StorageAccountId = providerSpecificDetails.RecoveryAzureStorageAccount;

                    input.Properties.ProviderSpecificDetails = reprotectInput;
                }
            }

            LongRunningOperationResponse response =
                RecoveryServicesClient.StartAzureSiteRecoveryReprotection(
                this.fabricName,
                this.protectionContainerName,
                this.ReplicationProtectedItem.Name,
                input);

            JobResponse jobResponse =
                RecoveryServicesClient
                .GetAzureSiteRecoveryJobDetails(PSRecoveryServicesClient.GetJobIdFromReponseLocation(response.Location));

            WriteObject(new ASRJob(jobResponse.Job));
        }

        /// <summary>
        /// Starts RP Reprotect.
        /// </summary>
        private void SetRPReprotect()
        {
            LongRunningOperationResponse response = RecoveryServicesClient.UpdateAzureSiteRecoveryProtection(
                this.RecoveryPlan.Name);

            JobResponse jobResponse =
                RecoveryServicesClient
                .GetAzureSiteRecoveryJobDetails(PSRecoveryServicesClient.GetJobIdFromReponseLocation(response.Location));

            WriteObject(new ASRJob(jobResponse.Job));
        }
    }
}