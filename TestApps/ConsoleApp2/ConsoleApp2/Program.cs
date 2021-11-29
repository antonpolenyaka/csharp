using System;
using System.Text.RegularExpressions;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Regex rx = new Regex(@"^u=\S{20};e=\S{4}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // Define a test string.        
            string textOK = "u=W7LZFTNR8S8FFNUY111A;e=BZZZ";
            string textBAD = "u2=W7ZFTNR8S8FFNUY111A;e=BZZZ";

            // Find matches.
            int? count = rx.Matches(textBAD)?.Count;
            if(count.HasValue && count == 1)
            {
                Console.WriteLine("Ok");
            }
            else
            {
                Console.WriteLine("Bad");
            }
            Console.ReadLine();
        }
    }
}
