using System;

namespace Generator_DomainNames
{
    class Program
    {
        static void Main(string[] args)
        {
            // have a minimum of 3 and a maximum of 63 characters;
            int len = 2;
            string letters = "abcdefghijklmnopqrstuvwxyz-0123456789";
            string dot = ".soccer";
            string result = string.Empty;
            if (len == 1)
            {
                for (int i1 = 0; i1 < letters.Length; i1++)
                {
                    string domainName = $"{letters[i1]}{dot}";
                    result += Environment.NewLine + domainName;
                }
            }
            else if (len == 2)
            {
                for (int i1 = 0; i1 < letters.Length; i1++)
                {
                    for (int i2 = 0; i2 < letters.Length; i2++)
                    {
                        string domainName = $"{letters[i1]}{letters[i2]}{dot}";
                        result += "," + domainName;
                    }
                }
            }
            else if (len == 3)
            {
                for (int i1 = 0; i1 < letters.Length; i1++)
                {
                    for (int i2 = 0; i2 < letters.Length; i2++)
                    {
                        for (int i3 = 0; i3 < letters.Length; i3++)
                        {
                            string domainName = $"{letters[i1]}{letters[i2]}{letters[i3]}{dot}";
                            result += Environment.NewLine + domainName;
                        }
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
