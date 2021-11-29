using Isotrol;
using Isotrol.IsotrolServiceReference;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace IsotrolClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IsotrolSender sender = new IsotrolSender()
            {
                TimeBetweenCheckTaskStateChangeMsec = 30000,
                MaxWaitProcessTaskMin = 10,
                TimeBetweenRetryRequestMsec = 30000,
                MaxNumRetries = 3,
                StgHostIp = "180.100.100.129",
                StgHostPort = 85,
                StgUsernameToken = "UsernameToken-6AFAE52CE5D01C51E0156148331601546",
                StgUsername = "sitel_externo",
                StgPassword = "$itel2258",
                StgNonce = "d+mL+05kZklAel/TGtgc+A==",
                StgUserId = "83ca42dd-0e6b-4278-900d-a73ae9d5a329"
            };
            sender.LogHandler += Sender_LogHandler;
            try
            {
                //List<string> meterIds = new List<string>() { "LGZ0018251259", "LGZ0018251251" };
                //sender.Send(meterIds, PrimeReport.S21);
                List<string> meterIds = new List<string>() { "CURDE40207600", "CUR3781009500" };
                ResponseMessageType result = sender.Send(meterIds, IsotrolPrimeReport.S14, new DateTime(2019, 3, 1, 1, 0, 0), new DateTime(2019, 03, 22, 1, 0, 0));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error:" + ex.Message);
            }
            Console.ReadKey();
        }

        private static void Sender_LogHandler(object sender, IsotrolLogEventArgs args)
        {
            if (args.IsError)
            {
                if (args.Exception == null)
                {
                    Debug.WriteLine($"Error: {args.Message}");
                }
                else
                {
                    Debug.WriteLine($"Error: {args.Message}. Exception message: {args.Exception.Message}. StackTrace: {args.Exception.StackTrace}");
                }
            }
            else
            {
                Debug.WriteLine(args.Message);
            }
        }
    }
}