using ConsoleApp11.Security;
using System;
using System.ServiceModel;

namespace ConsoleApp11
{
    class Program
    {

        private const int _sslPort = 443;
        private static ServiceHost _coreServiceHost;

        static void Main(string[] args)
        {
            bool useSecurity = true;
            string schemeUri;
            bool securityModeOn;
            bool useIIS;
            if (useSecurity)
            {
                schemeUri = "https";
                securityModeOn = true;
                useIIS = true;
            }
            else
            {
                schemeUri = "http";
                securityModeOn = false;
                useIIS = false;
            }
            _coreServiceHost = SecurityServiceManager.OpenService(serviceName: "AdminCoreService",
                serviceType: typeof(AdminCoreService),
                serviceInterfaceType: typeof(IAdminCoreService),
                serviceUri: $"{schemeUri}://localhost/TedisNet.Admin.CoreServiceTest",
                securityModeOn,
                useIIS);
            Console.WriteLine("ConsoleApp11 AdminCoreService Started...");
            Console.ReadLine();
            _coreServiceHost.Close();
        }
    }
}
