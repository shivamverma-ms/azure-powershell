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
using System.Management.Automation;
using Microsoft.Azure.Management.SiteRecovery.Models;
using System.Collections.Generic;

namespace Microsoft.Azure.Commands.SiteRecovery
{
    /// <summary>
    /// Reprotects Replication protected item.
    /// </summary>
    [Cmdlet(VerbsCommon.Switch, "AzureRmSiteRecoveryReplicationProtectedItem", SupportsShouldProcess = true)]
    [OutputType(typeof(ASRJob))]
    public class SwitchAzureRmSiteRecoveryReplicationProtectedItem : SiteRecoveryCmdletBase
    {
        /// <summary>
        /// Gets or sets Name of the Protection Container.
        /// </summary>
        public string protectionContainerName;

        /// <summary>
        /// Gets or sets Name of the Fabric.
        /// </summary>
        public string fabricName;

        /// <summary>
        /// Gets or sets the friendly name of the fabric.
        /// </summary>
        private string fabricFriendlyName;

        /// <summary>
        /// Long running operation response.
        /// </summary>
        private LongRunningOperationResponse response = null;

        /// <summary>
        /// Job response.
        /// </summary>
        JobResponse jobResponse = null;

        #region Parameters

        /// <summary>
        /// Gets or sets Replication Protected Item.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzure, Mandatory = true, ValueFromPipeline = true)]
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzureWithMultipleStorageAccount, Mandatory = true, ValueFromPipeline = true)]
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzureManagedDisk, Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRReplicationProtectedItem ReplicationProtectedItem { get; set; }

        /// <summary>
        /// Gets or sets Replication Protected Item Name.
        /// </summary>
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Protection Container Mapping.
        /// </summary>
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public ASRProtectionContainerMapping ProtectionContainerMapping { get; set; }

        /// <summary>
        /// Gets or sets Recovery Azure Storage Account Name of the Policy for A2A scenarios.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzure)]
        [ValidateNotNullOrEmpty]
        public string RecoveryAzureStorageAccountId { get; set; }

        /// <summary>
        /// Gets or sets list of disks to be replicated.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzureWithMultipleStorageAccount,
            Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public List<ASRAzureToAzureDiskDetails> AzureVmDiskDetails { get; set; }

        /// <summary>
        /// Gets or sets list of managed disks to be replicated.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzureManagedDisk, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public List<ASRAzureToAzureManagedDiskDetails> AzureVmManagedDiskDetails { get; set; }

        /// <summary>
        /// Gets or sets Staging storage account.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzure, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string PrimaryStagingAzureStorageAccountId { get; set; }

        /// <summary>
        /// Gets or sets Recovery Resource Group Id.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzure)]
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzureWithMultipleStorageAccount)]
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzureManagedDisk)]
        [ValidateNotNullOrEmpty]
        public string RecoveryResourceGroupId { get; set; }

        /// <summary>
        /// Gets or sets Recovery Cloud Service Id.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzure)]
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzureWithMultipleStorageAccount)]
        [ValidateNotNullOrEmpty]
        public string RecoveryCloudServiceId { get; set; }

        /// <summary>
        /// Gets or sets Recovery Availability Set Id.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzure)]
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzureWithMultipleStorageAccount)]
        [Parameter(ParameterSetName = ASRParameterSets.AzureToAzureManagedDisk)]
        [ValidateNotNullOrEmpty]
        public string RecoveryAvailabilitySetId { get; set; }

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
                Utilities.GetValueFromArmId(
                    this.ProtectionContainerMapping.PolicyId,
                    ARMResourceTypeConstants.Policies)).Policy;
            var policyInstanceType = policy.Properties.ProviderSpecificDetails.InstanceType;

            switch (this.ParameterSetName)
            {
                case ASRParameterSets.AzureToAzure:
                case ASRParameterSets.AzureToAzureManagedDisk:
                    if (policyInstanceType != Constants.AzureToAzure)
                    {
                        throw new PSArgumentException(
                            string.Format(
                                Properties.Resources.ContainerMappingParameterSetMismatch,
                                this.ProtectionContainerMapping.Name,
                                policyInstanceType));
                    }
                    this.protectionContainerName =
                        Utilities.GetValueFromArmId(
                            this.ReplicationProtectedItem.ID,
                            ARMResourceTypeConstants.ReplicationProtectionContainers);
                    this.fabricName = Utilities.GetValueFromArmId(
                        this.ReplicationProtectedItem.ID,
                        ARMResourceTypeConstants.ReplicationFabrics);
                    this.fabricFriendlyName =
                        this.ReplicationProtectedItem.PrimaryFabricFriendlyName;
                    this.SwitchProtection();
                    break;

                default:
                    break;
            }         
        }

        /// <summary>
        /// Switches protection from one container to another or one replication provider to
        /// another.
        /// </summary>
        private void SwitchProtection()
        {
            var switchProtectionInputProperties = new SwitchProtectionInputProperties()
            {
                ReplicationProtectedItemName = this.ReplicationProtectedItem.Name,
                ProviderSpecificDetails = new SwitchProtectionProviderSpecificInput()
            };

            SwitchProtectionInput input = new SwitchProtectionInput()
            {
                Properties = switchProtectionInputProperties
            };

            if (0 == string.Compare(
                this.ReplicationProtectedItem.ReplicationProvider,
                Constants.AzureToAzure,
                StringComparison.OrdinalIgnoreCase))
            {
                var a2aSwitchInput = new A2ASwitchProtectionInput()
                {
                    PolicyId = this.ProtectionContainerMapping.PolicyId,
                    RecoveryContainerId =
                        this.ProtectionContainerMapping.TargetProtectionContainerId,
                    VmDisks = new List<A2AVmDiskInputDetails>(),
                    VmManagedDisks = new List<A2AVmManagedDiskInputDetails>(),
                    RecoveryResourceGroupId = this.RecoveryResourceGroupId,
                    RecoveryCloudServiceId = this.RecoveryCloudServiceId,
                    RecoveryAvailabilitySetId = this.RecoveryAvailabilitySetId
                };

                // Fetch the latest Protectable item objects
                ReplicationProtectedItemResponse replicationProtectedItemResponse =
                    RecoveryServicesClient.GetAzureSiteRecoveryReplicationProtectedItem(
                        this.fabricName,
                        this.protectionContainerName,
                        this.ReplicationProtectedItem.Name);

                if (this.AzureVmManagedDiskDetails != null)
                {
                    foreach (ASRAzureToAzureManagedDiskDetails disk in this.AzureVmManagedDiskDetails)
                    {
                        if (string.IsNullOrEmpty(disk.PrimaryStagingAzureStorageAccountId))
                        {
                            throw new PSArgumentException(
                                string.Format(
                                    Properties.Resources.InvalidPrimaryStagingAzureStorageAccountIdDiskInput,
                                    disk.DiskId));
                        }

                        if (string.IsNullOrEmpty(disk.RecoveryAzureResourceGroupId))
                        {
                            throw new PSArgumentException(
                                string.Format(
                                    Properties.Resources.InvalidRecoveryAzureStorageAccountIdDiskInput,
                                    disk.DiskId));
                        }

                        a2aSwitchInput.VmManagedDisks.Add(new A2AVmManagedDiskInputDetails
                        {
                            DiskId = disk.DiskId,
                            RecoveryResourceGroupId =
                                    disk.RecoveryAzureResourceGroupId,
                            PrimaryStagingAzureStorageAccountId =
                                    disk.PrimaryStagingAzureStorageAccountId,
                        });
                    }
                }
                else
                {

                    if (this.AzureVmDiskDetails == null || this.AzureVmDiskDetails.Count == 0)
                    {
                        if (string.IsNullOrEmpty(this.PrimaryStagingAzureStorageAccountId))
                        {
                            throw new ArgumentException(
                                Properties.Resources.InvalidPrimaryStagingAzureStorageAccountId);
                        }

                        if (this.fabricFriendlyName !=
                            this.ProtectionContainerMapping.TargetFabricFriendlyName &&
                            RecoveryAzureStorageAccountId == null)
                        {
                            throw new ArgumentException(
                                Properties.Resources.InvalidRecoveryAzureStorageAccountId);
                        }

                        foreach (var disk in ((A2AReplicationDetails)replicationProtectedItemResponse
                            .ReplicationProtectedItem.Properties.ProviderSpecificDetails)
                            .ProtectedDisks)
                        {
                            a2aSwitchInput.VmDisks.Add(new A2AVmDiskInputDetails
                            {
                                DiskUri = disk.RecoveryDiskUri,
                                RecoveryAzureStorageAccountId =
                                    this.fabricFriendlyName ==
                                        this.ProtectionContainerMapping.TargetFabricFriendlyName &&
                                    this.RecoveryAzureStorageAccountId == null ?
                                        disk.PrimaryDiskAzureStorageAccountId :
                                        this.RecoveryAzureStorageAccountId,
                                PrimaryStagingAzureStorageAccountId =
                                    this.PrimaryStagingAzureStorageAccountId,
                            });
                        }
                    }
                    else
                    {
                        foreach (ASRAzureToAzureDiskDetails disk in this.AzureVmDiskDetails)
                        {
                            if (string.IsNullOrEmpty(disk.PrimaryStagingAzureStorageAccountId))
                            {
                                throw new PSArgumentException(
                                    string.Format(
                                        Properties.Resources.InvalidPrimaryStagingAzureStorageAccountIdDiskInput,
                                        disk.DiskUri));
                            }

                            if (string.IsNullOrEmpty(disk.RecoveryAzureStorageAccountId))
                            {
                                throw new PSArgumentException(
                                    string.Format(
                                        Properties.Resources.InvalidRecoveryAzureStorageAccountIdDiskInput,
                                        disk.DiskUri));
                            }

                            a2aSwitchInput.VmDisks.Add(new A2AVmDiskInputDetails
                            {
                                DiskUri = disk.DiskUri,
                                RecoveryAzureStorageAccountId = disk.RecoveryAzureStorageAccountId,
                                PrimaryStagingAzureStorageAccountId =
                                    disk.PrimaryStagingAzureStorageAccountId
                            });
                        }
                    }
                }

                input.Properties.ProviderSpecificDetails = a2aSwitchInput;
            }

            LongRunningOperationResponse response =
                RecoveryServicesClient.StartSwitchProtection(
                this.fabricName,
                this.protectionContainerName,
                input);

            JobResponse jobResponse =
                RecoveryServicesClient
                .GetAzureSiteRecoveryJobDetails(
                    PSRecoveryServicesClient.GetJobIdFromReponseLocation(response.Location));

            WriteObject(new ASRJob(jobResponse.Job));
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