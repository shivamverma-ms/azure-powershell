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

using System.Collections.Generic;
using System.Management.Automation;
using System.Linq;
using Microsoft.Azure.Management.SiteRecovery.Models;

namespace Microsoft.Azure.Commands.SiteRecovery
{
    /// <summary>
    /// Retrieves Azure Site Recovery Event.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "AzureRmSiteRecoveryEvent", DefaultParameterSetName = ASRParameterSets.ByObject)]
    [OutputType(typeof(IEnumerable<ASREvent>))]
    public class GetAzureRmSiteRecoveryEvent : SiteRecoveryCmdletBase
    {
        #region Parameters
        #endregion Parameters

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteSiteRecoveryCmdlet()
        {
            base.ExecuteSiteRecoveryCmdlet();

            switch (this.ParameterSetName)
            {
                case ASRParameterSets.ByObject:
                    this.GetAll();
                    break;
            }
        }

        /// <summary>
        /// Queries all Protected Items under given Protection Container.
        /// </summary>
        private void GetAll()
        {
            EventListResponse azureSiteRecoveryEventList =
                RecoveryServicesClient.GetAzureSiteRecoveryEvent();

            WriteAzureSiteRecoveryEventList(azureSiteRecoveryEventList.Events);
        }

        /// <summary>
        /// Write Protected Items
        /// </summary>
        /// <param name="protectableItems">List of protectable items</param>
        private void WriteAzureSiteRecoveryEventList(IList<Event> events)
        {
            this.WriteObject(events.Select(asrEvent => new ASREvent(asrEvent)), true);
        }
    }
}
