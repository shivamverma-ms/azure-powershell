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
using System.Management.Automation;

namespace Microsoft.Azure.Commands.SiteRecovery
{
    /// <summary>
    /// Set Protection Entity protection state.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "AzureRmSiteRecoveryReplicationProtectedItem", DefaultParameterSetName = ASRParameterSets.DisableDR, SupportsShouldProcess = true)]
    [OutputType(typeof(ASRJob))]
    public class NewAzureRmSiteRecoveryReplicationProtectedItem : SiteRecoveryCmdletBase
    {
        /// <summary>
        /// Job response.
        /// </summary>
        private LongRunningOperationResponse response = null;

        JobResponse jobResponse = null;

        #region Parameters

        /// <summary>
        /// Gets or sets Replication Protected Item.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToEnterprise, Mandatory = true, ValueFromPipeline = true)]
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToAzure, Mandatory = true, ValueFromPipeline = true)]
        [Parameter(ParameterSetName = ASRParameterSets.HyperVSiteToAzure, Mandatory = true, ValueFromPipeline = true)]
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzure, Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRProtectableItem ProtectableItem { get; set; }

        /// <summary>
        /// Gets or sets Replication Protected Item Name.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToEnterprise, Mandatory = true)]
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToAzure, Mandatory = true)]
        [Parameter(ParameterSetName = ASRParameterSets.HyperVSiteToAzure, Mandatory = true)]
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzure, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Policy.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToEnterprise, Mandatory = true)]
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToAzure, Mandatory = true)]
        [Parameter(ParameterSetName = ASRParameterSets.HyperVSiteToAzure, Mandatory = true)]
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzure, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public ASRProtectionContainerMapping ProtectionContainerMapping { get; set; }

        /// <summary>
        /// Gets or sets Recovery Azure Storage Account Name of the Policy for E2A, B2A and V2A scenarios.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToAzure, Mandatory = true)]
        [Parameter(ParameterSetName = ASRParameterSets.HyperVSiteToAzure, Mandatory = true)]
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzure, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string RecoveryAzureStorageAccountId { get; set; }

        /// <summary>
        /// Gets or sets OS disk name.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.HyperVSiteToAzure, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string OSDiskName { get; set; }

        /// <summary>
        /// Gets or sets OS Type
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.HyperVSiteToAzure, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [ValidateSet(
            Constants.OSWindows,
            Constants.OSLinux)]
        public string OS { get; set; }

        /// <summary>
        /// Gets or sets Process Server.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzure, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public ASRProcessServer ProcessServer { get; set; }

        /// <summary>
        /// Gets or sets RunAsAccount.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzure, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public ASRRunAsAccount Account { get; set; }

        /// <summary>
        /// Gets or sets Recovery Azure Network Id.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzure)]
        [ValidateNotNullOrEmpty]
        public string RecoveryAzureNetworkId { get; set; }

        /// <summary>
        /// Gets or sets Recovery Azure Subnet Id.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzure)]
        [ValidateNotNullOrEmpty]
        public string RecoveryAzureSubnetId { get; set; }

        /// <summary>
        /// Gets or sets Recovery Azure Log Storage Account Id.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzure)]
        [ValidateNotNullOrEmpty]
        public string CacheStorageAccount { get; set; }

        /// <summary>
        /// Gets or sets Replication Group Name.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzure)]
        [ValidateNotNullOrEmpty]
        public string ReplicationGroupName { get; set; }

        /// <summary>
        /// Gets or sets Disks to Include.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.VMwareToAzure)]
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

            var policy = RecoveryServicesClient.GetAzureSiteRecoveryPolicy(
                Utilities.GetValueFromArmId(this.ProtectionContainerMapping.PolicyId, ARMResourceTypeConstants.Policies)).Policy;
            var policyInstanceType = policy.Properties.ProviderSpecificDetails.InstanceType;

            switch (this.ParameterSetName)
            {
                case ASRParameterSets.EnterpriseToEnterprise:
                    if (policyInstanceType != Constants.HyperVReplica2012 &&
                        policyInstanceType != Constants.HyperVReplica2012R2)
                    {
                        throw new PSArgumentException(
                            string.Format(
                                Properties.Resources.ContainerMappingParameterSetMismatch,
                                this.ProtectionContainerMapping.Name,
                                policyInstanceType));
                    }
                    break;

                case ASRParameterSets.EnterpriseToAzure:
                case ASRParameterSets.HyperVSiteToAzure:
                    if (policyInstanceType != Constants.HyperVReplicaAzure)
                    {
                        throw new PSArgumentException(
                            string.Format(
                                Properties.Resources.ContainerMappingParameterSetMismatch,
                                this.ProtectionContainerMapping.Name,
                                policyInstanceType));
                    }
                    break;

                case ASRParameterSets.VMwareToAzure:
                    // Note: Policy Instance should be InMageAzureV2 and not InMage.
                    if (policyInstanceType != Constants.InMageAzureV2)
                    {
                        throw new PSArgumentException(
                            string.Format(
                                Properties.Resources.ContainerMappingParameterSetMismatch,
                                this.ProtectionContainerMapping.Name,
                                policyInstanceType));
                    }
                    break;

                default:
                    break;
            }

            EnableProtectionProviderSpecificInput enableProtectionProviderSpecificInput = new EnableProtectionProviderSpecificInput();
            EnableProtectionInputProperties inputProperties = new EnableProtectionInputProperties()
            {
                PolicyId = this.ProtectionContainerMapping.PolicyId,
                ProtectableItemId = this.ProtectableItem.ID,
                ProviderSpecificDetails = enableProtectionProviderSpecificInput
            };

            EnableProtectionInput input = new EnableProtectionInput()
            {
                Properties = inputProperties
            };

            // Validate the Replication Provider.
            // E2A and B2A.
            if (0 == string.Compare(this.ParameterSetName, ASRParameterSets.EnterpriseToAzure, StringComparison.OrdinalIgnoreCase) ||
                0 == string.Compare(this.ParameterSetName, ASRParameterSets.HyperVSiteToAzure, StringComparison.OrdinalIgnoreCase))
            {
                HyperVReplicaAzureEnableProtectionInput providerSettings = new HyperVReplicaAzureEnableProtectionInput();
                providerSettings.HvHostVmId = this.ProtectableItem.FabricObjectId;
                providerSettings.VmName = this.ProtectableItem.FriendlyName;

                // Id disk details are missing in input PE object, get the latest PE.
                if (string.IsNullOrEmpty(this.ProtectableItem.OS))
                {
                    // Just checked for OS to see whether the disk details got filled up or not
                    ProtectableItemResponse protectableItemResponse =
                        RecoveryServicesClient.GetAzureSiteRecoveryProtectableItem(
                        Utilities.GetValueFromArmId(this.ProtectableItem.ID, ARMResourceTypeConstants.ReplicationFabrics),
                        this.ProtectableItem.ProtectionContainerId,
                        this.ProtectableItem.Name);

                    this.ProtectableItem = new ASRProtectableItem(protectableItemResponse.ProtectableItem);
                }

                if (string.IsNullOrWhiteSpace(this.OS))
                {
                    providerSettings.OSType = ((string.Compare(this.ProtectableItem.OS, Constants.OSWindows, StringComparison.OrdinalIgnoreCase) == 0) ||
                        (string.Compare(this.ProtectableItem.OS, Constants.OSLinux) == 0)) ? this.ProtectableItem.OS : Constants.OSWindows;
                }
                else
                {
                    providerSettings.OSType = this.OS;
                }

                if (string.IsNullOrWhiteSpace(this.OSDiskName))
                {
                    providerSettings.VhdId = this.ProtectableItem.OSDiskId;
                }
                else
                {
                    foreach (var disk in this.ProtectableItem.Disks)
                    {
                        if (0 == string.Compare(disk.Name, this.OSDiskName, true))
                        {
                            providerSettings.VhdId = disk.Id;
                            break;
                        }
                    }
                }

                if (RecoveryAzureStorageAccountId != null)
                {
                    providerSettings.TargetStorageAccountId = RecoveryAzureStorageAccountId;
                }

                input.Properties.ProviderSpecificDetails = providerSettings;
            }
            else if (string.Compare
                        (this.ParameterSetName,
                        ASRParameterSets.VMwareToAzure,
                        StringComparison.OrdinalIgnoreCase) == 0)
            {
                // Create the InMageAzureV2 Provider specific input.
                // (todo: prmyaka) Add Resource group.
                InMageAzureV2EnableProtectionInput providerSettings =
                    new InMageAzureV2EnableProtectionInput()
                    {
                        ProcessServerId = this.ProcessServer.Id,
                        MasterTargetId = this.ProcessServer.Id, // Assumption: PS and MT are same.
                        RunAsAccountId = this.Account.AccountId,
                        StorageAccountId = this.RecoveryAzureStorageAccountId,
                        StorageSubscriptionId = (this.RecoveryAzureStorageAccountId != null) ? Utilities.GetValueFromArmId(
                            this.RecoveryAzureStorageAccountId, ARMResourceTypeConstants.Subscriptions) : null,
                        TargetAzureNetworkId = this.RecoveryAzureNetworkId,
                        TargetAzureSubnetId = this.RecoveryAzureSubnetId,
                        LogStorageAccountId = this.CacheStorageAccount,
                        MultiVmGroupName = this.ReplicationGroupName,
                        MultiVmGroupId = this.ReplicationGroupName,
                        TargetAzureVmName = this.ProtectableItem.FriendlyName,
                        EnableRDPOnTargetOption = Constants.NeverEnableRDPOnTargetOption,
                        DisksToInclude = (this.IncludeDiskIds != null) ?
                            new List<string>(this.IncludeDiskIds) : null
                    };

                // Check if the Replication Group Name is valid.
                if (this.ReplicationGroupName != null)
                {
                    // Get all the Protected Items in the Protection Container.
                    string fabricName = Utilities.GetValueFromArmId(
                        this.ProtectableItem.ID, ARMResourceTypeConstants.ReplicationFabrics);
                    string protectionContainerName = Utilities.GetValueFromArmId(
                        this.ProtectableItem.ID, ARMResourceTypeConstants.ReplicationProtectionContainers);
                    ReplicationProtectedItemListResponse rpiListResponse =
                        RecoveryServicesClient.GetAzureSiteRecoveryReplicationProtectedItem(
                            fabricName,
                            protectionContainerName);

                    // Loop over all the Protected Items and find if the Multi VM Group already exists.
                    bool flag = false;
                    foreach (var rpi in rpiListResponse.ReplicationProtectedItems)
                    {
                        // Check if the Replication Protected Item is an InMageAzureV2 Instance.
                        if (string.Compare(
                                rpi.Properties.ProviderSpecificDetails.InstanceType,
                                Constants.InMageAzureV2,
                                StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            // Get the InMageAzureV2 specific details.
                            InMageAzureV2ProviderSpecificSettings providerSpecificDetails =
                               (InMageAzureV2ProviderSpecificSettings)rpi.Properties.ProviderSpecificDetails;

                            // Compare the Multi VM Group Name.
                            if (string.Compare(
                                    this.ReplicationGroupName,
                                    providerSpecificDetails.MultiVmGroupName,
                                    StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                // Multi VM Group found.
                                // Set the values in the InMageAzureV2 Provider specific input.
                                providerSettings.MultiVmGroupName = providerSpecificDetails.MultiVmGroupName;
                                providerSettings.MultiVmGroupId = providerSpecificDetails.MultiVmGroupId;
                                flag = true;
                                break;
                            }
                        }
                    }

                    // Check if the Multi VM Group was found or is to be created now.
                    if (flag == false)
                    {
                        // Multi VM Group was not found.
                        // Create a new Multi VM Group and Set the values in the 
                        // InMageAzureV2 Provider specific input.
                        providerSettings.MultiVmGroupName = this.ReplicationGroupName;
                        providerSettings.MultiVmGroupId = Guid.NewGuid().ToString();
                    }
                }

                // Set the InMageAzureV2 Provider specific input in the Enable Protection Input.
                input.Properties.ProviderSpecificDetails = providerSettings;
            }

            this.response =
                RecoveryServicesClient.EnableProtection(
                Utilities.GetValueFromArmId(this.ProtectableItem.ID, ARMResourceTypeConstants.ReplicationFabrics),
                Utilities.GetValueFromArmId(this.ProtectableItem.ID, ARMResourceTypeConstants.ReplicationProtectionContainers),
                Name,
                input);

            jobResponse =
                RecoveryServicesClient
                .GetAzureSiteRecoveryJobDetails(PSRecoveryServicesClient.GetJobIdFromReponseLocation(response.Location));

            WriteObject(new ASRJob(jobResponse.Job));

            if (this.WaitForCompletion.IsPresent)
            {
                this.WaitForJobCompletion(this.jobResponse.Job.Name);

                jobResponse =
                RecoveryServicesClient
                .GetAzureSiteRecoveryJobDetails(PSRecoveryServicesClient.GetJobIdFromReponseLocation(response.Location));

                WriteObject(new ASRJob(jobResponse.Job));
            }
        }

        /// <summary>
        /// Writes Job.
        /// </summary>
        /// <param name="job">JOB object</param>
        private void WriteJob(Microsoft.Azure.Management.SiteRecovery.Models.Job job)
        {
            this.WriteObject(new ASRJob(job));
        }
    }
}