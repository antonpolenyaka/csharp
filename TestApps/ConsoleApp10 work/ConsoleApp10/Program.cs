using CERTENROLLLib;
using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace ConsoleApp10
{
    class Program
    {
        static void Main(string[] args)
        {
            bool securityModeOn = true;
            // Create the binding.
            WSHttpBinding myBinding = new WSHttpBinding();
            if (securityModeOn)
            {
                myBinding.Security.Mode = SecurityMode.Transport;
                //myBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
                myBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            }
            else
            {
                myBinding.Security.Mode = SecurityMode.None;
            }

            // Create the endpoint address. Note that the machine name
            // must match the subject or DNS field of the X.509 certificate
            // used to authenticate the service.
            EndpointAddress ea;
            string host = "192.168.23.70";
            if (securityModeOn)
            {
                ea = new EndpointAddress($"https://{host}/TedisNet.Admin.CoreServiceTest");
            }
            else
            {
                ea = new EndpointAddress($"http://{host}/TedisNet.Admin.CoreServiceTest");
            }

            // Create the client. The code for the calculator
            // client is not shown here. See the sample applications
            // for examples of the calculator code.
            //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            ServicePointManager.ServerCertificateValidationCallback += customCertificateValidation;
            using (AdminCoreServiceTestReference.AdminCoreServiceClient cc =
                new AdminCoreServiceTestReference.AdminCoreServiceClient(myBinding, ea))
            {
                // Begin using the client.
                try
                {
                    // string certificateName = "Sitel.TedisNet.Https.Certificate.2";
                    // cc.ClientCredentials.ClientCertificate.SetCertificate(StoreLocation.LocalMachine,
                    //     StoreName.My,
                    //     X509FindType.FindBySubjectName,
                    //     certificateName);
                    cc.Open();
                    int res = cc.MyTestMethod(10);
                    //Console.WriteLine(cc.Add(100, 1111));
                    Console.WriteLine($"res: {res}");

                    // Close the client.
                    cc.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + ex.StackTrace);
                }
            }
            Console.ReadLine();
        }

        private static bool customCertificateValidation(object sender, X509Certificate certificate, X509Chain chain, 
            SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
