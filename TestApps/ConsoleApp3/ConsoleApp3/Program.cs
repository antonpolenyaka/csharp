using System;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            string test = "0123456";
            string recortado = test.PadLeft(10);
            //string recortado = test?.ToString("{0:2}");
            Console.WriteLine("Hello World! '" + recortado + $"' Len{recortado?.Length ?? 0}");
            Console.ReadLine();
        }
    }
}
