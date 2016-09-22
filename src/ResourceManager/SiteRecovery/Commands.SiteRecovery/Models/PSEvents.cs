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
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Azure.Commands.SiteRecovery
{
    /// <summary>
    /// Azure Site Recovery events.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Keeping all related objects together.")]
    public class ASREvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ASREvent" /> class.
        /// </summary>
        public ASREvent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ASREvent" /> class with required 
        /// parameters.
        /// </summary>
        /// <param name="server">ASR Event object</param>
        public ASREvent(Event backendEvent)
        {
            this.Description = backendEvent.Properties.Description;
            this.EventType = backendEvent.Properties.EventType;
            this.FabricId = backendEvent.Properties.FabricId;
            this.AffectedObjectFriendlyName = backendEvent.Properties.AffectedObjectFriendlyName;
            this.EventCode = backendEvent.Properties.EventCode;
            this.TimeOfOccurence = backendEvent.Properties.TimeOfOccurrence;
            this.Severity = backendEvent.Properties.Severity;
            this.EventSpecificDetails =
                new ASREventSpecificDetails(backendEvent.Properties.EventSpecificDetails);
            this.HealthErrors = this.TranslateHealthErrors(backendEvent.Properties.HealthErrors);
            this.ProviderSpecificDetails =
                new ASREventProviderSpecificDetails(backendEvent.Properties.ProviderDetails);
        }

        /// <summary>
        /// Translate Health errors to Powershell object.
        /// </summary>
        /// <param name="healthErros">Rest API Health error object.</param>
        /// <returns></returns>
        private IList<ASRHealthError> TranslateHealthErrors (IList<HealthError> healthErros)
        {
            IList<ASRHealthError> asrHealthErrors = new List<ASRHealthError>();
            foreach (HealthError healthError in healthErros)
            {
                asrHealthErrors.Add(new ASRHealthError(healthError));
            }

            return asrHealthErrors;
        }

        /// <summary>
        /// Gets or sets the event name.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the event type (VmHealth, VMMHealth).
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// Gets or sets the event fabric arm id.
        /// </summary>
        public string FabricId { get; set; }

        /// <summary>
        /// Gets or sets the event affected object source name.
        /// </summary>
        public string AffectedObjectFriendlyName { get; set; }

        /// <summary>
        /// Gets or sets identifier for the type of the event on the source.
        /// </summary>
        public string EventCode { get; set; }

        /// <summary>
        /// Gets or sets the event time.
        /// </summary>
        public DateTime TimeOfOccurence { get; set; }

        /// <summary>
        /// Gets or sets the severity of the event.
        /// </summary>
        public string Severity { get; set; }

        /// <summary>
        /// Gets or sets the events specific settings.
        /// </summary>
        public ASREventSpecificDetails EventSpecificDetails { get; set; }

        /// <summary>
        /// Gets or sets the errors/warnings associated with the event.
        /// </summary>
        public IList<ASRHealthError> HealthErrors { get; set; }

        /// <summary>
        /// Gets or sets the provider specific settings.
        /// </summary>
        public ASREventProviderSpecificDetails ProviderSpecificDetails { get; set; }
    }

    /// <summary>
    /// The definition of a Protection Container mapping health object.
    /// </summary>
    public class ASRHealthError
    {
        /// <summary>
        /// Initializes a new instance of the HealthError class.
        /// </summary>
        /// <param name="healthError">Event health error object.</param>
        public ASRHealthError(HealthError healthError)
        {
            this.CreationTimeUtc = healthError.CreationTimeUtc;
            this.EntityId = healthError.EntityId;
            this.ErrorCode = healthError.ErrorCode;
            this.ErrorLevel = healthError.ErrorLevel;
            this.ErrorMessage = healthError.ErrorMessage;
            this.PossibleCauses = healthError.PossibleCauses;
            this.RecommendedAction = healthError.RecommendedAction;
            this.RecoveryProviderErrorMessage = healthError.RecoveryProviderErrorMessage;
        }

        /// <summary>
        /// Error creation time (UTC).
        /// </summary>
        public string CreationTimeUtc { get; set; }

        /// <summary>
        /// ID of the entity.
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// Error code.
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Level of error.
        /// </summary>
        public string ErrorLevel { get; set; }

        /// <summary>
        /// Error message.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Possible causes of error.
        /// </summary>
        public string PossibleCauses { get; set; }

        /// <summary>
        /// Recommended action to resolve error.
        /// </summary>
        public string RecommendedAction { get; set; }

        /// <summary>
        /// Recovery Provider error message.
        /// </summary>
        public string RecoveryProviderErrorMessage { get; set; }
    }

    /// <summary>
    /// Model class for provider specific details for an event.
    /// </summary>
    public class ASREventProviderSpecificDetails
    {
        /// <summary>
        /// Initializes a new instance of the EventProviderSpecificDetails class.
        /// </summary>
        public ASREventProviderSpecificDetails(EventProviderSpecificDetails specificeDetails)
        {
            this.InstanceType = specificeDetails.InstanceType;
        }

        /// <summary>
        /// Gets the class type. Overriden in derived classes.
        /// </summary>
        public string InstanceType { get; set; }
    }

    /// <summary>
    /// Model class for event specific details for an event.
    /// </summary>
    public class ASREventSpecificDetails
    {
        /// <summary>
        /// Initializes a new instance of the EventSpecificDetails class.
        /// </summary>
        /// <param name="specificDetails">Rest EventSpecificDetails object.</param>
        public ASREventSpecificDetails(EventSpecificDetails specificDetails)
        {
            this.InstanceType = specificDetails.InstanceType;
        }

        /// <summary>
        /// Gets the class type. Overriden in derived classes.
        /// </summary>
        public string InstanceType { get; set; }
    }
}