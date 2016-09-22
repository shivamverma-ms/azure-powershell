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
    /// Azure Site Recovery Alert settings.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.MaintainabilityRules",
        "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Keeping all related objects together.")]
    public class ASRAlertSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ASRAlertSettings" /> class.
        /// </summary>
        public ASRAlertSettings()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ASRAlertSettings" /> class with required 
        /// parameters.
        /// </summary>
        /// <param name="server">vCenter server object</param>
        public ASRAlertSettings(AlertSettings alertSettings)
        {
            this.CustomEmailAddresses = alertSettings.Properties.CustomEmailAddresses;
            this.EmailSubscriptionOwners = alertSettings.Properties.SendToOwners;
            this.Locale = alertSettings.Properties.Locale;
        }

        /// <summary>
        /// Gets or sets the custom email address for sending emails.
        /// </summary>
        public IList<string> CustomEmailAddresses { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether to send email to subscription owners.
        /// </summary>
        public string EmailSubscriptionOwners {
            get { return emailSubscriptionOwners; }
            set {
                emailSubscriptionOwners = (value.Equals(
                    SendToOwners.Send, StringComparison.InvariantCultureIgnoreCase)) ?
                    SendToOwners.On : SendToOwners.Off;
            }
        }

        /// <summary>
        /// Gets or sets the locale for the email notification.
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// private property to convert subscription owners to on/off.
        /// </summary>
        private string emailSubscriptionOwners;
    }
}