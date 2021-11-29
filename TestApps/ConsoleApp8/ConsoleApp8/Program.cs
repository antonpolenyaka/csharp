using ConsoleApp8.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    class Program
    {
        static void Main(string[] args)
        {
            StateServiceClient client = new StateServiceClient();
            var aa = client.GetStateEntitySet(new GetStateEntitySetRequest());
        }
    }
}
