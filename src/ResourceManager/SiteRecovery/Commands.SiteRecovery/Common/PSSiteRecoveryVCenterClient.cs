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
using System.Linq;
using System.Text.RegularExpressions;

namespace Microsoft.Azure.Commands.SiteRecovery
{
    /// <summary>
    /// Recovery services convenience client.
    /// </summary>
    public partial class PSRecoveryServicesClient
    {
        /// <summary>
        /// Gets Azure Site Recovery vCenter server.
        /// </summary>
        /// <param name="fabricId">Fabric ID.</param>
        /// <param name="vCenterName">vCenter name.</param>
        /// <returns>vCenter response.</returns>
        public VCenterResponse GetAzureRmSiteRecoveryVCenter(string fabricId, string vCenterName)
        {
            return this.GetSiteRecoveryClient().VCenters.Get(
                fabricId,
                vCenterName,
                this.GetRequestHeaders());
        }

        /// <summary>
        /// Gets all the vCenters in Fabric.
        /// </summary>
        /// <param name="fabricId">Fabric ID.</param>
        /// <returns>vCenter list response.</returns>
        public VCenterListResponse ListAzureRmSiteRecoveryVCenters(string fabricId)
        {
            return this.GetSiteRecoveryClient().VCenters.List(fabricId, this.GetRequestHeaders());
        }

        /// <summary>
        /// Adds the vCenter to Fabric and discovery its VMs.
        /// </summary>
        /// <param name="fabricName">Fabric ID.</param>
        /// <param name="vCenterName">vCenter Name.</param>
        /// <param name="input">Add vCenter input.</param>
        /// <returns>Operation response</returns>
        public LongRunningOperationResponse NewAzureRmSiteRecoveryVCenter(
            string fabricName,
            string vCenterName,
            CreateVCenterInput input)
        {
            return this.GetSiteRecoveryClient().VCenters.BeginCreating(
                fabricName,
                vCenterName,
                input,
                this.GetRequestHeaders());
        }

        /// <summary>
        /// Update the vCenter server.
        /// </summary>
        /// <param name="fabricName">Fabric ID.</param>
        /// <param name="vCenterName">vCenter Name.</param>
        /// <param name="input">Update vCenter input.</param>
        /// <returns>Operation response</returns>
        public LongRunningOperationResponse UpdateAzureRmSiteRecoveryVCenter(
            string fabricName,
            string vCenterName,
            UpdateVCenterInput input)
        {
            return this.GetSiteRecoveryClient().VCenters.BeginUpdating(
                fabricName,
                vCenterName,
                input,
                this.GetRequestHeaders());
        }

        /// <summary>
        /// Refresh Azure Site Recovery Provider.
        /// </summary>
        /// <param name="fabricName">Fabric ID.</param>
        /// <param name="vCenterName">vCenter Name.</param>
        /// <returns>Operation response</returns>
        public LongRunningOperationResponse RemoveAzureRmSiteRecoveryVCenter(
            string fabricName,
            string vCenterName)
        {
            return this.GetSiteRecoveryClient().VCenters.BeginDeleting(
                fabricName,
                vCenterName,
                this.GetRequestHeaders());
        }
    }
}