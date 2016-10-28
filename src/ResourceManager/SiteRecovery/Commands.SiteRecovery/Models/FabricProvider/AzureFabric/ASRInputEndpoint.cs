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

namespace Microsoft.Azure.Commands.SiteRecovery.Models.FabricProvider
{
    /// <summary>
    /// Azure VM input endpoint details.
    /// </summary>
    public class ASRInputEndpoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ASRInputEndpoint" /> class.
        /// </summary>
        public ASRInputEndpoint(InputEndpoint endpoint)
        {
            this.EndpointName = endpoint.EndpointName;
            this.PrivatePort = endpoint.PrivatePort;
            this.PublicPort = endpoint.PublicPort;
            this.Protocol = endpoint.Protocol;
        }

        /// <summary>
        /// Gets or sets the input endpoint name.
        /// </summary>
        public string EndpointName { get; set; }

        /// <summary>
        /// Gets or sets the input endpoint private port.
        /// </summary>
        public int PrivatePort { get; set; }

        /// <summary>
        /// Gets or sets the input endpoint public port.
        /// </summary>
        public int? PublicPort { get; set; }

        /// <summary>
        /// Gets or sets the input endpoint protocol.
        /// </summary>
        public string Protocol { get; set; }

        /// <summary>
        /// Returns a string representation of the object.
        /// </summary>
        /// <returns>Returns a string representing the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("EndpointName     : " + this.EndpointName);
            sb.AppendLine("PrivatePort      : " + this.PrivatePort);
            sb.AppendLine("PublicPort       : " + this.PublicPort);
            sb.AppendLine("Protocol         : " + this.Protocol);

            return sb.ToString();
        }
    }
}
