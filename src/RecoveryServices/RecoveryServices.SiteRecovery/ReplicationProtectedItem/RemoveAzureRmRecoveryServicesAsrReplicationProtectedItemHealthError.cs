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

using System.Management.Automation;
using System.Collections.Generic;
using Microsoft.Azure.Management.RecoveryServices.SiteRecovery.Models;
using Job = Microsoft.Azure.Management.RecoveryServices.SiteRecovery.Models.Job;

namespace Microsoft.Azure.Commands.RecoveryServices.SiteRecovery
{
    /// <summary>
    /// Removes health error for the replication protected item.
    /// </summary>
    [Cmdlet("Remove", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "RecoveryServicesAsrReplicationProtectedItemHealthError", DefaultParameterSetName = ASRParameterSets.AzureToAzure, SupportsShouldProcess = true)]
    [Alias("Remove-ASRReplicationProtectedItemHealthError")]
    [OutputType(typeof(ASRJob))]
    public class RemoveAzureRmRecoveryServicesAsrReplicationProtectedItemHealthError : SiteRecoveryCmdletBase
    {
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true)]
        public ASRReplicationProtectedItem ReplicationProtectedItem { get; set; }

        /// <summary>
        /// Gets or sets the errro id.
        /// </summary>
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string[] ErrorId { get; set; }

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteSiteRecoveryCmdlet()
        {
            base.ExecuteSiteRecoveryCmdlet();

            var input = new ResolveHealthInput { Properties = new ResolveHealthInputProperties() };
            FillResolveHealthErrorInput(input);

            if (this.ShouldProcess(
                this.ReplicationProtectedItem.FriendlyName,
                "Removes the protected item's health error"))
            {
                PSSiteRecoveryLongRunningOperation response =
                    this.RecoveryServicesClient.ResolveHealthError(
                        Utilities.GetValueFromArmId(
                            this.ReplicationProtectedItem.ID,
                            ARMResourceTypeConstants.ReplicationFabrics),
                        Utilities.GetValueFromArmId(
                            this.ReplicationProtectedItem.ID,
                            ARMResourceTypeConstants.ReplicationProtectionContainers),
                        this.ReplicationProtectedItem.Name,
                        input);

                this.jobResponse = this.RecoveryServicesClient.GetAzureSiteRecoveryJobDetails(
                    PSRecoveryServicesClient.GetJobIdFromReponseLocation(response.Location));

                this.WriteObject(new ASRJob(this.jobResponse));
            }
        }

        /// <summary>
        /// Helper method to fill in input details.
        /// </summary>
        private void FillResolveHealthErrorInput(ResolveHealthInput input)
        {
            input.Properties.HealthErrors = new List<ResolveHealthError>();

            foreach (string errorId in ErrorId)
            {
                input.Properties.HealthErrors.Add(new ResolveHealthError(errorId));
            }
        }

        /// <summary>
        /// Writes Job.
        /// </summary>
        /// <param name="job">Job object.</param>
        private void WriteJob(
            Job job)
        {
            this.WriteObject(new ASRJob(job));
        }

        private Job jobResponse;
    }
}
