using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.Json;

namespace EMailTest
{
    class Program
    {
        public class MyMailConfig
        {
            public string AlertSendParameterId_EmailSMTPServer { get; set; }
            public string AlertSendParameterId_EmailFromEmail { get; set; }
            public string AlertSendParameterId_EmailSubject { get; set; }
            public bool AlertSendParameterId_EmailIsBodyHtml { get; set; }
            public bool AlertSendParameterId_EmailUseCredentials { get; set; }
            public string AlertSendParameterId_EmailUserCredential { get; set; }
            public string AlertSendParameterId_EmailPasswordCredential { get; set; }
            public int AlertSendParameterId_EmailSMTPPort { get; set; }
            public bool AlertSendParameterId_EmailUseSSL { get; set; }
            public string Mail_Recipient { get; set; }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Start line");
            try
            {
                string json = File.ReadAllText("mailconfig.json");
                var config = JsonSerializer.Deserialize<MyMailConfig>(json, new JsonSerializerOptions(JsonSerializerDefaults.General));
                bool result = Test(config, out bool isSentOk, out string sentResultMessage, out bool sendAlarmAgain);
                Console.WriteLine($"Result {result}: isSentOk: {isSentOk}, sentResultMessage: {sentResultMessage}, sendAlarmAgain: {sendAlarmAgain}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            Console.ReadLine();
        }

        private static bool Test(MyMailConfig config, out bool isSentOk, out string sentResultMessage, out bool sendAlarmAgain)
        {
            List<string> recipients = new List<string>()
            {
                config.Mail_Recipient
            };
            Dictionary<string, object> alertSendParameters = new Dictionary<string, object>
            {
                { "AlertSendParameterId.EmailSMTPServer", config.AlertSendParameterId_EmailSMTPServer },
                { "AlertSendParameterId.EmailFromEmail", config.AlertSendParameterId_EmailFromEmail },
                { "AlertSendParameterId.EmailSubject", config.AlertSendParameterId_EmailSubject },
                { "AlertSendParameterId.EmailIsBodyHtml", config.AlertSendParameterId_EmailIsBodyHtml },
                { "AlertSendParameterId.EmailUseCredentials", config.AlertSendParameterId_EmailUseCredentials },
                { "AlertSendParameterId.EmailUserCredential", config.AlertSendParameterId_EmailUserCredential },
                { "AlertSendParameterId.EmailPasswordCredential", config.AlertSendParameterId_EmailPasswordCredential },
                { "AlertSendParameterId.EmailSMTPPort", config.AlertSendParameterId_EmailSMTPPort },
                { "AlertSendParameterId.EmailUseSSL", config.AlertSendParameterId_EmailUseSSL }
            };
            bool result = Step8SendNotifications(recipients, alertSendParameters,
                messageHeader: "Test " + DateTime.Now.ToString(),
                emailMessage: "Message Test" + DateTime.Now.ToString(),
                alertTag: "Alert Tag",
                executionId: 1,
                out isSentOk,
                out sentResultMessage,
                out sendAlarmAgain);
            return result;
        }

        private static void TestWithoutSSL()
        {
            List<string> recipients = new List<string>()
            {
                "anton@sitel-sa.com"
            };
            Dictionary<string, object> alertSendParameters = new Dictionary<string, object>
            {
                { "AlertSendParameterId.EmailSMTPServer", "smtp.sitel-sa.com" },
                { "AlertSendParameterId.EmailFromEmail", "mantis@sitel-sa.com" },
                { "AlertSendParameterId.EmailSubject", "Email subject" },
                { "AlertSendParameterId.EmailIsBodyHtml", true },
                { "AlertSendParameterId.EmailUseCredentials", true },
                { "AlertSendParameterId.EmailUserCredential", "mcc217c" },
                { "AlertSendParameterId.EmailPasswordCredential", "Mantis123" },
                { "AlertSendParameterId.EmailSMTPPort", 25 },
                { "AlertSendParameterId.EmailUseSSL", false }
            };
            bool result = Step8SendNotifications(recipients, alertSendParameters,
                messageHeader: "Test " + DateTime.Now.ToString(),
                emailMessage: "Message Test" + DateTime.Now.ToString(),
                alertTag: "Alert Tag",
                executionId: 1,
                out bool isSentOk,
                out string sentResultMessage,
                out bool sendAlarmAgain);
        }

        public static bool Step8SendNotifications(List<string> recipients,
                                                Dictionary<string, object> alertSendParameters,
                                                string messageHeader, // We send header by e-mail
                                                string emailMessage, // We send body by e-mail
                                                string alertTag,
                                                int executionId,
                                                out bool isSentOk,
                                                out string sentResultMessage,
                                                out bool sendAlarmAgain)
        {
            sentResultMessage = null;
            sendAlarmAgain = false;
            string server = (string)alertSendParameters["AlertSendParameterId.EmailSMTPServer"];
            string to = string.Empty;
            string from = (string)alertSendParameters["AlertSendParameterId.EmailFromEmail"];
            string subject = $"{(string)alertSendParameters["AlertSendParameterId.EmailSubject"]} {messageHeader}";
            string body = emailMessage;

            // Fill recipients
            to = recipients[0];
            for (int i = 1; i < recipients.Count; i++)
            {
                to += "," + recipients[i];
            }

            // Assemble the message to send
            MailMessage messageToSend = new MailMessage(from, to, subject, body);
            messageToSend.IsBodyHtml = (bool)alertSendParameters["AlertSendParameterId.EmailIsBodyHtml"];

            // We configure the client to send the message
            SmtpClient client = new SmtpClient(server);
            if ((bool)alertSendParameters["AlertSendParameterId.EmailUseCredentials"])
            {
                client.Credentials = new NetworkCredential((string)alertSendParameters["AlertSendParameterId.EmailUserCredential"], (string)alertSendParameters["AlertSendParameterId.EmailPasswordCredential"]);
            }
            client.Port = (int)alertSendParameters["AlertSendParameterId.EmailSMTPPort"];
            client.EnableSsl = (bool)alertSendParameters["AlertSendParameterId.EmailUseSSL"];

            // Send message
            try
            {
                client.Send(messageToSend);
                isSentOk = true;
                sentResultMessage = null;
            }
            catch (Exception ex)
            {
                isSentOk = false;
                string message = ex.Message;
            }

            return true;
        }
    }
}
