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
using System.Collections.Generic;
using Microsoft.Azure.Management.SiteRecovery.Models;

namespace Microsoft.Azure.Commands.SiteRecovery
{
    public class ASREvent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ASREvent"/> class.
        /// </summary>
        /// <param name="asrEvent">The asr monitoring event.</param>
        public ASREvent(Event asrEvent)
        {
            this.AffectedObjectFriendlyName =
                asrEvent.Properties.AffectedObjectFriendlyName;
            this.Description = asrEvent.Properties.Description;
            this.EventCode = asrEvent.Properties.EventCode;
            this.EventSpecificDetails = asrEvent.Properties.EventSpecificDetails;
            this.EventType = asrEvent.Properties.EventType;
            this.FabricId = asrEvent.Properties.FabricId;
            this.HealthErrors = asrEvent.Properties.HealthErrors;
            this.Severity = asrEvent.Properties.Severity;
            this.TimeOfOccurrence = asrEvent.Properties.TimeOfOccurrence.ToLocalTime();

            if (asrEvent.Properties.ProviderDetails is A2AEventDetails)
            {
                var a2aEventDetails = (A2AEventDetails)asrEvent.Properties.ProviderDetails;
                this.ProviderSpecificDetails =
                    new AzureToAzureProviderSpecificEventDetails(a2aEventDetails);
            }
        }

        #region Properties

        /// <summary>
        /// Gets or sets the time of occurence of the event.
        /// </summary
        public DateTime TimeOfOccurrence { get; set; }

        /// <summary>
        /// Gets or sets the severity of the event.
        /// </summary
        public string Severity { get; set; }

        /// <summary>
        /// Gets or sets the list of errors / warnings capturing details associated with the
        /// issue(s).
        /// </summary
        public IList<HealthError> HealthErrors { get; set; }

        /// <summary>
        /// Gets or sets the ARM ID of the fabric.
        /// </summary
        public string FabricId { get; set; }

        /// <summary>
        /// Gets or sets the type of the event.
        /// for example: VM Health, Server Health, Job Failure etc.
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// Gets or sets the event specific settings.
        /// </summary
        public EventSpecificDetails EventSpecificDetails { get; set; }

        /// <summary>
        /// Gets or sets the Id of the monitoring event.
        /// </summary>
        public string EventCode { get; set; }
        
        /// <summary>
        ///  Gets or sets the event name.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the friendly name of the source of the event on which it 
        /// is raised (for example, VM, VMM etc).
        /// </summary>
        public string AffectedObjectFriendlyName { get; set; }

        /// <summary>
        /// Gets or sets Provider Specific Details
        /// </summary>
        public ASREventProviderSpecificDetails ProviderSpecificDetails { get; set; }

        #endregion
    }

    /// <summary>
    /// Replication provider specific entity details.
    /// </summary>
    public class ASREventProviderSpecificDetails
    {
        /// <summary>
        /// Initializes a new instance of the<see cref="ASREventProviderSpecificDetails" /> class.
        /// </summary>
        public ASREventProviderSpecificDetails()
        {
        }

        /// <summary>
        /// Initializes a new instance of the<see cref="ASREventProviderSpecificDetails" /> class.
        /// </summary>
        public ASREventProviderSpecificDetails(EventProviderSpecificDetails providerSpecificDetails)
        {
            this.InstanceType = providerSpecificDetails.InstanceType;
        }

        /// <summary>
        /// Gets or sets the Instance type name.
        /// </summary>
        public string InstanceType { get; set; }
    }

    /// <summary>
    /// Defines A2A provider specific event details.
    /// </summary>
    public class AzureToAzureProviderSpecificEventDetails : ASREventProviderSpecificDetails
    {
        public AzureToAzureProviderSpecificEventDetails(A2AEventDetails a2AEventDetails)
            : base(a2AEventDetails)
        {
            this.ProtectedItemName = a2AEventDetails.ProtectedItemName;
            this.FabricObjectId = a2AEventDetails.FabricObjectId;
            this.FabricName = a2AEventDetails.FabricName;
            this.FabricLocation = a2AEventDetails.FabricLocation;
            this.RemoteFabricName = a2AEventDetails.RemoteFabricName;
            this.RemoteFabricLocation = a2AEventDetails.RemoteFabricLocation;
        }

        /// <summary>
        /// Gets or sets the protected item arm name.
        /// </summary>
        public string ProtectedItemName { get; set; }

        /// <summary>
        /// Gets or sets the azure vm arm id.
        /// </summary>
        public string FabricObjectId { get; set; }

        /// <summary>
        /// Gets or sets fabric arm name.
        /// </summary>
        public string FabricName { get; set; }

        /// <summary>
        /// Gets or sets the fabric location.
        /// </summary>
        public string FabricLocation { get; set; }

        /// <summary>
        /// Gets or sets remote fabric arm name.
        /// </summary>
        public string RemoteFabricName { get; set; }

        /// <summary>
        /// Gets or sets remote fabric location.
        /// </summary>
        public string RemoteFabricLocation { get; set; }
    }
}
