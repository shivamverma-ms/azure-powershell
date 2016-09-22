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
using System.Management.Automation;

namespace Microsoft.Azure.Commands.SiteRecovery
{
    /// <summary>
    /// Sets Azure Site Recovery alert and nofification settings.
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "AzureRmSiteRecoveryNotification")]
    [OutputType(typeof(IEnumerable<ASRAlertSettings>))]
    public class SetAzureRmSiteRecoveryNotification : SiteRecoveryCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets switch parameter. On passing, command waits till completion.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.SendToOwners, Mandatory = true)]
        [Parameter(ParameterSetName = ASRParameterSets.Set, Mandatory = true)]
        public SwitchParameter EmailSubscriptionOwners { get; set; }

        /// <summary>
        /// Gets or sets the custom email list.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.SetEmail, Mandatory = true)]
        [Parameter(ParameterSetName = ASRParameterSets.Set, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string[] CustomEmailAddresses { get; set; }

        /// <summary>
        /// Gets or sets locale.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.SendToOwners)]
        [Parameter(ParameterSetName = ASRParameterSets.SetEmail)]
        [Parameter(ParameterSetName = ASRParameterSets.Set)]
        [ValidateNotNullOrEmpty]
        public string LocaleID { get; set; }

        /// <summary>
        /// Gets or sets switch parameter. On passing, command waits till completion.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.Disable)]
        public SwitchParameter Disable { get; set; }

        #endregion Parameters

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteSiteRecoveryCmdlet()
        {
            base.ExecuteSiteRecoveryCmdlet();
            switch (this.ParameterSetName)
            {
                case ASRParameterSets.Disable:
                    this.DisableAlerts();
                    break;
                case ASRParameterSets.SendToOwners:
                case ASRParameterSets.SetEmail:
                case ASRParameterSets.Set:
                    this.ConfigureAlertSettings();
                    break;
            }
        }

        /// <summary>
        /// configure the notification settings.
        /// </summary>
        private void ConfigureAlertSettings()
        {
            ConfigureAlertSettingsRequestProperties alertProps =
                new ConfigureAlertSettingsRequestProperties();
            alertProps.CustomEmailAddresses = new List<string>();
            alertProps.Locale = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
            alertProps.SendToOwners = SendToOwners.DoNotSend;

            if (this.EmailSubscriptionOwners.IsPresent)
            {
                alertProps.SendToOwners = SendToOwners.Send;
            }

            if (!string.IsNullOrEmpty(this.LocaleID))
            {
                alertProps.Locale = this.LocaleID;
            }

            if ((this.CustomEmailAddresses != null) && (this.CustomEmailAddresses.Length != 0))
            {
                Utilities.ValidateCustomEmails(this.CustomEmailAddresses);
                alertProps.CustomEmailAddresses = CustomEmailAddresses;
            }

            ConfigureAlertSettingsRequest alertConfig = new ConfigureAlertSettingsRequest();
            alertConfig.Properties = alertProps;

            AlertSettingsResponse alertSettings =
                RecoveryServicesClient.SetAzureRmSiteRecoveryAlerts(alertConfig);

            this.WriteAlert(alertSettings.Alert);
        }

        /// <summary>
        /// Disable the alerts.
        /// </summary>
        private void DisableAlerts()
        {
            ConfigureAlertSettingsRequestProperties alertProps =
                new ConfigureAlertSettingsRequestProperties();
            alertProps.CustomEmailAddresses = new List<string>();
            alertProps.Locale = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
            alertProps.SendToOwners = SendToOwners.DoNotSend;

            ConfigureAlertSettingsRequest alertConfig = new ConfigureAlertSettingsRequest();
            alertConfig.Properties = alertProps;

            AlertSettingsResponse alertSettings =
                RecoveryServicesClient.SetAzureRmSiteRecoveryAlerts(alertConfig);

            this.WriteAlert(alertSettings.Alert);
        }

        /// <summary>
        /// write alerts.
        /// </summary>
        /// <param name="alertSettings"></param>
        private void WriteAlert(AlertSettings alertSettings)
        {
            this.WriteObject(new ASRAlertSettings(alertSettings));
        }
    }
}