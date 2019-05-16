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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using Microsoft.Azure.Commands.Common.Authentication;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.RecoveryServices.Properties;
using Microsoft.Azure.Management.RecoveryServices.Models;
using Microsoft.Azure.Portal.RecoveryServices.Models.Common;

namespace Microsoft.Azure.Commands.RecoveryServices
{
    /// <summary>
    /// Retrieves Azure Recovery Services Vault Settings File.
    /// </summary>
    [Cmdlet("Get", ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "RecoveryServicesVaultSettingsFile")]
    [OutputType(typeof(VaultSettingsFilePath))]
    public class GetAzureRmRecoveryServicesVaultSettingsFile : RecoveryServicesCmdletBase
    {
        /// <summary>
        /// Expiry in hours for generated certificate.
        /// </summary>
        private const int VaultCertificateExpiryInHoursForHRM = 120;

        /// <summary>
        /// Expiry in hours for generated certificate.
        /// </summary>
        private const int VaultCertificateExpiryInHoursForBackup = 48;

        /// <summary>
        /// Vault Credential version.
        /// </summary>
        public const string VaultCredentialVersionAad = "2.0";

        /// <summary>
        /// Recovery services vault type.
        /// </summary>
        public const string RecoveryServicesVaultType = "Vaults";

        #region Parameters

        /// <summary>
        /// Gets or sets vault Object.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 1)]
        [ValidateNotNullOrEmpty]
        public ARSVault Vault { get; set; }

