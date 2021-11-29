using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    public class Class3
    {
        public void ExecuteOnTest(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    for (int x = 0; x < 100000; x++)
                    {
                        long y = i * x;
                    }
                }
                Console.WriteLine($"Class3 {DateTime.Now:HH:mm:ss.fff}");
            });
        }
    }
}
