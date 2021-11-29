using Microsoft.Web.Administration;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp11.Security
{
    public static class ServerIISManager
    {
        public static string GetBindingsInformation(Site site)
        {
            string bindingDisplay = null;
            foreach (Binding binding in site.Bindings)
            {
                bindingDisplay = bindingDisplay + "  binding2:\n   binding2Information: " +
                    binding.BindingInformation;
                if (binding.Protocol == "https")
                {
                    // There is a CertificateHash and  
                    // CertificateStoreName for the https protocol only.
                    bindingDisplay = bindingDisplay + "\n   CertificateHash: " +
                        binding.CertificateHash + ": ";
                    // Display the hash.
                    foreach (byte certhashbyte in binding.CertificateHash)
                    {
                        bindingDisplay = bindingDisplay + certhashbyte.ToString() + " ";
                    }
                    bindingDisplay = bindingDisplay + "\n   CertificateStoreName: " +
                        binding.CertificateStoreName;
                }
                bindingDisplay = bindingDisplay + "\n   EndPoint: " + binding.EndPoint;
                bindingDisplay = bindingDisplay + "\n   Host: " + binding.Host;
                bindingDisplay = bindingDisplay + "\n   IsIPPortHostbinding2: " + binding.IsIPPortHostBinding;
                bindingDisplay = bindingDisplay + "\n   Protocol: " + binding.Protocol;
                bindingDisplay = bindingDisplay + "\n   ToString: " + binding.ToString();
                bindingDisplay = bindingDisplay + "\n   UseDsMapper: " + binding.UseDsMapper + "\n\n";
            }
            return bindingDisplay;
        }

        //private static void AddCertificateToNewBinding()
        //{
        //    ServerManager mgr = GetServerManager();
        //    Site site = GetSiteByName(mgr);
        //}

        private static ServerManager GetServerManager(string server = null)
        {
            ServerManager mgr;
            if (string.IsNullOrEmpty(server))
            {
                mgr = new ServerManager();
            }
            else
            {
                mgr = ServerManager.OpenRemote(server);
            }
            return mgr;
        }

        private static Site GetSiteByName(ServerManager mgr, string siteName = "Default Web Site")
        {
            Site site = mgr.Sites[siteName];
            return site;
        }

        /// <summary>
        /// Add certificate from certificate store to default binding in selected port for type https.
        /// 
        /// Cert hash is like "5792a4723338ae88e4da3cdd14d3f578d870caea".
        /// Cert store is like "My", "localmachine", etc.
        /// </summary>
        public static void AddUpdateBinding(string certHash, string certStore, int port = 443, Site site = null, ServerManager mgr = null)
        {
            try
            {
                if (mgr == null)
                {
                    mgr = GetServerManager();
                }

                if (site == null)
                {
                    site = GetSiteByName(mgr);
                }

                string bindingProtocol = "https";
                string bindingInfo = $"*:{port}:";

                // Find if binding already exist
                Binding binding = GetBinding(site, bindingProtocol, bindingInfo);

                // If binding not exist - create new
                if (binding == null)
                {
                    binding = site.Bindings.Add(bindingInfo, "https");
                    mgr.CommitChanges();
                }

                if (!string.IsNullOrEmpty(certHash))
                {
                    ConfigurationMethod method = binding.Methods["AddSslCertificate"];
                    if (method == null)
                    {
                        throw new Exception("Unable to access the AddSslCertificate configuration method");
                    }

                    ConfigurationMethodInstance mi = method.CreateInstance();
                    mi.Input.SetAttributeValue("certificateHash", certHash);
                    mi.Input.SetAttributeValue("certificateStoreName", certStore);
                    mi.Execute();

                    // Added certificate
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private static Binding GetBinding(Site site, string bindingProtocol, string bindingInfo)
        {
            Binding binding = null;
            foreach (Binding b in site.Bindings)
            {
                if (b.Protocol.Equals(bindingProtocol)
                    && b.BindingInformation.Equals(bindingInfo))
                {
                    binding = b;
                    break;
                }
            }
            return binding;
        }

        public static bool RemoveCertificate(string bindingProtocol = "https", int port = 443, Site site = null, ServerManager mgr = null)
        {
            string bindingInfo = $"*:{port}:";

            if (mgr == null)
            {
                mgr = GetServerManager();
            }

            if (site == null)
            {
                site = GetSiteByName(mgr);
            }

            Binding binding = GetBinding(site, bindingProtocol, bindingInfo);
            ConfigurationMethod method = binding.Methods["RemoveSslCertificate"];
            if (method == null)
            {
                throw new Exception("Unable to access the RemoveSslCertificate configuration method");
            }

            ConfigurationMethodInstance mi = method.CreateInstance();
            mi.Execute();

            // Removed certificate
            return true;
        }

        public static Uri GetUriAddressService(string host, string serviceUri)
        {
            UriBuilder uriAddress = new UriBuilder(serviceUri);
            string uriHost = uriAddress.Host;
            if (uriHost.Equals("localhost"))
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

        private static void AddBindings(string sitename, string hostname, int sslPort, string certificateStoreName, byte[] certificateHash)
        {
            //http:*:80:,https:*:443:
            Microsoft.Web.Administration.ServerManager serverMgr = new Microsoft.Web.Administration.ServerManager();

            // without IIS object serverMgr.Sites not exist, only serverMgr
            Microsoft.Web.Administration.Site mySite = null;
            if (serverMgr.Sites.GetCollection().Any(s => (string)s.GetAttributeValue("name") == sitename))
            {
                mySite = serverMgr.Sites[sitename];
            }

            // Check if binding already exist
            if (!mySite.Bindings.Any(b => b.Protocol == "https" && b.Host == hostname && b.EndPoint.Port == sslPort))
            {
                var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.OpenExistingOnly);
                var certificates = store.Certificates.Find(X509FindType.FindBySubjectName, certificateStoreName, false);

                string bindingInformation = string.Format("{0}:{1}:{2}", "*", sslPort, hostname);
                Microsoft.Web.Administration.Binding binding = mySite.Bindings.Add(bindingInformation, "https");

                Microsoft.Web.Administration.ConfigurationMethod method = binding.Methods["AddSslCertificate"];
                if (method == null)
                {
                    throw new Exception("Unable to access the AddSslCertificate configuration method");
                }
                Microsoft.Web.Administration.ConfigurationMethodInstance mi = method.CreateInstance();
                mi.Input.SetAttributeValue("certificateHash", certificates[0].GetCertHashString());
                mi.Input.SetAttributeValue("certificateStoreName", certificates[0].IssuerName.Name);
                mi.Execute();

                store.Close();
            }
            mySite.ServerAutoStart = true;

            serverMgr.CommitChanges();
        }
    }
}
