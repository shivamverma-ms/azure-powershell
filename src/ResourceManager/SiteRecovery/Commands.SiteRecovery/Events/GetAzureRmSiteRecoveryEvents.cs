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
using System.Linq;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.SiteRecovery
{
    /// <summary>
    /// Retrieves Azure Site Recovery alert and nofification settings.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "AzureRmSiteRecoveryEvents",
        DefaultParameterSetName = ASRParameterSets.Default)]
    [OutputType(typeof(IEnumerable<ASREvent>))]
    public class GetAzureRmSiteRecoveryEvents : SiteRecoveryCmdletBase
    {

        #region Parameters

        /// <summary>
        /// Gets or sets the Severity.
        /// </summary>
        [Parameter]
        [ValidateSet(Constants.Critical, Constants.Warning, Constants.Information)]
        public string Severity { get; set; }

        /// <summary>
        /// Gets or sets the fabric.
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRFabric Fabric { get; set; }

        /// <summary>
        /// Gets or sets server name.
        /// </summary>
        [Parameter]
        [ValidateNotNullOrEmpty]
        public string AffectedObjectName { get; set; }

        /// <summary>
        /// Gets or sets switch parameter. On passing, command waits till completion.
        /// </summary>
        [Parameter]
        [ValidateSet(Constants.VmHealth,
            Constants.ServerHealth,
            Constants.JobStatus,
            Constants.AgentHealth)]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the Start time.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.ByTime, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the Endtime.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.ByTime)]
        [ValidateNotNullOrEmpty]
        public DateTime EndTime { get; set; }

        #endregion Parameters

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteSiteRecoveryCmdlet()
        {
            base.ExecuteSiteRecoveryCmdlet();

            this.GetEvents();
        }

        /// <summary>
        /// Gets the alert and notification settings.
        /// </summary>
        private void GetEvents()
        {
            EventQueryParameter parameters = new EventQueryParameter();

            if (!string.IsNullOrEmpty(this.Type))
            {
                parameters.EventType = this.Type;
            }

            if (!string.IsNullOrEmpty(this.Severity))
            {
                parameters.Severity = this.Severity;
            }

            if (this.Fabric != null)
            {
                parameters.FabricName = this.Fabric.Name;
            }

            if (!string.IsNullOrEmpty(this.AffectedObjectName))
            {
                parameters.AffectedObjectFriendlyName = this.AffectedObjectName;
            }

            if (this.StartTime != DateTime.MinValue)
            {
                parameters.StartTime = this.StartTime.ToString();
            }

            if (this.EndTime != DateTime.MinValue)
            {
                parameters.EndTime = this.EndTime.ToString();
            }

            EventListResponse eventsListResponse =
                RecoveryServicesClient.GetAzureRmSiteRecoveryEvents(parameters);

            this.WriteEvents(eventsListResponse.Events);
        }

        /// <summary>
        /// Write events.
        /// </summary>
        /// <param name="asrEvent">List of events.</param>
        private void WriteEvents(IList<Event> asrEvent)
        {
            this.WriteObject(asrEvent.Select(p => new ASREvent(p)), true);
        }
    }
}