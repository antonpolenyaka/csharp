using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp15
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var principalContext2 = new PrincipalContext(ContextType.Domain, "sitel-sa.com"))
            {
                // Username and password for authentication.
                string UserNameInDomain = @"sitel-sa.com\apolenyaka";
                string password = "Oordoord890@";
                bool valid = principalContext.ValidateCredentials(UserNameInDomain, password);
                if (!valid)
                {
                    var user = "";
                }
            }
        }
    }
}
