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
    /// Adds Azure Site Recovery Policy settings to a Protection Container.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "AzureRmSiteRecoveryProtectionContainerMapping", DefaultParameterSetName = ASRParameterSets.EnterpriseToAzureAndVMwareToAzure)]
    [OutputType(typeof(ASRJob))]
    public class NewAzureRmSiteRecoveryProtectionContainerMapping : SiteRecoveryCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets Policy object.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToEnterpriseAndVMwareToVMware, Mandatory = true)]
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToAzureAndVMwareToAzure, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Policy object.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToEnterpriseAndVMwareToVMware, Mandatory = true, ValueFromPipeline = true)]
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToAzureAndVMwareToAzure, Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRPolicy Policy { get; set; }

        /// <summary>
        /// Gets or sets Protection Container to be applied the Policy settings on.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToEnterpriseAndVMwareToVMware, Mandatory = true)]
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToAzureAndVMwareToAzure, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public ASRProtectionContainer PrimaryProtectionContainer { get; set; }

        /// <summary>
        /// Gets or sets Recovery Protection Container to be applied the Policy settings on.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.EnterpriseToEnterpriseAndVMwareToVMware, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public ASRProtectionContainer RecoveryProtectionContainer { get; set; }

        #endregion Parameters

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteSiteRecoveryCmdlet()
        {
            base.ExecuteSiteRecoveryCmdlet();

            switch (this.ParameterSetName)
            {
                case ASRParameterSets.EnterpriseToAzureAndVMwareToAzure:
                    this.EnterpriseToAzureAndVMwareToAzureAssociation();
                    break;
                case ASRParameterSets.EnterpriseToEnterpriseAndVMwareToVMware:
                    this.EnterpriseToEnterpriseAndVMwareToVMwareAssociation();
                    break;
            }
        }

        /// <summary>
        /// Associates Policy with enterprise based protection containers
        /// </summary>
        private void EnterpriseToEnterpriseAndVMwareToVMwareAssociation()
        {
            if ((string.Compare(
                    this.Policy.ReplicationProvider,
                    Constants.HyperVReplica2012,
                    StringComparison.OrdinalIgnoreCase) != 0) &&
                (string.Compare(
                    this.Policy.ReplicationProvider,
                    Constants.HyperVReplica2012R2,
                    StringComparison.OrdinalIgnoreCase) != 0) &&
                (string.Compare(
                    this.Policy.ReplicationProvider,
                    Constants.InMage,
                    StringComparison.OrdinalIgnoreCase) != 0))
            {
                throw new InvalidOperationException(
                    string.Format(
                    Properties.Resources.IncorrectReplicationProvider,
                    this.Policy.ReplicationProvider));
            }

            Associate(this.RecoveryProtectionContainer.ID);
        }


        /// <summary>
        /// Associates Azure Policy with enterprise based protection containers
        /// </summary>
        private void EnterpriseToAzureAndVMwareToAzureAssociation()
        {
            if ((string.Compare(
                    this.Policy.ReplicationProvider,
                    Constants.HyperVReplicaAzure,
                    StringComparison.OrdinalIgnoreCase) != 0) &&
                (string.Compare(
                    this.Policy.ReplicationProvider,
                    Constants.InMageAzureV2,
                    StringComparison.OrdinalIgnoreCase) != 0))

            {
                throw new InvalidOperationException(
                    string.Format(
                    Properties.Resources.IncorrectReplicationProvider,
                    this.Policy.ReplicationProvider));
            }

            Associate(Constants.AzureContainer);
        }

        /// <summary>
        /// Helper to configure cloud
        /// </summary>
        private void Associate(string targetProtectionContainerId)
        {
            CreateProtectionContainerMappingInputProperties inputProperties = new CreateProtectionContainerMappingInputProperties()
            {
                PolicyId = this.Policy.ID,
                ProviderSpecificInput = new ReplicationProviderContainerMappingInput(),
                TargetProtectionContainerId = targetProtectionContainerId
            };

            CreateProtectionContainerMappingInput input = new CreateProtectionContainerMappingInput()
            {
                Properties = inputProperties
            };

            LongRunningOperationResponse response = RecoveryServicesClient.ConfigureProtection(
                Utilities.GetValueFromArmId(this.PrimaryProtectionContainer.ID, ARMResourceTypeConstants.ReplicationFabrics),
                this.PrimaryProtectionContainer.Name,
                this.Name,
                input);

            JobResponse jobResponse =
                RecoveryServicesClient.
                GetAzureSiteRecoveryJobDetails(PSRecoveryServicesClient.GetJobIdFromReponseLocation(response.Location));

            this.WriteObject(new ASRJob(jobResponse.Job));
        }

    }
}