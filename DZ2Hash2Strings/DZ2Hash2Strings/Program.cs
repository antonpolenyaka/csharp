using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DZ2Hash2Strings
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Anton DZ2!");
            SHA256 sha256 = SHA256.Create();

            Dictionary<string, byte[]> dict = new Dictionary<string, byte[]>();
            bool stop = false;

            for (int i1 = 0; i1 < 256 && !stop; i1++)
            {
                for (int i2 = 0; i2 < 256 && !stop; i2++)
                {
                    for (int i3 = 0; i3 < 256 && !stop; i3++)
                    {
                        for (int i4 = 0; i4 < 256 && !stop; i4++)
                        {
                            byte[] str = new byte[4];
                            str[0] = (byte)i1;
                            str[1] = (byte)i2;
                            str[2] = (byte)i3;
                            str[3] = (byte)i4;
                            byte[] hash = sha256.ComputeHash(str);
                            byte[] partHash = GetFirst4(hash);
                            string partHashStr = BitConverter.ToString(partHash).Replace("-", "");
                            if(!dict.TryAdd(partHashStr, str))
                            {
                                byte[] str1orig = dict[partHashStr];
                                byte[] hash1 = sha256.ComputeHash(str1orig);
                                string str1 = Encoding.UTF8.GetString(str1orig);
                                string str2 = Encoding.UTF8.GetString(str);
                                Console.WriteLine($"Collapse: part hash 4 bytes '{partHashStr}', str1: '{str1}', str2: '{str2}'");
                                string hashStr1 = BitConverter.ToString(hash1).Replace("-", "");
                                string hashStr2 = BitConverter.ToString(hash).Replace("-", "");
                                Console.WriteLine($"Complete hash of str1 ('{str1}'): {hashStr1}");
                                Console.WriteLine($"Complete hash of str1 ('{str2}'): {hashStr2}");
                                stop = true;
                            }
                        }
                    }
                }
            }

            Console.ReadLine();
        }

        private static byte[] GetFirst4(byte[] data)
        {
            byte[] first4 = new byte[4];
            Array.Copy(data, 0, first4, 0, 4);
            return first4;
        }
    }
}
