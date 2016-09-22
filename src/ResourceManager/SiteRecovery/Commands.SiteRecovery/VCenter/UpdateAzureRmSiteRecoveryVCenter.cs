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
    /// Updates the Azure Site Recovery VCenter.
    /// </summary>
    [Cmdlet(VerbsData.Update, "AzureRmSiteRecoveryVCenter", DefaultParameterSetName = ASRParameterSets.Default)]
    [OutputType(typeof(IEnumerable<ASRJob>))]
    public class UpdateAzureRmSiteRecoveryVCenter : SiteRecoveryCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets the VCenter object.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.Default, Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRVCenter VCenter { get; set; }

        /// <summary>
        /// Gets or sets the port number.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.Default, Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public Nullable<int> Port { get; set; }

        /// <summary>
        /// Gets or sets the Process server object.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.Default, Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string ProcessServerId { get; set; }

        /// <summary>
        /// Gets or sets Run as Account object.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.Default, Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string AccountId { get; set; }

        #endregion Parameters

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteSiteRecoveryCmdlet()
        {
            base.ExecuteSiteRecoveryCmdlet();

            this.UpdateVCenter();
        }

        /// <summary>
        /// Update the vCenter.
        /// </summary>
        private void UpdateVCenter()
        {
            UpdateVCenterInput updatevCenterInput = new UpdateVCenterInput();

            UpdateVCenterProperties updateVCenterProperties =
                new UpdateVCenterProperties();

            if (this.Port.HasValue)
            {
                updateVCenterProperties.Port = this.Port.ToString();
            }

            if (!string.IsNullOrEmpty(this.ProcessServerId))
            {
                updateVCenterProperties.ProcessServerId = this.ProcessServerId;
            }

            if (!string.IsNullOrEmpty(this.AccountId))
            {
                updateVCenterProperties.RunAsAccountId = this.AccountId;
            }

            updatevCenterInput.Properties = updateVCenterProperties;

            LongRunningOperationResponse response =
                RecoveryServicesClient.UpdateAzureRmSiteRecoveryVCenter(
                     this.VCenter.FabricArmResourceName,
                     this.VCenter.Name,
                     updatevCenterInput);

            JobResponse jobResponse =
                RecoveryServicesClient.GetAzureSiteRecoveryJobDetails(
                    PSRecoveryServicesClient.GetJobIdFromReponseLocation(response.Location));

            WriteObject(new ASRJob(jobResponse.Job));
        }
    }
}