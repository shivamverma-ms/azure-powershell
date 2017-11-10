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
using Microsoft.Azure.Commands.RecoveryServices.SiteRecovery.Properties;
using Microsoft.Azure.Management.RecoveryServices.SiteRecovery.Models;

namespace Microsoft.Azure.Commands.RecoveryServices.SiteRecovery
{
    /// <summary>
    ///     Creates Azure Site Recovery Fabric object.
    /// </summary>
    [Cmdlet(
        VerbsCommon.New,
        "AzureRmRecoveryServicesAsrFabric",
        DefaultParameterSetName = ASRParameterSets.Default,
        SupportsShouldProcess = true)]
    [Alias("New-ASRFabric")]
    [OutputType(typeof(ASRJob))]
    public class NewAzureRmRecoveryServicesAsrFabric : SiteRecoveryCmdletBase
    {
        /// <summary>
        ///     Gets or sets the name of the fabric to be created.
        /// </summary>
        [Parameter(
            ParameterSetName = ASRParameterSets.Default,
            Mandatory = true,
            HelpMessage = "Name of the fabric to be created")]
        [Parameter(
            ParameterSetName = ASRParameterSets.HyperVFabric,
            Mandatory = true,
            HelpMessage = "Name of the fabric to be created")]
        [Parameter(
            ParameterSetName = ASRParameterSets.VMwareV2Fabric,
            Mandatory = true,
            HelpMessage = "Name of the fabric to be created")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the fabric type.
        ///     This parameter will be deprecated soon. Type will be decided based on the
        ///     switch parameters from now on. Introduce a new switch in case new fabric type is
        ///     needed.
        /// </summary>
        [Parameter(
            ParameterSetName = ASRParameterSets.Default,
            Mandatory = false)]
        [ValidateSet(FabricProviders.HyperVSite)]
        [Obsolete("To create a Hyper-V fabric, use the command with HyperV switch parameter.",
            false)]
        public string Type { get; set; }

        /// <summary>
        ///     Gets or sets the HyperV switch parameter.
        /// </summary>
        [Parameter(
            ParameterSetName = ASRParameterSets.HyperVFabric,
            Mandatory = true)]
        public SwitchParameter HyperV { get; set; }

        /// <summary>
        ///     Gets or sets the VMwareV2 switch parameter.
        /// </summary>
        [Parameter(
            ParameterSetName = ASRParameterSets.VMwareV2Fabric,
            Mandatory = true)]
        public SwitchParameter VMwareV2 { get; set; }

        /// <summary>
        ///     Gets or sets the Key Vault URL.
        /// </summary>
        [Parameter(
            ParameterSetName = ASRParameterSets.VMwareV2Fabric,
            Mandatory = true,
            HelpMessage = "The URL of the key vault to be associated with the fabric")]
        [ValidateNotNullOrEmpty]
        public string KeyVaultUrl { get; set; }

        /// <summary>
        ///     Gets or sets the Key Vault ARM Id.
        /// </summary>
        [Parameter(
            ParameterSetName = ASRParameterSets.VMwareV2Fabric,
            Mandatory = true,
            HelpMessage ="The ARM Id of the key vault to be associated with the fabric")]
        [ValidateNotNullOrEmpty]
        public string KeyVaultResourceId { get; set; }

        /// <summary>
        ///     ProcessRecord of the command.
        /// </summary>
        public override void ExecuteSiteRecoveryCmdlet()
        {
            base.ExecuteSiteRecoveryCmdlet();

            if (this.ShouldProcess(
                this.Name,
                VerbsCommon.New))
            {
                var input = new FabricCreationInput()
                {
                    Properties = new FabricCreationInputProperties()
                };

                if (this.VMwareV2.IsPresent)
                {
                    input.Properties.CustomDetails = new VMwareV2FabricCreationInput
                    {
                        KeyVaultUrl = this.KeyVaultUrl,
                        KeyVaultResourceArmId = this.KeyVaultResourceId
                    };
                }
                else
                {
                    input.Properties.CustomDetails = new FabricSpecificCreationInput();
                }

                var response = this.RecoveryServicesClient.CreateAzureSiteRecoveryFabric(
                    this.Name,
                    input);

                var jobResponse = this.RecoveryServicesClient.GetAzureSiteRecoveryJobDetails(
                    PSRecoveryServicesClient.GetJobIdFromReponseLocation(response.Location));

                this.WriteObject(new ASRJob(jobResponse));
            }
        }
    }
}