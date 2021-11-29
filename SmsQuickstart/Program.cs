using System;
using Azure.Communication;
using Azure.Communication.Sms;

namespace SmsQuickstart
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // This code demonstrates how to fetch your connection string
            // from an environment variable.
            string connectionString = Environment.GetEnvironmentVariable("endpoint=https://comunication-phone.communication.azure.com/;accesskey=AXqQEBvAzv7oT/PBytn5OcxTKpiwF7FV5rQwDej+tiu5w/aemGO3PznN3zI0I3Ws2l5rPhypmEmHNeoRo0K5Ng==");

            SmsClient smsClient = new SmsClient(connectionString);

            smsClient.Send(
                from: new PhoneNumber("<leased-phone-number>"),
                to: new PhoneNumber("+34646595858"),
                message: "Hello World via SMS Anton",
                new SendSmsOptions { EnableDeliveryReport = true } // optional
            );
        }
    }
}
