using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    public class AdminCoreService : IAdminCoreService
    {
        public int MyTestMethod(int data)
        {
            return data + 1;
        }
    }
}
