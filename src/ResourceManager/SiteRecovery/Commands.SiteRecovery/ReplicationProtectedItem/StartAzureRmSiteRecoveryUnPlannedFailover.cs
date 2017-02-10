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
using System.IO;
using System.Management.Automation;
using Microsoft.Azure.Management.SiteRecovery.Models;

namespace Microsoft.Azure.Commands.SiteRecovery
{
    /// <summary>
    /// Used to initiate a failover operation.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "AzureRmSiteRecoveryUnplannedFailover", DefaultParameterSetName = ASRParameterSets.ByPEObject)]
    [OutputType(typeof(ASRJob))]
    public class StartAzureRmSiteRecoveryUnplannedFailover : SiteRecoveryCmdletBase
    {
        #region local parameters

        /// <summary>
        /// Gets or sets Name of the PE.
        /// </summary>
        public string protectionEntityName;

        /// <summary>
        /// Gets or sets Name of the Protection Container.
        /// </summary>
        public string protectionContainerName;

        /// <summary>
        /// Gets or sets Name of the Fabric.
        /// </summary>
        public string fabricName;

        /// <summary>
        /// Primary Kek Cert pfx file.
        /// </summary>
        string primaryKekCertpfx = null;

        /// <summary>
        /// Secondary Kek Cert pfx file.
        /// </summary>
        string secondaryKekCertpfx = null;

        #endregion local parameters

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
        [Parameter(Mandatory = true)]
        [ValidateSet(Constants.PrimaryToRecovery, Constants.RecoveryToPrimary)]
        public string Direction { get; set; }

        /// <summary>
        /// Gets or sets switch parameter. This is required to PerformSourceSideActions.
        /// </summary>
        [Parameter]
        public SwitchParameter PerformSourceSideActions { get; set; }

        /// <summary>
        /// Gets or sets Data encryption certificate file path for failover of Protected Item.
        /// </summary>
        [Parameter]
        [ValidateNotNullOrEmpty]
        public string DataEncryptionPrimaryCertFile { get; set; }

        /// <summary>
        /// Gets or sets Data encryption certificate file path for failover of Protected Item.
        /// </summary>
        [Parameter]
        [ValidateNotNullOrEmpty]
        public string DataEncryptionSecondaryCertFile { get; set; }

        /// <summary>
        /// Gets or sets Recovery Point object.
        /// </summary>
        [Parameter]
        [ValidateNotNullOrEmpty]
        public ASRRecoveryPoint RecoveryPoint { get; set; }

        /// <summary>
        /// Gets or Sets recovery point type.
        /// </summary>
        [Parameter]
        [ValidateNotNullOrEmpty]
        [ValidateSet(
            Constants.Latest,
            Constants.LatestProcessed,
            Constants.LatestApplicationConsistent,
            Constants.LatestCrashConsistent)]
        public string RecoveryPointType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use recovery cloud service or 
        /// create new cloud service for test failover.
        /// </summary>
        [Parameter]
        [ValidateNotNullOrEmpty]
        [ValidateSet(Constants.UseRecoveryCloudService, Constants.AutoCreateCloudService)]
        public string CloudServiceCreationOption { get; set; }

        #endregion Parameters

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteSiteRecoveryCmdlet()
        {
            base.ExecuteSiteRecoveryCmdlet();

            if (!string.IsNullOrEmpty(this.DataEncryptionPrimaryCertFile))
            {
                byte[] certBytesPrimary = File.ReadAllBytes(this.DataEncryptionPrimaryCertFile);
                primaryKekCertpfx = Convert.ToBase64String(certBytesPrimary);
            }

            if (!string.IsNullOrEmpty(this.DataEncryptionSecondaryCertFile))
            {
                byte[] certBytesSecondary = File.ReadAllBytes(this.DataEncryptionSecondaryCertFile);
                secondaryKekCertpfx = Convert.ToBase64String(certBytesSecondary);
            }

            switch (this.ParameterSetName)
            {
                case ASRParameterSets.ByPEObject:
                    this.protectionContainerName = 
                        Utilities.GetValueFromArmId(this.ReplicationProtectedItem.ID, ARMResourceTypeConstants.ReplicationProtectionContainers);
                    this.fabricName = Utilities.GetValueFromArmId(this.ReplicationProtectedItem.ID, ARMResourceTypeConstants.ReplicationFabrics);
                    this.StartPEUnplannedFailover();
                    break;
                case ASRParameterSets.ByRPObject:
                    this.StartRpUnplannedFailover();
                    break;
            }
        }

