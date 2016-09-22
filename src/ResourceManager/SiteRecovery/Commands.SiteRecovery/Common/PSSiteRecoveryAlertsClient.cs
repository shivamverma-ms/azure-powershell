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

using Microsoft.Azure.Management.SiteRecovery;
using Microsoft.Azure.Management.SiteRecovery.Models;
using System;
using System.Text.RegularExpressions;

namespace Microsoft.Azure.Commands.SiteRecovery
{
    /// <summary>
    /// Recovery services convenience client.
    /// </summary>
    public partial class PSRecoveryServicesClient
    {
        /// <summary>
        /// Gets the alerts and notification settings.
        /// </summary>
        /// <returns>Alert settings</returns>
        public AlertSettingsListResponse GetAzureRmSiteRecoveryNotificationSettings()
        {
            return this.GetSiteRecoveryClient().AlertSettings.List(this.GetRequestHeaders());
        }

        /// <summary>
        /// Set the alert settings.
        /// </summary>
        /// <param name="input">alert setting input.</param>
        /// <returns></returns>
        public AlertSettingsResponse SetAzureRmSiteRecoveryAlerts(ConfigureAlertSettingsRequest input)
        {
            return this.GetSiteRecoveryClient().AlertSettings.Configure(
                Constants.DefaultAlertSettingName,
                input,
                this.GetRequestHeaders());
        }
    }
}