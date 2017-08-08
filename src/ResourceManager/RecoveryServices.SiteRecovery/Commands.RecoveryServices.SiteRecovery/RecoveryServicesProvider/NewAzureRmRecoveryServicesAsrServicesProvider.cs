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
using Microsoft.Azure.Management.RecoveryServices.SiteRecovery.Models;

namespace Microsoft.Azure.Commands.RecoveryServices.SiteRecovery
{
    /// <summary>
    ///     Creates Azure Site Recovery Services Provider.
    /// </summary>
    [Cmdlet(
        VerbsCommon.New,
        "AzureRmRecoveryServicesAsrServicesProvider",
        DefaultParameterSetName = ASRParameterSets.AddRspWithRsAad,
        SupportsShouldProcess = true)]
    [Alias("New-ASRServicesProvider")]
    [OutputType(typeof(ASRJob))]
    public class NewAzureRmRecoveryServicesAsrServicesProvider : SiteRecoveryCmdletBase
    {
        /// <summary>
        ///     Gets or sets the name of the fabric to be created.
        /// </summary>
        [Parameter(
            ParameterSetName = ASRParameterSets.AddRspWithRsAad,
            Mandatory = true,
            HelpMessage = "Name of the recovery services provider to be created.")]
        [Parameter(
            ParameterSetName = ASRParameterSets.AddRspWithCustomerAad,
            Mandatory = true,
            HelpMessage = "Name of the recovery services provider to be created.")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the Fabric under which the provider is to be added.
        /// </summary>
        [Parameter(
            ParameterSetName = ASRParameterSets.AddRspWithRsAad,
            Mandatory = true,
            HelpMessage = "The fabric under which the recovery services provider is to be created.")]
        [Parameter(
            ParameterSetName = ASRParameterSets.AddRspWithCustomerAad,
            Mandatory = true,
            HelpMessage = "The fabric under which the recovery services provider is to be created..")]
        [ValidateNotNullOrEmpty]
        public ASRFabric Fabric { get; set; }

        /// <summary>
        ///     Gets or sets the certificate value.
        /// </summary>
        [Parameter(
            ParameterSetName = ASRParameterSets.AddRspWithRsAad,
            Mandatory = true,
            HelpMessage =
                "The value of the \"asymmetric\" credential type. It represents the " +
                "base 64 encoded certificate.")]
        [ValidateNotNullOrEmpty]
        public string CertValue { get; set; }

        /// <summary>
        ///     Gets or sets the name of the Object Id of the service principal.
        /// </summary>
        [Parameter(
            ParameterSetName = ASRParameterSets.AddRspWithCustomerAad,
            Mandatory = true,
            HelpMessage = "Object Id of the service principal.")]
        [ValidateNotNullOrEmpty]
        public Guid ObjectId { get; set; }

        /// <summary>
        ///     Gets ors sets the unique Application Id for the service principal in a tenant.
        /// </summary>
        [Parameter(
            ParameterSetName = ASRParameterSets.AddRspWithCustomerAad,
            Mandatory = true,
            HelpMessage = "The unique Application Id for the service principal in a tenant.")]
        [ValidateNotNullOrEmpty]
        public Guid ApplicationId { get; set; }

        /// <summary>
        ///     Gets ors sets the tenant Id.
        /// </summary>
        [Parameter(
            ParameterSetName = ASRParameterSets.AddRspWithCustomerAad,
            Mandatory = true,
            HelpMessage = "The Tenant Id for the service principal.")]
        [ValidateNotNullOrEmpty]
        public Guid TenantId { get; set; }

        /// <summary>
        ///     Gets ors sets the audience.
        /// </summary>
        [Parameter(
            ParameterSetName = ASRParameterSets.AddRspWithCustomerAad,
            Mandatory = true,
            HelpMessage = "The intended audience for the service principal.")]
        [ValidateNotNullOrEmpty]
        public string Audience { get; set; }

        /// <summary>
        ///     Gets ors sets the AAD authority.
        /// </summary>
        [Parameter(
            ParameterSetName = ASRParameterSets.AddRspWithCustomerAad,
            Mandatory = true,
            HelpMessage = "The base AAD authority for the service principal.")]
        [ValidateNotNullOrEmpty]
        public string AadAuthority { get; set; }

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
                var input = new AddRecoveryServicesProviderInput();
                input.Properties = new AddRecoveryServicesProviderInputProperties();

                switch (this.ParameterSetName)
                {
                    case ASRParameterSets.AddRspWithRsAad:
                        input.Properties.IdentityProviderInput =
                            new RecoveryServicesAadIdentityProviderInput
                            {
                                RawCertData = this.CertValue
                            };
                        break;

                    case ASRParameterSets.AddRspWithCustomerAad:
                        input.Properties.IdentityProviderInput =
                            new CustomerAadIdentityProviderInput
                            {
                                AadAuthority = this.AadAuthority,
                                Audience = this.Audience,
                                ApplicationId = this.ApplicationId.ToString(),
                                ObjectId = this.ObjectId.ToString(),
                                TenantId = this.TenantId.ToString()
                            };
                        break;

                    default:
                        throw new NotImplementedException(
                            $"ParameterSetName {this.ParameterSetName} not handled.");
                }

                var response = this.RecoveryServicesClient.CreateAzureSiteRecoveryProvider(
                    this.Fabric.Name,
                    this.Name,
                    input);

                var jobResponse = this.RecoveryServicesClient.GetAzureSiteRecoveryJobDetails(
                    PSRecoveryServicesClient.GetJobIdFromReponseLocation(response.Location));

                this.WriteObject(new ASRJob(jobResponse));
            }
        }
    }
}