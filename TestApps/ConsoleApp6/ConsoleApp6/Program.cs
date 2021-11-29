using System;
using System.IO;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");
                string ruta = "....";
                if (Directory.Exists(ruta))
                {
                    Console.WriteLine("existe");
                }
                else
                {
                    Console.WriteLine("no existe");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
