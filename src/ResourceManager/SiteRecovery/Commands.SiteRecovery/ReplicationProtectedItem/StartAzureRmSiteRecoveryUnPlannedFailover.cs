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
using System.IO;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.SiteRecovery
{
    /// <summary>
    /// Used to initiate a failover operation.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "AzureRmSiteRecoveryUnplannedFailoverJob", DefaultParameterSetName = ASRParameterSets.ByPEObject)]
    [OutputType(typeof(ASRJob))]
    public class StartAzureRmSiteRecoveryUnplannedFailoverJob : SiteRecoveryCmdletBase
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
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzureAndVMwareToVMwareRP, Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRRecoveryPlan RecoveryPlan { get; set; }

        /// <summary>
        /// Gets or sets Protection Entity object.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.ByPEObject, Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRProtectionEntity ProtectionEntity { get; set; }

        /// <summary>
        /// Gets or sets Replication Protected Item.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.ByRPIObject, Mandatory = true, ValueFromPipeline = true)]
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzureAndVMwareToVMwareRPI, Mandatory = true, ValueFromPipeline = true)]
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
        [Parameter(ParameterSetName = ASRParameterSets.ByRPObject)]
        [Parameter(ParameterSetName = ASRParameterSets.ByPEObject)]
        [Parameter(ParameterSetName = ASRParameterSets.ByRPIObject)]
        [ValidateNotNullOrEmpty]
        public string DataEncryptionPrimaryCertFile { get; set; }

        /// <summary>
        /// Gets or sets Data encryption certificate file path for failover of Protected Item.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.ByRPObject)]
        [Parameter(ParameterSetName = ASRParameterSets.ByPEObject)]
        [Parameter(ParameterSetName = ASRParameterSets.ByRPIObject)]
        [ValidateNotNullOrEmpty]
        public string DataEncryptionSecondaryCertFile { get; set; }

        /// <summary> 
        /// Gets or sets Recovery Point object. 
        /// </summary> 
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzureAndVMwareToVMwareRPI)]
        [ValidateNotNullOrEmpty]
        public ASRRecoveryPoint RecoveryPoint { get; set; }

        /// <summary> 
        /// Gets or sets Recovery Tag for the Recovery Point Type. 
        /// </summary> 
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzureAndVMwareToVMwareRPI)]
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzureAndVMwareToVMwareRP)]
        [ValidateNotNullOrEmpty]
        [ValidateSet(
            Constants.RecoveryTagLatest,
            Constants.RecoveryTagLatestAvailable,
            Constants.RecoveryTagLatestAvailableApplicationConsistent)]
        public string RecoveryTag { get; set; }

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
                    this.WriteWarningWithTimestamp(Properties.Resources.ParameterSetWillBeDeprecatedSoon);
                    this.protectionEntityName = this.ProtectionEntity.Name;
                    this.protectionContainerName = this.ProtectionEntity.ProtectionContainerId;
                    this.fabricName = Utilities.GetValueFromArmId(this.ProtectionEntity.ID, ARMResourceTypeConstants.ReplicationFabrics);
                    this.StartPEUnplannedFailover();
                    break;
                case ASRParameterSets.ByRPIObject:
                case ASRParameterSets.VMwareToAzureAndVMwareToVMwareRPI:
                    this.protectionContainerName =
                        Utilities.GetValueFromArmId(this.ReplicationProtectedItem.ID, ARMResourceTypeConstants.ReplicationProtectionContainers);
                    this.fabricName = Utilities.GetValueFromArmId(this.ReplicationProtectedItem.ID, ARMResourceTypeConstants.ReplicationFabrics);
                    this.StartRPIUnplannedFailover();
                    break;
                case ASRParameterSets.ByRPObject:
                case ASRParameterSets.VMwareToAzureAndVMwareToVMwareRP:
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
                SourceSiteOperations = this.PerformSourceSideActions ? "Required" : "NotRequired", //Required|NotRequired
                ProviderSpecificDetails = new ProviderSpecificFailoverInput()
            };

            var input = new UnplannedFailoverInput()
            {
                Properties = unplannedFailoverInputProperties
            };

            // fetch the latest PE object
            ProtectableItemResponse protectableItemResponse =
                                        RecoveryServicesClient.GetAzureSiteRecoveryProtectableItem(this.fabricName,
                                        this.ProtectionEntity.ProtectionContainerId, this.ProtectionEntity.Name);

            ReplicationProtectedItemResponse replicationProtectedItemResponse =
                        RecoveryServicesClient.GetAzureSiteRecoveryReplicationProtectedItem(this.fabricName,
                        this.ProtectionEntity.ProtectionContainerId, Utilities.GetValueFromArmId(protectableItemResponse.ProtectableItem.Properties.ReplicationProtectedItemId, ARMResourceTypeConstants.ReplicationProtectedItems));

            PolicyResponse policyResponse = RecoveryServicesClient.GetAzureSiteRecoveryPolicy(Utilities.GetValueFromArmId(replicationProtectedItemResponse.ReplicationProtectedItem.Properties.PolicyID, ARMResourceTypeConstants.ReplicationPolicies));

            this.ProtectionEntity = new ASRProtectionEntity(protectableItemResponse.ProtectableItem, replicationProtectedItemResponse.ReplicationProtectedItem);

            if (0 == string.Compare(
                this.ProtectionEntity.ReplicationProvider,
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

            LongRunningOperationResponse response =
                RecoveryServicesClient.StartAzureSiteRecoveryUnplannedFailover(
                this.fabricName,
                this.protectionContainerName,
                Utilities.GetValueFromArmId(replicationProtectedItemResponse.ReplicationProtectedItem.Id, ARMResourceTypeConstants.ReplicationProtectedItems),
                input);

            JobResponse jobResponse =
                RecoveryServicesClient
                .GetAzureSiteRecoveryJobDetails(PSRecoveryServicesClient.GetJobIdFromReponseLocation(response.Location));

            WriteObject(new ASRJob(jobResponse.Job));
        }

        /// <summary>
        /// Starts RPI Unplanned failover.
        /// </summary>
        private void StartRPIUnplannedFailover()
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

            // Validate the Replication Provider.
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
            else if (string.Compare(
                        this.ReplicationProtectedItem.ReplicationProvider,
                        Constants.InMageAzureV2,
                        StringComparison.OrdinalIgnoreCase) == 0)
            {
                // Validate if the Replication Protection Item is part of any Replication Group.
                Guid guidResult;
                bool parseFlag = Guid.TryParse(
                    ((ASRInMageAzureV2SpecificRPIDetails)
                        this.ReplicationProtectedItem.ProviderSpecificRPIDetails).MultiVmGroupName,
                        out guidResult);
                if (parseFlag == false ||
                    guidResult == Guid.Empty ||
                    (string.Compare(
                        ((ASRInMageAzureV2SpecificRPIDetails)
                            this.ReplicationProtectedItem.ProviderSpecificRPIDetails).MultiVmGroupName,
                        ((ASRInMageAzureV2SpecificRPIDetails)
                            this.ReplicationProtectedItem.ProviderSpecificRPIDetails).MultiVmGroupId) != 0))
                {
                    // Replication Group was created at the time of Protection.
                    throw new InvalidOperationException(
                        string.Format(
                            Properties.Resources.UnsupportedReplicationProtectionActionForUnplannedFailover.ToString(),
                            this.ReplicationProtectedItem.ReplicationProvider));
                }

                // Validate the Direction as PrimaryToRecovery.
                if (this.Direction == Constants.PrimaryToRecovery)
                {
                    // Set the InMageAzureV2 Provider specific input in the Unplanned Failover Input.
                    var failoverInput = new InMageAzureV2FailoverProviderInput()
                    {
                        VaultLocation = this.GetCurrentVaultLocation(),
                        RecoveryPointId = this.RecoveryPoint != null ? this.RecoveryPoint.ID : null
                    };
                    input.Properties.ProviderSpecificDetails = failoverInput;
                }
                else
                {
                    // RecoveryToPrimary Direction is Invalid for InMageAzureV2.
                    new ArgumentException(Properties.Resources.InvalidDirectionForVMWareToAzure);
                }
            }
            else if (string.Compare(
                        this.ReplicationProtectedItem.ReplicationProvider,
                        Constants.InMage,
                        StringComparison.OrdinalIgnoreCase) == 0)
            {
                // Validate if the Replication Protection Item is part of any Replication Group.
                Guid guidResult;
                bool parseFlag = Guid.TryParse(
                    (((ASRInMageSpecificRPIDetails)
                        this.ReplicationProtectedItem.ProviderSpecificRPIDetails).MultiVmGroupName),
                        out guidResult);
                if (parseFlag == false ||
                    guidResult == Guid.Empty ||
                    (string.Compare(
                        ((ASRInMageSpecificRPIDetails)
                            this.ReplicationProtectedItem.ProviderSpecificRPIDetails).MultiVmGroupName,
                        ((ASRInMageSpecificRPIDetails)
                            this.ReplicationProtectedItem.ProviderSpecificRPIDetails).MultiVmGroupId) != 0))
                {
                    // Replication Group was created at the time of Protection.
                    throw new InvalidOperationException(
                        string.Format(
                            Properties.Resources.UnsupportedReplicationProtectionActionForUnplannedFailover.ToString(),
                            this.ReplicationProtectedItem.ReplicationProvider));
                }

                // Validate the Direction as PrimaryToRecovery.
                if (this.Direction == Constants.PrimaryToRecovery)
                {
                    // Set the Recovery Point Types for InMage.
                    var recoveryPointType =
                        (this.RecoveryPoint != null) ? InMageRecoveryTag.Custom.ToString() :
                            (this.RecoveryTag == Constants.RecoveryTagLatestAvailableApplicationConsistent) ?
                                InMageRecoveryTag.LatestTag.ToString() :
                                InMageRecoveryTag.LatestTime.ToString();

                    // Set the InMage Provider specific input in the Unplanned Failover Input.
                    var failoverInput = new InMageFailoverProviderInput()
                    {
                        RecoveryPointType = recoveryPointType,
                        RecoveryPointId = this.RecoveryPoint != null ? this.RecoveryPoint.ID : null
                    };
                    input.Properties.ProviderSpecificDetails = failoverInput;
                }
                else
                {
                    // RecoveryToPrimary Direction is Invalid for InMage.
                    new ArgumentException(Properties.Resources.InvalidDirectionForAzureToVMWare);
                }
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
                // Validate the Replication Provider.
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
                else if (string.Compare(
                            replicationProvider,
                            Constants.InMageAzureV2,
                            StringComparison.OrdinalIgnoreCase) == 0)
                {
                    // Check if the Direction is PrimaryToRecovery.
                    if (this.Direction == Constants.PrimaryToRecovery)
                    {
                        // Set the Recovery Point Types for InMage.
                        var recoveryPointType =
                            (this.RecoveryTag == Constants.RecoveryTagLatestAvailableApplicationConsistent) ?
                                InMageAzureV2RecoveryTag.LatestApplicationConsistent.ToString() :
                                    (this.RecoveryTag == Constants.RecoveryTagLatestAvailable) ?
                                        InMageAzureV2RecoveryTag.LatestProcessed.ToString() :
                                        InMageAzureV2RecoveryTag.Latest.ToString();

                        // Create the InMageAzureV2 Provider specific input.
                        var recoveryPlanInMageAzureV2FailoverInput = new RecoveryPlanInMageAzureV2FailoverInput()
                        {
                            InstanceType = replicationProvider,
                            VaultLocation = this.GetCurrentVaultLocation(),
                            RecoveryPointType = recoveryPointType
                        };

                        // Add the InMageAzureV2 Provider specific input in the Planned Failover Input.
                        recoveryPlanUnplannedFailoverInputProperties.ProviderSpecificDetails.Add(
                            recoveryPlanInMageAzureV2FailoverInput);
                    }
                }
                else if (string.Compare(
                            replicationProvider,
                            Constants.InMage,
                            StringComparison.OrdinalIgnoreCase) == 0)
                {
                    // Check if the Direction is RecoveryToPrimary.
                    if (this.Direction == Constants.RecoveryToPrimary)
                    {
                        // Set the Recovery Point Types for InMage.
                        var recoveryPointType =
                            (this.RecoveryTag == Constants.RecoveryTagLatestAvailableApplicationConsistent) ?
                                InMageRecoveryTag.LatestTag.ToString() :
                                InMageRecoveryTag.LatestTime.ToString();

                        // Create the InMage Provider specific input.
                        var recoveryPlanInMageFailoverInput = new RecoveryPlanInMageFailoverInput()
                        {
                            InstanceType = replicationProvider,
                            RecoveryPointType = recoveryPointType
                        };

                        // Add the InMage Provider specific input in the Planned Failover Input.
                        recoveryPlanUnplannedFailoverInputProperties.ProviderSpecificDetails.Add(
                            recoveryPlanInMageFailoverInput);
                    }
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