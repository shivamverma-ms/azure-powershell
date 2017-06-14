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
using Microsoft.Azure.Management.SiteRecovery.Models;

namespace Microsoft.Azure.Commands.SiteRecovery
{
    /// <summary>
    /// Used to initiate a test failover cleanup operation.
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "AzureRmSiteRecoveryTestFailoverCleanup", DefaultParameterSetName = ASRParameterSets.ByPEObject)]
    [OutputType(typeof(ASRJob))]
    public class StartAzureRmSiteRecoveryTestFailoverCleanup : SiteRecoveryCmdletBase
    {
        #region local parameters

        /// <summary>
        /// Gets or sets Name of the Protection Container.
        /// </summary>
        public string protectionContainerName;

        /// <summary>
        /// Gets or sets Name of the Fabric.
        /// </summary>
        public string fabricName;

        #endregion local parameters

        #region Parameters

        /// <summary>
        /// Gets or sets Recovery Plan object.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.ByRPObject, Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRRecoveryPlan RecoveryPlan { get; set; }

        /// <summary>
        /// Gets or sets Replication Protected Item.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.ByPEObject, Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRReplicationProtectedItem ReplicationProtectedItem { get; set; }

        /// <summary>
        /// Gets or sets test failover cleanup comments.
        /// </summary>
        [Parameter]
        public string Comments { get; set; }

        #endregion Parameters

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteSiteRecoveryCmdlet()
        {
            base.ExecuteSiteRecoveryCmdlet();

            if (this.ParameterSetName == ASRParameterSets.ByRPObject)
            {
                this.StartRpTestFailoverCleanup();
            }
            else
            {
                this.protectionContainerName =
                    Utilities.GetValueFromArmId(this.ReplicationProtectedItem.ID, ARMResourceTypeConstants.ReplicationProtectionContainers);
                this.fabricName = Utilities.GetValueFromArmId(this.ReplicationProtectedItem.ID, ARMResourceTypeConstants.ReplicationFabrics);
                this.StartPETestFailoverCleanup();
            }
        }

        /// <summary>
        /// Starts PE Test failover cleanup.
        /// </summary>
        private void StartPETestFailoverCleanup()
        {
            var testFailoverCleanupInputProperties = new TestFailoverCleanupInputProperties()
            {
                Comments = this.Comments
            };

            var input = new TestFailoverCleanupInput()
            {
                Properties = testFailoverCleanupInputProperties
            };

            LongRunningOperationResponse response =
                RecoveryServicesClient.StartAzureSiteRecoveryTestFailoverCleanup(
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
        /// Starts RP Test failover cleanup.
        /// </summary>
        private void StartRpTestFailoverCleanup()
        {
            // Refresh RP Object
            var rp = RecoveryServicesClient.GetAzureSiteRecoveryRecoveryPlan(this.RecoveryPlan.Name);

            var recoveryPlanTestFailoverCleanupInputProperties = new RecoveryPlanTestFailoverCleanupInputProperties()
            {
                Comments = this.Comments
            };

            var recoveryPlanTestFailoverCleanupInput = new RecoveryPlanTestFailoverCleanupInput()
            {
                Properties = recoveryPlanTestFailoverCleanupInputProperties
            };

            LongRunningOperationResponse response = RecoveryServicesClient.StartAzureSiteRecoveryTestFailoverCleanup(
                this.RecoveryPlan.Name,
                recoveryPlanTestFailoverCleanupInput);

            JobResponse jobResponse =
                RecoveryServicesClient
                .GetAzureSiteRecoveryJobDetails(PSRecoveryServicesClient.GetJobIdFromReponseLocation(response.Location));

            WriteObject(new ASRJob(jobResponse.Job));
        }
    }
}