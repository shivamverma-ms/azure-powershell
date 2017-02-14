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
using System.ComponentModel;
using System.Management.Automation;

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

        #region Parameters

        /// <summary>
        /// Gets or sets Recovery Plan object.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.ByRPObject, Mandatory = true, ValueFromPipeline = true)]
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzureAndVMwareToVMwareRP, Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRRecoveryPlan RecoveryPlan { get; set; }

        /// <summary>
        /// Gets or sets Protection Entity Object.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.ByPEObject, Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRProtectionEntity ProtectionEntity { get; set; }

        /// <summary>
        /// Gets or sets Replication Protected Item.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.ByRPIObject, Mandatory = true, ValueFromPipeline = true)]
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzureRPI, Mandatory = true, ValueFromPipeline = true)]
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToVMwareRPI, Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRReplicationProtectedItem ReplicationProtectedItem { get; set; }

        /// <summary>
        /// Gets or sets Policy.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzureRPI, Mandatory = true)]
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToVMwareRPI, Mandatory = true)]
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzureAndVMwareToVMwareRP, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public ASRProtectionContainerMapping ProtectionContainerMapping { get; set; }

        /// <summary>
        /// Gets or sets Failover direction for the recovery plan.
        /// </summary>
        [Parameter(Mandatory = true)]
        [ValidateSet(
            Constants.PrimaryToRecovery,
            Constants.RecoveryToPrimary)]
        public string Direction { get; set; }

        /// <summary>
        /// Gets or sets Process Server.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzureRPI, Mandatory = true)]
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToVMwareRPI, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public ASRProcessServer ProcessServer { get; set; }

        /// <summary>
        /// Gets or sets Master Target Server.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzureRPI)]
        [ValidateNotNullOrEmpty]
        public ASRMasterTargetServer MasterTarget { get; set; }

        /// <summary>
        /// Gets or sets RunAsAccount.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzureRPI)]
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToVMwareRPI, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public ASRRunAsAccount Account { get; set; }

        /// <summary>
        /// Gets or sets Retention Volume.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzureRPI, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string RetentionVolume { get; set; }

        /// <summary>
        /// Gets or sets DataStore.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzureRPI, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string DataStore { get; set; }

        /// <summary>
        /// Gets or sets Recovery Azure Storage Account Name of the Policy for V2A scenarios.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToVMwareRPI)]
        public string RecoveryAzureStorageAccountId { get; set; }

        /// <summary>
        /// Gets or sets Recovery Azure Log Storage Account Id.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToVMwareRPI)]
        [ValidateNotNullOrEmpty]
        public string RecoveryAzureLogStorageAccountId { get; set; }

        /// <summary>
        /// Gets or sets Volumes to Exclude.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzureRPI)]
        [ValidateNotNullOrEmpty]
        public string[] ExcludeVolumeLabels { get; set; }

        /// <summary>
        /// Gets or sets OnlyExcludeIfSingleVolume Flag.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzureRPI)]
        [DefaultValue(true)]
        [ValidateNotNullOrEmpty]
        public bool OnlyExcludeIfSingleVolume { get; set; }

        /// <summary>
        /// Gets or sets Disk Signatures to Exclude.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzureRPI)]
        [ValidateNotNullOrEmpty]
        public string[] ExcludeDiskSignatures { get; set; }

        /// <summary>
        /// Gets or sets Disks to Include.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzureRPI)]
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToVMwareRPI)]
        [ValidateNotNullOrEmpty]
        public string[] IncludeDiskIds { get; set; }

        /// <summary>
        /// Gets or sets switch parameter. On passing, command waits till completion.
        /// </summary>
        [Parameter]
        public SwitchParameter WaitForCompletion { get; set; }

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
                    this.WriteWarningWithTimestamp(Properties.Resources.ParameterSetWillBeDeprecatedSoon);
                    this.protectionEntityName = this.ProtectionEntity.Name;
                    this.protectionContainerName = this.ProtectionEntity.ProtectionContainerId;
                    this.fabricName = Utilities.GetValueFromArmId(this.ProtectionEntity.ID, ARMResourceTypeConstants.ReplicationFabrics);
                    this.SetPEReprotect();
                    break;
                case ASRParameterSets.ByRPIObject:
                case ASRParameterSets.VMwareToAzureRPI:
                case ASRParameterSets.VMwareToVMwareRPI:
                    this.protectionContainerName =
                        Utilities.GetValueFromArmId(this.ReplicationProtectedItem.ID, ARMResourceTypeConstants.ReplicationProtectionContainers);
                    this.fabricName = Utilities.GetValueFromArmId(this.ReplicationProtectedItem.ID, ARMResourceTypeConstants.ReplicationFabrics);
                    this.SetRPIReprotect();
                    break;
                case ASRParameterSets.ByRPObject:
                case ASRParameterSets.VMwareToAzureAndVMwareToVMwareRP:
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
                    HyperVReplicaAzureReprotectInput reprotectInput = new HyperVReplicaAzureReprotectInput()
                    {
                        HvHostVmId = this.ProtectionEntity.FabricObjectId,
                        VmName = this.ProtectionEntity.FriendlyName,
                        OSType = ((string.Compare(this.ProtectionEntity.OS, "Windows") == 0) || (string.Compare(this.ProtectionEntity.OS, "Linux") == 0)) ? this.ProtectionEntity.OS : "Windows",
                        VHDId = this.ProtectionEntity.OSDiskId
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
                Utilities.GetValueFromArmId(replicationProtectedItemResponse.ReplicationProtectedItem.Id, ARMResourceTypeConstants.ReplicationProtectedItems),
                input);

            JobResponse jobResponse =
                RecoveryServicesClient
                .GetAzureSiteRecoveryJobDetails(PSRecoveryServicesClient.GetJobIdFromReponseLocation(response.Location));

            WriteObject(new ASRJob(jobResponse.Job));
        }

        /// <summary>
        /// RPI Reprotect.
        /// </summary>
        private void SetRPIReprotect()
        {
            ReverseReplicationInputProperties reprotectInputProperties = new ReverseReplicationInputProperties()
            {
                FailoverDirection = this.Direction,
                ProviderSpecificDetails = new ReverseReplicationProviderSpecificInput()
            };

            ReverseReplicationInput input = new ReverseReplicationInput()
            {
                Properties = reprotectInputProperties
            };

            // Fetch the latest Protectable item objects
            ReplicationProtectedItemResponse replicationProtectedItemResponse =
                        RecoveryServicesClient.GetAzureSiteRecoveryReplicationProtectedItem(this.fabricName,
                        this.protectionContainerName, this.ReplicationProtectedItem.Name);

            ProtectableItemResponse protectableItemResponse =
                        RecoveryServicesClient.GetAzureSiteRecoveryProtectableItem(this.fabricName, this.protectionContainerName,
                        Utilities.GetValueFromArmId(replicationProtectedItemResponse.ReplicationProtectedItem.Properties.ProtectableItemId,
                        ARMResourceTypeConstants.ProtectableItems));

            var aSRProtectableItem = new ASRProtectableItem(protectableItemResponse.ProtectableItem);

            // Validate the Replication Provider.
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
            else if (string.Compare(
                        this.ReplicationProtectedItem.ReplicationProvider,
                        Constants.InMageAzureV2,
                        StringComparison.OrdinalIgnoreCase) == 0)
            {
                // Validate the Direction as RecoveryToPrimary.
                if (this.Direction == Constants.RecoveryToPrimary)
                {
                    // Set the InMage Provider specific input in the Reprotect Input.
                    InMageReprotectInput reprotectInput = new InMageReprotectInput()
                    {
                        ProcessServerId = this.ProcessServer.Id,
                        MasterTargetId = (this.MasterTarget != null) ?
                            this.MasterTarget.Id : this.ProcessServer.Id, // Assumption: PS and MT may or may not be same.
                        RunAsAccountId = (this.Account != null) ? this.Account.AccountId : null,
                        RetentionDrive = this.RetentionVolume,
                        DatastoreName = this.DataStore,
                        ProfileId = this.ProtectionContainerMapping.PolicyId,
                        DiskExclusionInput = new InMageDiskExclusionInput()
                        {
                            VolumeOptions = new List<InMageVolumeExclusionOptions>(),
                            DiskSignatureOptions = (this.ExcludeDiskSignatures != null) ?
                                new List<InMageDiskSignatureExclusionOptions>() : null,
                        },
                        DisksToInclude = (this.IncludeDiskIds != null) ?
                            new List<string>(this.IncludeDiskIds) : null
                    };

                    // excluding the azure temporary storage.
                    reprotectInput.DiskExclusionInput.VolumeOptions.Add(new InMageVolumeExclusionOptions
                    {
                        VolumeLabel = Constants.TemporaryStorage,
                        OnlyExcludeIfSingleVolume = Constants.Yes
                    });

                    input.Properties.ProviderSpecificDetails = reprotectInput;

                    // Update the Disk Exclusion Input.
                    if (this.ExcludeVolumeLabels != null)
                    {
                        foreach (var vol in this.ExcludeVolumeLabels)
                        {
                            InMageVolumeExclusionOptions volToExclude = new InMageVolumeExclusionOptions()
                            {
                                VolumeLabel = vol,
                                OnlyExcludeIfSingleVolume = this.OnlyExcludeIfSingleVolume.ToString()
                            };
                            reprotectInput.DiskExclusionInput.VolumeOptions.Add(volToExclude);
                        }
                    }

                    if (this.ExcludeDiskSignatures != null)
                    {
                        foreach (var sig in this.ExcludeDiskSignatures)
                        {
                            InMageDiskSignatureExclusionOptions sigToExclude =
                                new InMageDiskSignatureExclusionOptions()
                                {
                                    DiskSignature = sig
                                };
                            reprotectInput.DiskExclusionInput.DiskSignatureOptions.Add(sigToExclude);
                        }
                    }
                }
                else
                {
                    // PrimaryToRecovery Direction is Invalid for InMageAzureV2.
                    new ArgumentException(Properties.Resources.InvalidDirectionForAzureToVMWare);
                }
            }
            else if (string.Compare(
                        this.ReplicationProtectedItem.ReplicationProvider,
                        Constants.InMage,
                        StringComparison.OrdinalIgnoreCase) == 0)
            {
                // Validate the Direction as RecoveryToPrimary.
                if (this.Direction == Constants.RecoveryToPrimary)
                {
                    // Set the InMageAzureV2 Provider specific input in the Reprotect Input.
                    InMageAzureV2ReprotectInput reprotectInput = new InMageAzureV2ReprotectInput()
                    {
                        ProcessServerId = this.ProcessServer.Id,
                        MasterTargetId = this.ProcessServer.Id, // Assumption: PS and MT are same. 
                        RunAsAccountId = this.Account.AccountId,
                        PolicyId = this.ProtectionContainerMapping.PolicyId,
                        StorageAccountId = this.RecoveryAzureStorageAccountId,
                        LogStorageAccountId = this.RecoveryAzureLogStorageAccountId,
                        DisksToInclude = (this.IncludeDiskIds != null) ?
                            new List<string>(this.IncludeDiskIds) : null
                    };
                    input.Properties.ProviderSpecificDetails = reprotectInput;
                }
                else
                {
                    // PrimaryToRecovery Direction is Invalid for InMage.
                    new ArgumentException(Properties.Resources.InvalidDirectionForVMWareToAzure);
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

            // Wait for the Reprotect Operation to Complete.
            if (this.WaitForCompletion.IsPresent)
            {
                this.WaitForJobCompletion(jobResponse.Job.Name);

                jobResponse =
                    RecoveryServicesClient.GetAzureSiteRecoveryJobDetails(
                        PSRecoveryServicesClient.GetJobIdFromReponseLocation(response.Location));

                WriteObject(new ASRJob(jobResponse.Job));
            }
        }

        /// <summary>
        /// Starts RP Reprotect.
        /// </summary>
        private void SetRPReprotect()
        {
            // Check if the Recovery Plan contains any InMageAzureV2 and InMage Replication Provider Entities.
            var rp = RecoveryServicesClient.GetAzureSiteRecoveryRecoveryPlan(this.RecoveryPlan.Name);
            foreach (string replicationProvider in rp.RecoveryPlan.Properties.ReplicationProviders)
            {
                if ((string.Compare(
                        replicationProvider,
                        Constants.InMageAzureV2,
                        StringComparison.OrdinalIgnoreCase) == 0) ||
                    (string.Compare(
                        replicationProvider,
                        Constants.InMage,
                        StringComparison.OrdinalIgnoreCase) == 0))
                {
                    throw new InvalidOperationException(
                        string.Format(
                            Properties.Resources.UnsupportedReplicationProviderForReprotect.ToString(),
                            replicationProvider));
                }
            }

            LongRunningOperationResponse response = RecoveryServicesClient.UpdateAzureSiteRecoveryProtection(
                this.RecoveryPlan.Name);

            JobResponse jobResponse =
                RecoveryServicesClient
                .GetAzureSiteRecoveryJobDetails(PSRecoveryServicesClient.GetJobIdFromReponseLocation(response.Location));

            WriteObject(new ASRJob(jobResponse.Job));

            // Wait for the Reprotect Operation to Complete.
            if (this.WaitForCompletion.IsPresent)
            {
                this.WaitForJobCompletion(jobResponse.Job.Name);

                jobResponse =
                    RecoveryServicesClient.GetAzureSiteRecoveryJobDetails(
                        PSRecoveryServicesClient.GetJobIdFromReponseLocation(response.Location));

                WriteObject(new ASRJob(jobResponse.Job));
            }
        }
    }
}