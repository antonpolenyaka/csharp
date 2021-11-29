using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace ConsoleApp11.Security
{
    public static class SecurityServiceManager
    {
        public static ServiceHost OpenService(string serviceName, Type serviceType, Type serviceInterfaceType, string serviceUri,
            bool securityModeOn = true, bool useIIS = true, string certificateStoreName = "Sitel.TedisNet.Https.Certificate.v1")
        {
            Uri uriAddress = ServerIISManager.GetUriAddressService(host: null, serviceUri);
            ServiceHost serviceHost = new ServiceHost(serviceType, uriAddress);
            Binding binding;
            ServiceMetadataBehavior metadataBehavior = new ServiceMetadataBehavior();
            X509Certificate2 certificate = null;
            if (securityModeOn)
            {
                binding = new BasicHttpsBinding();
                ((BasicHttpsBinding)binding).Security.Mode = BasicHttpsSecurityMode.Transport;
                ((BasicHttpsBinding)binding).Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;

                metadataBehavior.HttpGetEnabled = false;
                metadataBehavior.HttpsGetEnabled = true;

                // Set the client certificate.
                certificate = CertificateManager.AddIfNotExistCertificateToStore(certificateStoreName);
            }
            else
            {
                binding = new WSHttpBinding(SecurityMode.None);
                metadataBehavior.HttpGetEnabled = true;
                metadataBehavior.HttpsGetEnabled = false;
            }
            binding.Namespace = "http://Sitel.TedisNet.Admin.AdminService";
            serviceHost.AddServiceEndpoint(serviceInterfaceType, binding, uriAddress);
            if (securityModeOn)
            {
                // Specify a certificate to authenticate the service.
                if (useIIS)
                {
                    //AddBindings("Default Web Site", "", _certificateStoreName, certificate.GetCertHash());
                    ServerIISManager.AddUpdateBinding(certificate.GetCertHashString(), certStore: "My");
                }
                else
                {
                    CertificateManager.SetCertificateToService(serviceHost, certificateStoreName, certificate);
                }
            }
            certificate = null;
            serviceHost.Description.Behaviors.Add(metadataBehavior);
            serviceHost.Open();
            return serviceHost;
        }
    }
}