        /// <summary>
        /// Gets or sets the path where the credential file is to be generated
        /// </summary>
        /// <summary>
        /// Gets or sets vault Object.
        /// </summary>
        [Parameter(Position = 2)]
        public string Path { get; set; }

// TODO: Remove IfDef
#if NETSTANDARD
        /// <summary>
        /// Gets or sets Site Identifier.
        /// </summary>
        [Parameter(ParameterSetName = ARSParameterSets.ForSiteWithCertificate, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string SiteIdentifier { get; set; }

        /// <summary>
        /// Gets or sets Certificate.
        /// </summary>
        [Parameter(ParameterSetName = ARSParameterSets.ForSiteWithCertificate, Mandatory = true)]
        [Parameter(ParameterSetName = ARSParameterSets.ByDefaultWithCertificate, Mandatory = true)]
        [Parameter(ParameterSetName = ARSParameterSets.ForBackupVaultTypeWithCertificate, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Certificate { get; set; }

        /// <summary>
        /// Gets or sets SiteFriendlyName.
        /// </summary>
        [Parameter(ParameterSetName = ARSParameterSets.ForSiteWithCertificate, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string SiteFriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the path where the credential file is to be generated
        /// </summary>
        /// <summary>
        /// Gets or sets vault Object.
        /// </summary>
        [Parameter(ParameterSetName = ARSParameterSets.ByDefaultWithCertificate, Mandatory = false)]
        [Parameter(ParameterSetName = ARSParameterSets.ForSiteWithCertificate, Mandatory = false)]
        public SwitchParameter SiteRecovery
        {
            get { return _siteRecovery; }
            set { _siteRecovery = value; }
        }

        /// <summary>
        /// Gets or sets the path where the credential file is to be generated
        /// </summary>
        /// <summary>
        /// Gets or sets vault Object.
        /// </summary>
        [Parameter(ParameterSetName = ARSParameterSets.ForBackupVaultTypeWithCertificate, Mandatory = true)]
        public SwitchParameter Backup
        {
            get { return _backup; }
            set { _backup = value; }
        }
#else
        /// <summary>
        /// Gets or sets Site Identifier.
        /// </summary>
        [Parameter(ParameterSetName = ARSParameterSets.ForSite, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string SiteIdentifier { get; set; }

        /// <summary>
        /// Gets or sets SiteFriendlyName.
        /// </summary>
        [Parameter(ParameterSetName = ARSParameterSets.ForSite, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string SiteFriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the path where the credential file is to be generated
        /// </summary>
        /// <summary>
        /// Gets or sets vault Object.
        /// </summary>
        [Parameter(ParameterSetName = ARSParameterSets.ByDefault, Mandatory = false)]
        [Parameter(ParameterSetName = ARSParameterSets.ForSite, Mandatory = false)]
        public SwitchParameter SiteRecovery
        {
            get { return _siteRecovery; }
            set { _siteRecovery = value; }
        }

        /// <summary>
        /// Gets or sets the path where the credential file is to be generated
        /// </summary>
        /// <summary>
        /// Gets or sets vault Object.
        /// </summary>
        [Parameter(ParameterSetName = ARSParameterSets.ForBackupVaultType, Mandatory = true)]
        public SwitchParameter Backup
        {
            get { return _backup; }
            set { _backup = value; }
        }
#endif

        private bool _siteRecovery;
        private bool _backup;

        #endregion Parameters

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteCmdlet()
        {   
            try
            {
// TODO: Remove IfDef
#if NETSTANDARD
                if (_backup)
                {
                    GetBackupCredentialsWithCertificate(Certificate);
                }
                else
                {
                    GetSiteRecoveryCredentialsWithCertificate(Certificate);
                }
#else
                if (_backup)
                {
                    this.GetAzureRMRecoveryServicesVaultBackupCredentials();
                }
                else
                {
                    this.GetSiteRecoveryCredentials();
                }
#endif
            }
            catch (AggregateException aggregateEx)
            {
                // if an exception is thrown from a task, it will be wrapped in AggregateException 
                // and propagated to the main thread. Just throwing the first exception in the list.
                var exception = aggregateEx.InnerExceptions.First();
                HandleException(exception);
            }
        }

        /// <summary>
        /// Method to execute the command
        /// </summary>
        private void GetBackupCredentialsWithCertificate(string certificate)
        {  // for .netStandard

            var targetLocation = string.IsNullOrEmpty(Path) ? Utilities.GetDefaultPath() : Path;
            if (!Directory.Exists(targetLocation))
            {
                throw new ArgumentException(Resources.VaultCredPathException);
            }

            var subscriptionId = DefaultContext.Subscription.Id;
            var displayName = Vault.Name;

            WriteDebug(string.Format(CultureInfo.InvariantCulture,
                                      Resources.ExecutingGetVaultCredCmdlet,
                                      subscriptionId, Vault.ResourceGroupName, Vault.Name, targetLocation));

            VaultCertificateResponse vaultCertificateResponse;
            var channelIntegrityKey = string.Empty;
            try
            {
                // Upload cert into ID Mgmt
                WriteDebug(string.Format(CultureInfo.InvariantCulture, Resources.UploadingCertToIdmgmt));
                var bytes = System.Text.Encoding.ASCII.GetBytes("MIIC3TCCAcWgAwIBAgIQQ8YRASgQ879GfkO+RmapijANBgkqhkiG9w0BAQUFADAeMRwwGgYDVQQDExNXaW5kb3dzIEF6dXJlIFRvb2xzMB4XDTE5MDUxNjIwMDIxMloXDTIwMDUxNTIwMTIxMlowHjEcMBoGA1UEAxMTV2luZG93cyBBenVyZSBUb29sczCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAKF + PjIghIyukeeKU9vZqPvYIp5YuoefxQaKwMPpLrdHXuSQmfd7hbLmK0WLDjSaKaR8k / q / NVKTyK3QBgXKLdO0je0AkzYxFvxFe + 80cc0SAm9 + nGpZdXdcr7YZlxXT0Sp2NtXb3 / 3BsKyR + DGc694G / QJ6vCujlEdEiPEcPN46si07sYWJ / OSkSYKe8U5HutoVXlYfE0EKzqK9bh04zjH4j7v1z6k3Po1QoEbUAX15OABiHFyV2ei8B23yjWPiy2Cj + mx / m9HGApIv6aYPjM3 / 7j + Z3q1WQQFZqu + 9q1KNb1s / PWgalvTFlSYnYAVj5BOozoReYTH1yvJu8ET1kPUCAwEAAaMXMBUwEwYDVR0lBAwwCgYIKwYBBQUHAwIwDQYJKoZIhvcNAQEFBQADggEBADXwL3bMCcS2L8yOtovMoBqpgIwxYZ5Fn7lpD08yVrbTJatBuO9Vd1HaGheWlt5fF / Vc7cWjqz2fLykOJnfyyqi02KEth8a946hrN7 + 7jUgGh07LEOxV2hx7doLY5ovxERDbvzFH04dxMkoxT4AaXjgQXXkCcfL2++MrDERbG9YqLXGVi7TNkFpN0PYg3N2modKTpVhD8wocBF7w3IOvR5njfJUVMT18//IZ+8SZlAFk/V2uOEpwS+arS0b/KcJzcUW1+TYr/u3/l4Rt024zitPEbxn1CqZKkNchqzK0ASL/q1k/UIY7yAlS0xp4W8adYwoNwWIi4rhIPhzADYRJLLs =") ;
                var certificateArgs = new CertificateRequest
                {
                    Properties = new RawCertificateData {Certificate = bytes, AuthType = AuthType.AAD}
                };


                var dateString = DateTime.Now.ToString("M-d-yyyy");

                var friendlyName = string.Format("{0}{1}-{2}-vaultcredentials", Vault.Name, subscriptionId, dateString);

                vaultCertificateResponse = RecoveryServicesClient.GetRecoveryServicesClient.VaultCertificates.CreateWithHttpMessagesAsync(
                    Vault.ResourceGroupName,
                    Vault.Name,
                    friendlyName,
                    certificateArgs,
                    RecoveryServicesClient.GetRequestHeaders()).Result.Body;
                WriteDebug(string.Format(CultureInfo.InvariantCulture, Resources.UploadedCertToIdmgmt));
            }
            catch (Exception exception)
            {
                throw exception;
            }

            // generate vault credentials
            var vaultCredsFileContent = GenerateVaultCredsForBackup("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", subscriptionId, vaultCertificateResponse);

            // NOTE: One of the scenarios for this cmdlet is to generate a file which will be an input 
            //       to DPM servers. 
            //       We found a bug in the DPM UI which is looking for a particular namespace in the input file.
            //       The below is a hack to circumvent this issue and this would be removed once the bug can be fixed.
            vaultCredsFileContent = vaultCredsFileContent.Replace("Microsoft.Azure.Commands.AzureBackup.Models",
                "Microsoft.Azure.Portal.RecoveryServices.Models.Common");

            // prepare for download
            var fileName = string.Format("{0}_{1:ddd MMM dd yyyy}.VaultCredentials", displayName, DateTime.UtcNow);
            var filePath = System.IO.Path.Combine(targetLocation, fileName);
            WriteDebug(string.Format(Resources.SavingVaultCred, filePath));

            AzureSession.Instance.DataStore.WriteFile(filePath, Encoding.UTF8.GetBytes(vaultCredsFileContent));

            var output = new VaultSettingsFilePath
            {
                FilePath = filePath,
            };

            // Output filename back to user
            WriteObject(output);
        }

        private void GetSiteRecoveryCredentialsWithCertificate(string certificate)
        {
            var subscriptionId = DefaultContext.Subscription.Id;
            var site = new ASRSite();

            if (!string.IsNullOrEmpty(SiteIdentifier)
                   && !string.IsNullOrEmpty(SiteFriendlyName))
            {
                site.ID = SiteIdentifier;
                site.Name = SiteFriendlyName;
            }
            try
            {
                var fileName = GenerateFileName();

                var filePath = string.IsNullOrEmpty(Path) ? Utilities.GetDefaultPath() : Path;
                var fullFilePath = System.IO.Path.Combine(filePath, fileName);
                // Upload cert into ID Mgmt
                WriteDebug(string.Format(CultureInfo.InvariantCulture, Resources.UploadingCertToIdmgmt));
                var bytes = Encoding.ASCII.GetBytes(certificate);
                var certificateArgs = new CertificateRequest
                {
                    Properties = new RawCertificateData {Certificate = bytes, AuthType = AuthType.AAD}
                };


                var dateString = DateTime.Now.ToString("M-d-yyyy");

                var friendlyName = string.Format("{0}{1}-{2}-vaultcredentials", Vault.Name, subscriptionId, dateString);
                var vaultCertificateResponse = RecoveryServicesClient.GetRecoveryServicesClient.VaultCertificates.CreateWithHttpMessagesAsync(
                    Vault.ResourceGroupName,
                    Vault.Name,
                    friendlyName,
                    certificateArgs,
                    RecoveryServicesClient.GetRequestHeaders()).Result.Body;
                WriteDebug(string.Format(CultureInfo.InvariantCulture, Resources.UploadedCertToIdmgmt));

                var vaultCredsFileContent = GenerateVaultCredsForSiteRecovery(
                        certificate,
                        subscriptionId,
                        vaultCertificateResponse,
                        site);

                WriteDebug(string.Format(Resources.SavingVaultCred, fullFilePath));

                AzureSession.Instance.DataStore.WriteFile(fullFilePath, Encoding.UTF8.GetBytes(vaultCredsFileContent));

                var output = new VaultSettingsFilePath
                {
                    FilePath = fullFilePath,
                };

                // Output filename back to user
                WriteObject(output, true);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Method to execute the command
        /// </summary>
        private void GetSiteRecoveryCredentials()
        {
            var subscription = DefaultProfile.DefaultContext.Subscription;

            // Generate certificate
            var cert = CertUtils.CreateSelfSignedCertificate(
                VaultCertificateExpiryInHoursForHRM,
                subscription.Id,
                Vault.Name);

            var site = new ASRSite();

            if (!string.IsNullOrEmpty(SiteIdentifier)
                   && !string.IsNullOrEmpty(SiteFriendlyName))
            {
                site.ID = SiteIdentifier;
                site.Name = SiteFriendlyName;
            }

            var fileName = GenerateFileName();

            var filePath = string.IsNullOrEmpty(Path) ? Utilities.GetDefaultPath() : Path;

            // Generate file.
            if (RecoveryServicesClient.getVaultAuthType(Vault.ResourceGroupName, Vault.Name) == 0)
            {
                var vaultCreds = RecoveryServicesClient.GenerateVaultCredential(
                    cert,
                    Vault,
                    site,
                    AuthType.ACS);

                // write the content to a file.
                var output = new VaultSettingsFilePath
                {
                    FilePath = Utilities.WriteToFile(vaultCreds, filePath, fileName)
                };

                // print the path to the user.
                WriteObject(output, true);
            }
            else
            {
                var fullFilePath = System.IO.Path.Combine(filePath, fileName);
                WriteDebug(
                     string.Format(
                      CultureInfo.InvariantCulture,
                      Resources.ExecutingGetVaultCredCmdlet,
                      subscription.Id,
                      Vault.ResourceGroupName,
                      Vault.Name,
                      fullFilePath));

                VaultCertificateResponse vaultCertificateResponse;
                try
                {
                    // Upload cert into ID Mgmt
                    WriteDebug(string.Format(CultureInfo.InvariantCulture, Resources.UploadingCertToIdmgmt));
                    vaultCertificateResponse = UploadCert(cert);
                    WriteDebug(string.Format(CultureInfo.InvariantCulture, Resources.UploadedCertToIdmgmt));

                    var managementCert = CertUtils.SerializeCert(cert, X509ContentType.Pfx);
                    // generate vault credentials
                    var vaultCredsFileContent = GenerateVaultCredsForSiteRecovery(
                        managementCert,
                        subscription.Id,
                        vaultCertificateResponse,
                        site);

                    WriteDebug(string.Format(Resources.SavingVaultCred, fullFilePath));

                    AzureSession.Instance.DataStore.WriteFile(fullFilePath, Encoding.UTF8.GetBytes(vaultCredsFileContent));

                    var output = new VaultSettingsFilePath
                    {
                        FilePath = fullFilePath,
                    };

                    // Output filename back to user
                    WriteObject(output, true);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        /// <summary>
        /// Method to generate the file name
        /// </summary>
        /// <returns>file name as string.</returns>
        private string GenerateFileName()
        {
            string fileName;
            const string format = "yyyy-MM-ddTHH-mm-ss";

            if (string.IsNullOrEmpty(SiteIdentifier) || string.IsNullOrEmpty(SiteFriendlyName))
            {
                fileName = string.Format("{0}_{1}.VaultCredentials", Vault.Name, DateTime.UtcNow.ToString(format));
            }
            else
            {
                fileName = string.Format("{0}_{1}_{2}.VaultCredentials", SiteFriendlyName, Vault.Name, DateTime.UtcNow.ToString(format));
            }

            return fileName;
        }

        #region Backup Vault Credentials
        /// <summary>
        /// Get vault credentials for backup vault type.
        /// </summary>
        public void GetAzureRMRecoveryServicesVaultBackupCredentials()
        {
            var targetLocation = string.IsNullOrEmpty(Path) ? Utilities.GetDefaultPath() : Path;
            if (!Directory.Exists(targetLocation))
            {
                throw new ArgumentException(Resources.VaultCredPathException);
            }

            var subscriptionId = DefaultContext.Subscription.Id;
            var displayName = Vault.Name;

            WriteDebug(string.Format(CultureInfo.InvariantCulture,
                                      Resources.ExecutingGetVaultCredCmdlet,
                                      subscriptionId, Vault.ResourceGroupName, Vault.Name, targetLocation));

            // Generate certificate
            var cert = CertUtils.CreateSelfSignedCertificate(
                VaultCertificateExpiryInHoursForBackup, subscriptionId, Vault.Name);

            VaultCertificateResponse vaultCertificateResponse;
            var channelIntegrityKey = string.Empty;
            try
            {
                // Upload cert into ID Mgmt
                WriteDebug(string.Format(CultureInfo.InvariantCulture, Resources.UploadingCertToIdmgmt));
                vaultCertificateResponse = UploadCert(cert);
                WriteDebug(string.Format(CultureInfo.InvariantCulture, Resources.UploadedCertToIdmgmt));
            }
            catch (Exception exception)
            {
                throw exception;
            }

            // generate vault credentials
            var vaultCredsFileContent = GenerateVaultCreds(cert, subscriptionId, vaultCertificateResponse);

            // NOTE: One of the scenarios for this cmdlet is to generate a file which will be an input 
            //       to DPM servers. 
            //       We found a bug in the DPM UI which is looking for a particular namespace in the input file.
            //       The below is a hack to circumvent this issue and this would be removed once the bug can be fixed.
            vaultCredsFileContent = vaultCredsFileContent.Replace("Microsoft.Azure.Commands.AzureBackup.Models",
                "Microsoft.Azure.Portal.RecoveryServices.Models.Common");

            // prepare for download
            var fileName = string.Format("{0}_{1:ddd MMM dd yyyy}.VaultCredentials", displayName, DateTime.UtcNow);
            var filePath = System.IO.Path.Combine(targetLocation, fileName);
            WriteDebug(string.Format(Resources.SavingVaultCred, filePath));

            AzureSession.Instance.DataStore.WriteFile(filePath, Encoding.UTF8.GetBytes(vaultCredsFileContent));

            var output = new VaultSettingsFilePath
            {
                FilePath = filePath,
            };

            // Output filename back to user
            WriteObject(output);
        }

        /// <summary>
        /// Upload certificate
        /// </summary>
        /// <param name="cert">management certificate</param>
        /// <returns>acs namespace of the uploaded cert</returns>
        private VaultCertificateResponse UploadCert(X509Certificate2 cert)
        {
            return RecoveryServicesClient.UploadCertificate(cert, Vault);
        }

        /// <summary>
        /// Generates vault creds file
        /// </summary>
        /// <param name="cert">management certificate</param>
        /// <param name="subscriptionId">subscription Id</param>
        /// <param name="acsNamespace">acs namespace</param>
        /// <returns>xml file in string format</returns>
        private string GenerateVaultCreds(X509Certificate2 cert, string subscriptionId, VaultCertificateResponse vaultCertificateResponse)
        {
            try
            {
                var certString = CertUtils.SerializeCert(cert, X509ContentType.Pfx);
                return GenerateVaultCredsForBackup(certString, subscriptionId, vaultCertificateResponse);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Generates vault creds file content for backup Vault
        /// </summary>
        /// <param name="cert">management certificate</param>
        /// <param name="subscriptionId">subscription Id</param>
        /// <param name="acsNamespace">acs namespace</param>
        /// <returns>xml file in string format</returns>
        private string GenerateVaultCredsForBackup(string certificateString, string subscriptionId,
            VaultCertificateResponse vaultCertificateResponse)
        {
            using (var output = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(output, GetXmlWriterSettings()))
                {
                    var aadDetails = vaultCertificateResponse.Properties as ResourceCertificateAndAadDetails;

                    var vaultCreds = new RSBackupVaultAADCreds
                    {
                        SubscriptionId = subscriptionId,
                        ResourceName = Vault.Name,
                        ManagementCert = certificateString,
                        ResourceId = aadDetails.ResourceId.Value,
                        AadAuthority = aadDetails.AadAuthority,
                        AadTenantId = aadDetails.AadTenantId,
                        ServicePrincipalClientId = aadDetails.ServicePrincipalClientId,
                        IdMgmtRestEndpoint = aadDetails.AzureManagementEndpointAudience,
                        ProviderNamespace = PSRecoveryServicesClient.ProductionRpNamespace,
                        ResourceGroup = Vault.ResourceGroupName,
                        Location = Vault.Location,
                        Version = VaultCredentialVersionAad,
                        ResourceType = RecoveryServicesVaultType,
                        AgentLinks = GetAgentLinks()
                    };

                    var serializer = new DataContractSerializer(typeof(RSBackupVaultAADCreds));
                    serializer.WriteObject(writer, vaultCreds);

                    WriteDebug(string.Format(CultureInfo.InvariantCulture, Resources.BackupVaultSerialized));
                }

                return Encoding.UTF8.GetString(output.ToArray());
            }
        }

        /// <summary>
        /// Generates vault creds file content for Site Recovery Vault
        /// </summary>
        /// <param name="cert">management certificate</param>
        /// <param name="subscriptionId">subscription Id</param>
        /// <param name="vaultCertificateResponse">vaultCertificate Response</param>
        /// <param name="asrSite">asrSite Info</param>
        /// <returns>xml file in string format</returns>
        private string GenerateVaultCredsForSiteRecovery(string managementCert, string subscriptionId,
            VaultCertificateResponse vaultCertificateResponse, ASRSite asrSite)
        {
            using (var output = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(output, GetXmlWriterSettings()))
                {
                    var aadDetails = vaultCertificateResponse.Properties as ResourceCertificateAndAadDetails;
                    var resourceProviderNamespace = string.Empty;
                    var resourceType = string.Empty;

                    Utilities.GetResourceProviderNamespaceAndType(Vault.ID, out resourceProviderNamespace, out resourceType);

                    Logger.Instance.WriteDebug(string.Format(
                        "GenerateVaultCredential resourceProviderNamespace = {0}, resourceType = {1}",
                        resourceProviderNamespace,
                        resourceType));

                    // Update vault settings with the working vault to generate file
                    Utilities.UpdateCurrentVaultContext(new ASRVaultCreds
                    {
                        ResourceGroupName = Vault.ResourceGroupName,
                        ResourceName = Vault.Name,
                        ResourceNamespace = resourceProviderNamespace,
                        ARMResourceType = resourceType
                    });

                    //Code taken from Ibiza code
                    var aadAudience = string.Format(CultureInfo.InvariantCulture,
                        @"https://RecoveryServiceVault/{0}/{1}/{2}",
                        Vault.Location,
                        Vault.Name,
                        aadDetails.ResourceId);

                    var vaultCreds = new RSVaultAsrCreds
                    {
                        VaultDetails = new ASRVaultDetails
                        {
                            SubscriptionId = subscriptionId,
                            ResourceGroup = Vault.ResourceGroupName,
                            ResourceName = Vault.Name,
                            ResourceId = aadDetails.ResourceId.Value,
                            Location = Vault.Location,
                            ResourceType = RecoveryServicesVaultType,
                            ProviderNamespace = PSRecoveryServicesClient.ProductionRpNamespace
                        },
                        ManagementCert = managementCert,
                        Version = VaultCredentialVersionAad,
                        AadDetails = new ASRVaultAadDetails
                        {
                            AadAuthority = aadDetails.AadAuthority,
                            AadTenantId = aadDetails.AadTenantId,
                            ServicePrincipalClientId = aadDetails.ServicePrincipalClientId,
                            AadVaultAudience = aadAudience,
                            ArmManagementEndpoint = aadDetails.AzureManagementEndpointAudience
                        },
                        ChannelIntegrityKey = RecoveryServicesClient.GetCurrentVaultChannelIntegrityKey(),
                        SiteId = asrSite.ID ?? String.Empty,
                        SiteName = asrSite.Name ?? String.Empty
                    };

                    var serializer = new DataContractSerializer(typeof(RSVaultAsrCreds));
                    serializer.WriteObject(writer, vaultCreds);
                }

                return Encoding.UTF8.GetString(output.ToArray());
            }
        }

        /// <summary>
        /// Get Agent Links
        /// </summary>
        /// <returns>Agent links in string format</returns>
        private static string GetAgentLinks()
        {
            return "WABUpdateKBLink,http://go.microsoft.com/fwlink/p/?LinkId=229525;" +
                   "StorageQuotaPurchaseLink,http://go.microsoft.com/fwlink/?LinkId=205490;" +
                   "WebPortalLink,http://go.microsoft.com/fwlink/?LinkId=817353;" +
                   "WABprivacyStatement,http://go.microsoft.com/fwlink/?LinkId=221308";
        }

        /// <summary>
        /// A set of XmlWriterSettings to use for the publishing profile
        /// </summary>
        /// <returns>The XmlWriterSettings set</returns>
        private static XmlWriterSettings GetXmlWriterSettings()
        {
            return new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(false),
                Indent = true,
                NewLineOnAttributes = true
            };
        }

        #endregion
    }
}