        /// <summary>
        /// Starts PE Unplanned failover.
        /// </summary>
        private void StartPEUnplannedFailover()
        {
            var unplannedFailoverInputProperties = new UnplannedFailoverInputProperties()
            {
                FailoverDirection = this.Direction,
                SourceSiteOperations = this.PerformSourceSideActions ? "Required" : "NotRequired",
                ProviderSpecificDetails = new ProviderSpecificFailoverInput()
            };

            var input = new UnplannedFailoverInput()
            {
                Properties = unplannedFailoverInputProperties
            };

            if (0 == string.Compare(
                this.ReplicationProtectedItem.ReplicationProvider,
                Constants.HyperVReplicaAzure,
                StringComparison.OrdinalIgnoreCase))
            {
                if (this.Direction == Constants.PrimaryToRecovery)
                {
                    var failoverInput = new HyperVReplicaAzureFailoverProviderInput()
                    {
                        PrimaryKekCertificatePfx = primaryKekCertpfx,
                        SecondaryKekCertificatePfx = secondaryKekCertpfx,
                        VaultLocation = this.GetCurrentVaultLocation()
                    };

                    input.Properties.ProviderSpecificDetails = failoverInput;
                }
            }
            else if(0 == string.Compare(
                this.ReplicationProtectedItem.ReplicationProvider,
                Constants.AzureToAzure,
                StringComparison.OrdinalIgnoreCase))
            {
                var failoverInput = new A2AFailoverProviderInput()
                {
                    RecoveryPointId = this.RecoveryPoint != null ? this.RecoveryPoint.ID : null
                };

                input.Properties.ProviderSpecificDetails = failoverInput;
            }

            LongRunningOperationResponse response =
                RecoveryServicesClient.StartAzureSiteRecoveryUnplannedFailover(
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
        /// Starts RP Unplanned failover.
        /// </summary>
        private void StartRpUnplannedFailover()
        {
            // Refresh RP Object
            var rp = RecoveryServicesClient.GetAzureSiteRecoveryRecoveryPlan(this.RecoveryPlan.Name);

            var recoveryPlanUnplannedFailoverInputProperties = new RecoveryPlanUnplannedFailoverInputProperties()
            {
                FailoverDirection = this.Direction,
                SourceSiteOperations = this.PerformSourceSideActions ? "Required" : "NotRequired", //Required|NotRequired
                ProviderSpecificDetails = new List<RecoveryPlanProviderSpecificFailoverInput>()
            };

            foreach (string replicationProvider in rp.RecoveryPlan.Properties.ReplicationProviders)
            {
                if (0 == string.Compare(
                    replicationProvider,
                    Constants.HyperVReplicaAzure,
                    StringComparison.OrdinalIgnoreCase))
                {
                    if (this.Direction == Constants.PrimaryToRecovery)
                    {
                        var recoveryPlanHyperVReplicaAzureFailoverInput = new RecoveryPlanHyperVReplicaAzureFailoverInput()
                        {
                            InstanceType = replicationProvider,
                            PrimaryKekCertificatePfx = primaryKekCertpfx,
                            SecondaryKekCertificatePfx = secondaryKekCertpfx,
                            VaultLocation = this.GetCurrentVaultLocation()
                        };
                        recoveryPlanUnplannedFailoverInputProperties.ProviderSpecificDetails.Add(recoveryPlanHyperVReplicaAzureFailoverInput);
                    }
                }

                else if (0 == string.Compare(
                    replicationProvider,
                    Constants.AzureToAzure,
                    StringComparison.OrdinalIgnoreCase))
                {
                    var recoveryPlanA2AFailoverInput = new RecoveryPlanA2AFailoverInput()
                    {
                        InstanceType = replicationProvider,
                        RecoveryPointType =
                            string.IsNullOrEmpty(this.RecoveryPointType) ?
                            Constants.Latest :
                            this.RecoveryPointType,
                        CloudServiceCreationOption =
                        string.IsNullOrEmpty(this.CloudServiceCreationOption) ?
                            Constants.UseRecoveryCloudService :
                            this.CloudServiceCreationOption
                    };
                    recoveryPlanUnplannedFailoverInputProperties.ProviderSpecificDetails.Add(recoveryPlanA2AFailoverInput);
                }
            }

            var recoveryPlanUnplannedFailoverInput = new RecoveryPlanUnplannedFailoverInput()
            {
                Properties = recoveryPlanUnplannedFailoverInputProperties
            };

            LongRunningOperationResponse response = RecoveryServicesClient.StartAzureSiteRecoveryUnplannedFailover(
                this.RecoveryPlan.Name,
                recoveryPlanUnplannedFailoverInput);

            JobResponse jobResponse =
                RecoveryServicesClient
                .GetAzureSiteRecoveryJobDetails(PSRecoveryServicesClient.GetJobIdFromReponseLocation(response.Location));

            WriteObject(new ASRJob(jobResponse.Job));
        }
    }
}