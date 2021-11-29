using CERTENROLLLib;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace ConsoleApp11
{
    class Program
    {
        private static ServiceHost _coreServiceHost;
        private static bool _securityModeOn = true;

        static void Main(string[] args)
        {
            string schemeUri = "https";
            _coreServiceHost = OpenService("AdminCoreService",
                typeof(AdminCoreService),
                typeof(IAdminCoreService),
                $"{schemeUri}://localhost/TedisNet.Admin.CoreServiceTest");
            Console.WriteLine("ConsoleApp11 AdminCoreService Started...");
            Console.ReadLine();
            _coreServiceHost.Close();
        }

        public static Uri GetUriAddressService(string hostMy, string serviceUri)
        {
            UriBuilder uriAddress = new UriBuilder(serviceUri);
            string host = uriAddress.Host;
            if (host.Equals("localhost"))
            {
                string baseAddress = "localhost";
                if (baseAddress.Equals("localhost"))
                {
                    baseAddress = Environment.MachineName;
                }
                uriAddress.Host = baseAddress;
            }
            if (uriAddress.Scheme == "http")
            {
                uriAddress.Port = 80;
            }
            else if (uriAddress.Scheme == "https")
            {
                uriAddress.Port = 443;
            }
            else if (uriAddress.Scheme == "net.tcp")
            {
                uriAddress.Port = 8080;
            }
            return uriAddress.Uri;
        }

        private static ServiceHost OpenService(string serviceName, Type serviceType, Type serviceInterfaceType, string serviceUri)
        {
            string hostMy = null;
            Uri uriAddress = GetUriAddressService(hostMy, serviceUri);
            EventLog.WriteEntry("TEST", "WCF Service " + serviceName + " opening...", EventLogEntryType.Information);
            ServiceHost serviceHost = new ServiceHost(serviceType, uriAddress);
            WSHttpBinding binding;
            ServiceMetadataBehavior metadataBehavior = new ServiceMetadataBehavior();
            string certificateName = "Sitel.TedisNet.Https.Certificate.2";
            if (_securityModeOn)
            {
                binding = new WSHttpBinding();
                binding.Security.Mode = SecurityMode.Transport;
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                //binding.Security.Mode = SecurityMode.TransportWithMessageCredential;
                //binding.Security.Mode = SecurityMode.Transport;
                //binding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
                //binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                //binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;

                //metadataBehavior.HttpGetEnabled = false;
                metadataBehavior.HttpsGetEnabled = true;

                // Set the client certificate.
                /*serviceHost.Credentials.ClientCertificate.SetCertificate(
                    StoreLocation.CurrentUser,
                    StoreName.My,
                    X509FindType.FindBySubjectName,
                    "client.com");*/
                X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadWrite);
                bool exist = false;
                foreach (X509Certificate2 certificate in store.Certificates)
                {
                    if (certificate.FriendlyName == certificateName)
                    {
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                {
                    X509Certificate2 certificateNew = CreateSelfSignedCertificate(certificateName);
                    store.Add(certificateNew);
                }
                store.Close();
            }
            else
            {
                binding = new WSHttpBinding(SecurityMode.None);
                metadataBehavior.HttpGetEnabled = true;
                metadataBehavior.HttpsGetEnabled = false;
            }
            binding.Namespace = "http://Sitel.TedisNet.Admin.AdminService";
            serviceHost.AddServiceEndpoint(serviceInterfaceType, binding, uriAddress);
            if (_securityModeOn)
            {
                serviceHost.Credentials.ServiceCertificate.SetCertificate(
                    StoreLocation.LocalMachine,
                    StoreName.My,
                    X509FindType.FindBySubjectName,
                    certificateName);
            }
            serviceHost.Description.Behaviors.Add(metadataBehavior);
            serviceHost.Open();
            EventLog.WriteEntry("test", "WCF Service " + serviceName + " open.", EventLogEntryType.Information);
            return serviceHost;
        }

        public static X509Certificate2 CreateSelfSignedCertificate(string subjectName)
        {
            // create DN for subject and issuer
            var dn = new CX500DistinguishedName();
            dn.Encode("CN=" + subjectName, X500NameFlags.XCN_CERT_NAME_STR_NONE);

            // create a new private key for the certificate
            CX509PrivateKey privateKey = new CX509PrivateKey();
            privateKey.ProviderName = "Microsoft Base Cryptographic Provider v1.0";
            privateKey.MachineContext = true;
            privateKey.Length = 2048;
            privateKey.KeySpec = X509KeySpec.XCN_AT_SIGNATURE; // use is not limited
            privateKey.ExportPolicy = X509PrivateKeyExportFlags.XCN_NCRYPT_ALLOW_PLAINTEXT_EXPORT_FLAG;
            privateKey.Create();

            // Use the stronger SHA512 hashing algorithm
            var hashobj = new CObjectId();
            hashobj.InitializeFromAlgorithmName(ObjectIdGroupId.XCN_CRYPT_HASH_ALG_OID_GROUP_ID,
                ObjectIdPublicKeyFlags.XCN_CRYPT_OID_INFO_PUBKEY_ANY,
                AlgorithmFlags.AlgorithmFlagsNone, "SHA512");

            // add extended key usage if you want - look at MSDN for a list of possible OIDs
            var oid = new CObjectId();
            oid.InitializeFromValue("1.3.6.1.5.5.7.3.1"); // SSL server
            var oidlist = new CObjectIds();
            oidlist.Add(oid);
            var eku = new CX509ExtensionEnhancedKeyUsage();
            eku.InitializeEncode(oidlist);

            // Create the self signing request
            var cert = new CX509CertificateRequestCertificate();
            cert.InitializeFromPrivateKey(X509CertificateEnrollmentContext.ContextMachine, privateKey, "");
            cert.Subject = dn;
            cert.Issuer = dn; // the issuer and the subject are the same
            cert.NotBefore = DateTime.Now;
            // this cert expires immediately. Change to whatever makes sense for you
            cert.NotAfter = DateTime.Now;
            cert.X509Extensions.Add((CX509Extension)eku); // add the EKU
            cert.HashAlgorithm = hashobj; // Specify the hashing algorithm
            cert.Encode(); // encode the certificate

            // Do the final enrollment process
            var enroll = new CX509Enrollment();
            enroll.InitializeFromRequest(cert); // load the certificate
            enroll.CertificateFriendlyName = subjectName; // Optional: add a friendly name
            string csr = enroll.CreateRequest(); // Output the request in base64
                                                 // and install it back as the response
            enroll.InstallResponse(InstallResponseRestrictionFlags.AllowUntrustedCertificate,
                csr, EncodingType.XCN_CRYPT_STRING_BASE64, ""); // no password
                                                                // output a base64 encoded PKCS#12 so we can import it back to the .Net security classes
            var base64encoded = enroll.CreatePFX("", // no password, this is for internal consumption
                PFXExportOptions.PFXExportChainWithRoot);

            // instantiate the target class with the PKCS#12 data (and the empty password)
            return new System.Security.Cryptography.X509Certificates.X509Certificate2(
                System.Convert.FromBase64String(base64encoded), "",
                // mark the private key as exportable (this is usually what you want to do)
                System.Security.Cryptography.X509Certificates.X509KeyStorageFlags.Exportable
            );
        }
    }
}
