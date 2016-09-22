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
using System.Collections.Generic;
using System.ComponentModel;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.SiteRecovery
{
    /// <summary>
    /// Retrieves Azure Site Recovery vCenter.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "AzureRmSiteRecoveryVCenter",
        DefaultParameterSetName = ASRParameterSets.Default)]
    [OutputType(typeof(IEnumerable<ASRJob>))]
    public class NewAzureRmSiteRecoveryVCenter : SiteRecoveryCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets Fabric of the vCenter.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRFabric Fabric { get; set; }

        /// <summary>
        /// Gets or sets name of the vCenter.
        /// </summary>
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets ip address or hostname of the vCenter.
        /// </summary>
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets port number of the vCenter.
        /// </summary>
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [DefaultValue(443)]
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the process server id of the vCenter.
        /// </summary>
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ProcessServerId { get; set; }

        /// <summary>
        /// Gets or sets the account id of the vCenter.
        /// </summary>
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string AccountId { get; set; }

        #endregion Parameters

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteSiteRecoveryCmdlet()
        {
            base.ExecuteSiteRecoveryCmdlet();
            Utilities.ValidateIpOrHostName(this.Server);
            this.DiscoverVCenter();
        }

        /// <summary>
        /// Discover the vCenter.
        /// </summary>
        private void DiscoverVCenter()
        {
            FabricResponse fabricResponse =
                RecoveryServicesClient.GetAzureSiteRecoveryFabric(Fabric.Name);
            Fabric extendedFabric = fabricResponse.Fabric;

            VMwareFabricDetails vmwareFabricDetails =
                (VMwareFabricDetails)extendedFabric.Properties.CustomDetails;

            CreateVCenterInput createvCenterInput = new CreateVCenterInput();

            CreateVCenterProperties createVCenterProperties =
                new CreateVCenterProperties();
            createVCenterProperties.FriendlyName = this.Name;
            createVCenterProperties.IpAddress = this.Server;
            createVCenterProperties.Port = this.Port.ToString();
            createVCenterProperties.ProcessServerId = this.ProcessServerId;
                ////RecoveryServicesClient.GetInbuiltProcessServer(vmwareFabricDetails);
            createVCenterProperties.RunAsAccountId = this.AccountId;

            createvCenterInput.Properties = createVCenterProperties;

            LongRunningOperationResponse response =
                RecoveryServicesClient.NewAzureRmSiteRecoveryVCenter(
                     this.Fabric.Name,
                     this.Name,
                     createvCenterInput);

            JobResponse jobResponse =
                RecoveryServicesClient.GetAzureSiteRecoveryJobDetails(
                    PSRecoveryServicesClient.GetJobIdFromReponseLocation(response.Location));

            WriteObject(new ASRJob(jobResponse.Job));
        }
    }
}