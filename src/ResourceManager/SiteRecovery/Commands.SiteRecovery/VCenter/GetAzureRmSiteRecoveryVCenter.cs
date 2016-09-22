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
    /// Retrieves Azure Site Recovery vCenter server.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "AzureRmSiteRecoveryVCenter",
        DefaultParameterSetName = ASRParameterSets.Default)]
    [OutputType(typeof(IEnumerable<ASRVCenter>))]
    public class GetAzureRmSiteRecoveryVCenter : SiteRecoveryCmdletBase
    {
        #region Parameters
        /// <summary>
        /// Gets or sets friendly name of the vCenter.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.ByName, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Fabric server of the vCenter.
        /// </summary>
        [Parameter(ParameterSetName = ASRParameterSets.ByName,
            Mandatory = true,
            ValueFromPipeline = true)]
        [Parameter(ParameterSetName = ASRParameterSets.ByFabricObject,
            Mandatory = true,
            ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRFabric Fabric { get; set; }
        #endregion Parameters

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteSiteRecoveryCmdlet()
        {
            base.ExecuteSiteRecoveryCmdlet();

            switch (this.ParameterSetName)
            {
                case ASRParameterSets.ByName:
                    this.GetVCenterByName();
                    break;
                case ASRParameterSets.ByFabricObject:
                    this.GetAllvCentersInFabric();
                    break;
            }
        }

        /// <summary>
        /// Queries by vCenter name.
        /// </summary>
        private void GetVCenterByName()
        {
            VCenterResponse vcenterResponse =
                 RecoveryServicesClient.GetAzureRmSiteRecoveryVCenter(this.Fabric.Name, this.Name);

            this.WriteVCenter(vcenterResponse.VCenter);
        }

        /// <summary>
        /// Queries by fabric server.
        /// </summary>
        private void GetAllvCentersInFabric()
        {
            VCenterListResponse vCenterListResponse =
                 RecoveryServicesClient.ListAzureRmSiteRecoveryVCenters(this.Fabric.Name);

            this.WritevCenters(vCenterListResponse.VCenters);
        }

        /// <summary>
        /// Write vCenter Objects.
        /// </summary>
        /// <param name="vcenters">List of vCenters</param>
        private void WritevCenters(IList<VCenter> vcenters)
        {
            this.WriteObject(vcenters.Select(p => new ASRVCenter(p)), true);
        }

        /// <summary>
        /// Write vCenter.
        /// </summary>
        /// <param name="vcenter">vCenter object</param>
        private void WriteVCenter(VCenter vcenter)
        {
            this.WriteObject(new ASRVCenter(vcenter));
        }
    }
}