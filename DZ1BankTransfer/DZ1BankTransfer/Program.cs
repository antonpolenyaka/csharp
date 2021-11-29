using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DZ1BankTransfer
{
    class Program
    {
        private const string _cardSrc = "2445-5900-1167";
        private const string _cardDst = "2445-5833-9378";
        private const int _amount = 12000; // $
        // SHA256 = 64 bytes

        static void Main(string[] args)
        {
            Console.WriteLine("Anton DZ#1!");
            byte[] payEncBytes = File.ReadAllBytes("BankPayRequest.enc");
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding("windows-1254");

            // Get sha256 from "AES.CTR(K,R)||SHA256(R)" Exis 32 bytes
            byte[] originalHash = GetSha256(payEncBytes);

            // Test all pins
            for (int pin = 0; pin < 10000; pin++)
            {
                string myTransferStr = $"Bank SunBank. Src: {_cardSrc}, Dest: {_cardDst}, Amount: {_amount} $, PIN: {pin:0000}.";
                byte[] encMyTransfer = encoding.GetBytes(myTransferStr);
                SHA256 sha256Hash = SHA256.Create();
                byte[] myHash = sha256Hash.ComputeHash(encMyTransfer);
                // My enc is equal to sha256 of original transfer?
                bool isSame = Equality(myHash, originalHash);
                if (isSame)
                {
                    Console.WriteLine($"Pin is: {pin}");
                }
            }

            Console.ReadLine();
        }

        private static byte[] GetSha256(byte[] data)
        {
            byte[] last32 = new byte[32];
            Array.Copy(data, data.Length - 32, last32, 0, 32);
            return last32;
        }

        public static bool Equality(byte[] a1, byte[] b1)
        {
            int i;
            if (a1.Length == b1.Length)
            {
                i = 0;
                while (i < a1.Length && (a1[i] == b1[i])) //Earlier it was a1[i]!=b1[i]
                {
                    i++;
                }
                if (i == a1.Length)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
