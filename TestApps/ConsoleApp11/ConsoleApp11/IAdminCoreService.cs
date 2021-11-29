using System.ServiceModel;

namespace ConsoleApp11
{
    [ServiceContract(Namespace = "http://Sitel.TedisNet.Admin.AdminService")]
    public interface IAdminCoreService
    {
        [OperationContract()]
        int MyTestMethod(int data);
    }
}
