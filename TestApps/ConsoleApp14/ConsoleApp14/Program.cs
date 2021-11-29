using System;
using System.Globalization;

namespace ConsoleApp14
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string line = string.Empty;
            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                line += $"{ci.ThreeLetterISOLanguageName};{ci.DisplayName}" + Environment.NewLine;
            }
            Console.WriteLine(line);
            Console.ReadLine();
        }
    }
}
