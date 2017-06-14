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

using System.Text;
using Microsoft.Azure.Management.SiteRecovery.Models;

namespace Microsoft.Azure.Commands.SiteRecovery.Models.ReplicationProtectedItem
{
    /// <summary>
    /// Azure role assignment details.
    /// </summary>
    public class ASRRoleAssignment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ASRRoleAssignment" /> class.
        /// </summary>
        public ASRRoleAssignment(RoleAssignment role)
        {
            this.Id = role.Id;
            this.Name = role.Name;
            this.Scope = role.Scope;
            this.PrincipalId = role.PrincipalId;
            this.RoleDefinitionId = role.RoleDefinitionId;
        }

        /// <summary>
        /// Gets or sets the ARM Id of the role assignment.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the role assignment.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets role assignment scope.
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// Gets or sets principal Id.
        /// </summary>
        public string PrincipalId { get; set; }

        /// <summary>
        /// Gets or sets role definition id.
        /// </summary>
        public string RoleDefinitionId { get; set; }

        /// <summary>
        /// Returns a string representation of the object.
        /// </summary>
        /// <returns>Returns a string representing the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Id: " + this.Id);
            sb.AppendLine("Name: " + this.Name);
            sb.AppendLine("Scope: " + this.Scope);
            sb.AppendLine("PrincipalId: " + this.PrincipalId);
            sb.AppendLine("RoleDefinitionId: " + this.RoleDefinitionId);

            return sb.ToString();
        }
    }
}
