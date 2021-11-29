using Isotrol.IsotrolServiceReference;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Xml;

namespace Isotrol
{
    public class IsotrolSender
    {
        #region Properties
        public int TimeBetweenCheckTaskStateChangeMsec { get; set; } = 30000;
        public int MaxWaitProcessTaskMin { get; set; } = 10;
        public int TimeBetweenRetryRequestMsec { get; set; } = 30000;
        public int MaxNumRetries { get; set; } = 3;
        public string StgHostIp { get; set; } = "180.100.100.129"; // 180.100.100.129
        public ushort StgHostPort { get; set; } = 85;
        public string StgUsernameToken { get; set; } = "UsernameToken-6AFAE52CE5D01C51E0156148331601546";
        public string StgUsername { get; set; } = "sitel_externo";
        public string StgPassword { get; set; } = "$itel2258";
        public string StgNonce { get; set; } = "d+mL+05kZklAel/TGtgc+A==";
        public string StgUserId { get; set; } = "83ca42dd-0e6b-4278-900d-a73ae9d5a329";
        public string StgUrl => $"https://{StgHostIp}:{StgHostPort}/WsCimSoapService";
        #endregion

        #region Constructors
        public IsotrolSender()
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback
            (
                delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                }
            );
        }
        #endregion

        #region Log
        public event EventHandler<IsotrolLogEventArgs> LogHandler;
        private void OnLogWriteLine(bool isError, string message, Exception exception)
        {
            LogHandler?.Invoke(this, new IsotrolLogEventArgs()
            {
                IsError = isError,
                Message = message,
                Exception = exception,
                Timestamp = DateTime.Now
            });
        }
        #endregion

        #region Methods
        /// <summary>
        /// Send and retriev data from meter
        /// </summary>
        /// <param name="meterId">Example: LGZ0018251259</param>
        public ResponseMessageType Send(List<string> meterIds, IsotrolPrimeReport primeReport, DateTime? startTime = null, DateTime? endTime = null)
        {
            ResponseMessageType responseSoapResult = null;

            for (int numRetrie = 0; numRetrie < MaxNumRetries && responseSoapResult == null; numRetrie++)
            {
                if (numRetrie > 0)
                {
                    OnLogWriteLine(false, $"Wait before retrie send request meter reading. {TimeBetweenRetryRequestMsec} msec.", null);
                    Thread.Sleep(TimeBetweenRetryRequestMsec);
                }

                string logMeterIds = string.Join(",", meterIds);
                OnLogWriteLine(false, $"Send request meter reading. Num retrie: '{numRetrie}', meter id's: '{logMeterIds}'", null);
                (Exception errorMeter, ReplyTypeResult replyResult, string taskId, state taskState) = CreateSendRequestMeterReadings(
                    meterIds, StgUsernameToken, StgUsername, StgPassword, StgNonce, StgUserId, StgUrl, StgHostIp, StgHostPort, primeReport, startTime, endTime);

                if (errorMeter != null)
                {
                    OnLogWriteLine(true, $"Error in create and send request for meter readings. Error: {errorMeter.Message}, meter id's: '{logMeterIds}'", null);
                    continue;
                }

                OnLogWriteLine(false, $"Check if request is received. Result: '{replyResult}', meter id's: '{logMeterIds}'", null);
                if (replyResult != ReplyTypeResult.OK)
                {
                    OnLogWriteLine(true, $"Retry create send request meter readings to STG. Result: '{replyResult}', meter id's: '{logMeterIds}'", null);
                    continue;
                }
                else
                {
                    (Exception errorTask, state taskInfoState) = CreateSendRequestTaskInfo(
                        taskState, taskId, meterIds, StgUsernameToken, StgUsername, StgPassword, StgNonce, StgUserId, StgUrl, StgHostIp, StgHostPort);

                    // If task is COMPLETED, call to get data
                    OnLogWriteLine(false, $"Check if request is received about task info. Result: '{replyResult}', task id: '{taskId}'", null);
                    if (taskInfoState != state.COMPLETED)
                    {
                        // Don't do nothing go to next retrie
                        OnLogWriteLine(true, $"Data result not received, go to next complete retrie. Result: '{replyResult}', task id: '{taskId}'", null);
                        continue;
                    }
                    else
                    {
                        (Exception errorReport, ResponseMessageType responseSoap) = CreateSendRequestReportResult(
                            taskId, StgUsernameToken, StgUsername, StgPassword, StgNonce, StgUserId, meterIds, StgUrl, StgHostIp, StgHostPort);
                        if (errorReport != null)
                        {
                            OnLogWriteLine(true, $"Error in create and send request for report result. Error: {errorMeter.Message}, task id: '{taskId}'", null);
                            continue;
                        }
                        else
                        {
                            replyResult = responseSoap.Reply.Result;
                            OnLogWriteLine(false, $"Data result received. Result: '{replyResult}', task id: '{taskId}'", null);
                            if (replyResult == ReplyTypeResult.OK)
                            {
                                responseSoapResult = responseSoap;
                                // Exit from retries, we already have data of task                                
                            }
                        }
                    }
                }
            }
            OnLogWriteLine(false, $"Finish sending and retriev data from meters", null);
            return responseSoapResult;
        }

        private (Exception error, ResponseMessageType responseSoap) CreateSendRequestReportResult(
            string taskId, string usernameToken, string username, string password, string nonce, string userId, List<string> meterIds, string url, string hostIp, ushort hostPort)
        {
            string logMeterIds = string.Join(",", meterIds);
            ResponseMessageType responseSoap = null;

            OnLogWriteLine(false, $"Generate xml to get data of task. Task id: '{taskId}', meter id's: '{logMeterIds}'", null);
            string dataXmlTask = GetReportResultSoapXml(taskId, usernameToken, username, password, nonce, userId);

            OnLogWriteLine(false, $"Send request to get data from STG of task to url '{url}'. Task id: '{taskId}', meter id's: '{logMeterIds}'", null);
            (Exception error, string responseXml) = PostSoapXmlAndGetResponseSoapXml(url, dataXmlTask, hostIp, hostPort);

            if (error == null)
            {
                OnLogWriteLine(false, $"Parse response from STG about task state. Task id: '{taskId}', meter id's: '{logMeterIds}'", null);
                responseSoap = ParseResponseSoapXML(responseXml);
            }

            return (error, responseSoap);
        }

        private (Exception error, state taskState) CreateSendRequestTaskInfo(
            state taskState, string taskId, List<string> meterIds, string usernameToken, string username, string password, string nonce, string userId, string url, string hostIp, ushort hostPort)
        {
            string logMeterIds = string.Join(",", meterIds);
            Exception error = null;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            // Reply OK, now we wait state processed with MaxTimeout
            DateTime maxTimeout = DateTime.Now.AddMinutes(MaxWaitProcessTaskMin);
            OnLogWriteLine(false, $"Wait response for the task until {maxTimeout.ToString("HH:mm:ss")}. Task id: '{taskId}', meter id's: '{logMeterIds}'", null);

            while (taskState != state.COMPLETED && taskState != state.FAILED && DateTime.Now < maxTimeout)
            {
                OnLogWriteLine(false, $"Test if task is completed {DateTime.Now.ToString("HH:mm:ss")}. Task id: '{taskId}', meter id's: '{logMeterIds}'", null);
                // Wait time
                Thread.Sleep(TimeBetweenCheckTaskStateChangeMsec);

                OnLogWriteLine(false, $"Generate xml to know state of task. Task id: '{taskId}', meter id's: '{logMeterIds}'", null);
                string dataXmlTask = GetTaskInfoSoapXml(taskId, usernameToken, username, password, nonce, userId);

                OnLogWriteLine(false, $"Send request to STG for know state of task to url '{url}'. Task id: '{taskId}', meter id's: '{logMeterIds}'", null);
                (Exception errorPost, string responseXml) = PostSoapXmlAndGetResponseSoapXml(url, dataXmlTask, hostIp, hostPort);

                if (errorPost != null)
                {
                    OnLogWriteLine(true, $"Error in post SOAP and get response. Error: {errorPost.Message}. Task id: '{taskId}', meter id's: '{logMeterIds}'", null);
                    error = errorPost;
                }
                else
                {
                    error = null;
                    OnLogWriteLine(false, $"Parse response from STG about task state. Task id: '{taskId}', meter id's: '{logMeterIds}'", null);
                    ResponseMessageType responseSoapTask = ParseResponseSoapXML(responseXml);
                    if (responseSoapTask.Reply.Result == ReplyTypeResult.OK)
                    {
                        TaskInfo taskInfo = (TaskInfo)responseSoapTask.Payload.Item;
                        taskState = taskInfo.State;
                    }
                    OnLogWriteLine(false, $"Result parse task info. Result: '{responseSoapTask.Reply.Result}', " +
                        $"task state: {((TaskInfo)responseSoapTask.Payload.Item).State}, task id: '{taskId}', meter id's: '{logMeterIds}'", null);
                }
            }
            sw.Stop();

            OnLogWriteLine(false, $"Finish wait state of task. State: {taskState}, task id : {taskId}, meter id's: '{logMeterIds}', used time (Milliseconds):{sw.ElapsedMilliseconds}", null);
            return (error, taskState);
        }

        private (Exception error, ReplyTypeResult replyResult, string taskId, state taskState) CreateSendRequestMeterReadings(
            List<string> meterIds, string usernameToken, string username, string password, string nonce, string userId, string url, string hostIp, ushort hostPort,
            IsotrolPrimeReport primeReport, DateTime? startTime = null, DateTime? endTime = null)
        {
            string logMeterIds = string.Join(",", meterIds);
            OnLogWriteLine(false, $"Generate Xml to send. Meter id's '{logMeterIds}'", null);
            string dataXmlMeter = GetMeterReadingsSoapXml(meterIds, usernameToken, username, password, nonce, userId, primeReport, startTime, endTime);

            // Send soap Xml and receive soap Xml response
            OnLogWriteLine(false, $"Send post request to STG and receive response to url '{url}'. Meter id's '{logMeterIds}'", null);
            (Exception error, string responseXml) = PostSoapXmlAndGetResponseSoapXml(url, dataXmlMeter, hostIp, hostPort);

            (Exception error, ReplyTypeResult replyResult, string taskId, state taskState) result;
            if (error != null)
            {
                result = (error, ReplyTypeResult.FAILED, null, state.FAILED);
            }
            else
            {
                // Parse
                OnLogWriteLine(false, $"Parse response from STG. Meter id's: '{logMeterIds}'", null);
                ResponseMessageType responseSoapMeter = ParseResponseSoapXML(responseXml);
                TaskInfo taskInfo = (TaskInfo)responseSoapMeter.Payload.Item;
                result = (error, responseSoapMeter.Reply.Result, taskInfo.Id, taskInfo.State);
            }
            return result;
        }

        private ResponseMessageType ParseResponseSoapXML(string responseXml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(responseXml);
            XmlNode root = doc.DocumentElement;

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("s", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("u", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
            nsmgr.AddNamespace("o", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
            nsmgr.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            nsmgr.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");

            // Get data form soap XML
            string created = doc.SelectSingleNode("//s:Envelope/s:Header/o:Security/u:Timestamp/u:Created", nsmgr).InnerText;
            string expires = doc.SelectSingleNode("//s:Envelope/s:Header/o:Security/u:Timestamp/u:Expires", nsmgr).InnerText;
            XmlNode nodeBody = doc.SelectSingleNode("//s:Envelope/s:Body", nsmgr);
            string verb = nodeBody["Response"]["Header"]["Verb"].InnerText;
            string noun = nodeBody["Response"]["Header"]["Noun"].InnerText;
            string revision = nodeBody["Response"]["Header"]["Revision"].InnerText;
            string timestampHeader = nodeBody["Response"]["Header"]["Timestamp"].InnerText;
            string source = nodeBody["Response"]["Header"]["Source"].InnerText;
            string result = nodeBody["Response"]["Reply"]["Result"].InnerText;

            // Get object Item
            object item = null;
            XmlNode nodeTaskInfo = nodeBody["Response"]["Payload"].GetElementsByTagName("TaskInfo")?.Item(0);
            XmlNode nodeMeterReadings = nodeBody["Response"]["Payload"].GetElementsByTagName("MeterReadings")?.Item(0);
            if (nodeTaskInfo != null)
            {
                string id = nodeBody["Response"]["Payload"]["TaskInfo"]["Id"].InnerText;
                string state = nodeBody["Response"]["Payload"]["TaskInfo"]["State"].InnerText;
                item = new TaskInfo()
                {
                    Id = id,
                    State = (state)Enum.Parse(typeof(state), state)
                };
            }
            else if (nodeMeterReadings != null)
            {
                string meterMRid = nodeMeterReadings["MeterReading"]["Meter"]["mRID"].InnerText;
                XmlNodeList nodesReadings = nodeMeterReadings["MeterReading"].GetElementsByTagName("Readings");
                XmlNodeList nodesReadingTypes = nodeBody["Response"]["Payload"]["Response"]["Payload"]["MeterReadings"].GetElementsByTagName("ReadingType");

                List<Readings> readings = null;
                if (nodesReadings != null)
                {
                    readings = new List<Readings>();
                    for (int i = 0; i < nodesReadings.Count; i++)
                    {
                        string timeStamp = nodesReadings[i]["timeStamp"].InnerText;
                        string value = nodesReadings[i]["value"].InnerText;
                        string readingTypeRef = nodesReadings[i]["ReadingType"].Attributes["ref"].InnerText;

                        readings.Add(new Readings()
                        {
                            timeStamp = DateTime.Parse(timeStamp),
                            value = float.Parse(value),
                            ReadingType = new ReadingsReadingType() { @ref = readingTypeRef }
                        });
                    }
                }
                List<ReadingType> readingTypes = null;
                if (nodesReadingTypes != null)
                {
                    readingTypes = new List<ReadingType>();
                    foreach (XmlNode nodesReadingType in nodesReadingTypes)
                    {
                        if (nodesReadingType.HasChildNodes)
                        {
                            string mRID = nodesReadingType["mRID"].InnerText;
                            string aliasName = nodesReadingType["aliasName"].InnerText;

                            readingTypes.Add(new ReadingType()
                            {
                                mRID = mRID,
                                aliasName = aliasName
                            });
                        }
                    }
                }

                // Readings
                item = new MeterReadings()
                {
                    MeterReading = new MeterReading[]
                    {
                        new MeterReading()
                        {
                            Meter = new Meter() { mRID = meterMRid },
                            Readings = readings?.ToArray()
                        },
                    },
                    ReadingType = readingTypes?.ToArray()
                };
            }

            // Generate response
            ResponseMessageType soapResponse = new ResponseMessageType
            {
                Header = new HeaderType
                {
                    Verb = (HeaderTypeVerb)Enum.Parse(typeof(HeaderTypeVerb), verb),
                    Noun = noun,
                    Revision = revision,
                    Timestamp = DateTime.Parse(timestampHeader),
                    Source = source
                },
                Reply = new ReplyType()
                {
                    Result = (ReplyTypeResult)Enum.Parse(typeof(ReplyTypeResult), result)
                },
                Payload = new PayloadType()
                {
                    Item = item
                }
            };

            return soapResponse;
        }

        private string GetReportResultSoapXml(string taskId, string usernameToken, string username, string password, string nonce, string userId)
        {
            // <wsu:Created>2019-06-25T19:55:00.015Z</wsu:Created>
            string timestamp = $"{DateTime.Now.ToString("yyyy-MM-dd")}T{DateTime.Now.ToString("HH:mm:ss.fff")}Z";
            string dataXml = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:mes=""http://iec.ch/TC57/2011/schema/message"">
                                    <soapenv:Header>
                                        <wsse:Security soapenv:mustUnderstand=""1"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"">
                                            <wsse:UsernameToken wsu:Id=""{usernameToken}"">
                                                <wsse:Username>{username}</wsse:Username>
                                                <wsse:Password Type=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText"">{password}</wsse:Password>
                                                <wsse:Nonce EncodingType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary"">{nonce}</wsse:Nonce>
                                                <wsu:Created>{timestamp}</wsu:Created>
                                            </wsse:UsernameToken>
                                        </wsse:Security>
                                    </soapenv:Header>
                                    <soapenv:Body>
                                        <mes:Request>
                                            <mes:Header>
                                                <mes:Verb>get</mes:Verb>
                                                <mes:Noun>ReportResult</mes:Noun>
                                                <mes:AsyncReplyFlag>false</mes:AsyncReplyFlag>
                                                <mes:User>
                                                    <mes:UserID>{userId}</mes:UserID>
                                                </mes:User>
                                                <mes:Property>
                                                    <mes:Name>CompressFlag</mes:Name>
                                                    <mes:Value>false</mes:Value>
                                                </mes:Property>
                                            </mes:Header>
                                            <mes:Request>
         		                                <!-- ID de la tarea a consultar -->
                                                <mes:ID>{taskId}</mes:ID>
                                            </mes:Request>
                                        </mes:Request>
                                    </soapenv:Body>
                                </soapenv:Envelope>";
            return dataXml;
        }

        private string GetTaskInfoSoapXml(string taskId, string usernameToken, string username, string password, string nonce, string userId)
        {
            // <wsu:Created>2019-06-25T19:55:00.015Z</wsu:Created>
            string timestamp = $"{DateTime.Now.ToString("yyyy-MM-dd")}T{DateTime.Now.ToString("HH:mm:ss.fff")}Z";
            string dataXml = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:mes=""http://iec.ch/TC57/2011/schema/message"">
                                    <soapenv:Header>
                                        <wsse:Security soapenv:mustUnderstand=""1"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"">
                                            <wsse:UsernameToken wsu:Id=""{usernameToken}"">
                                                <wsse:Username>{username}</wsse:Username>
                                                <wsse:Password Type=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText"">{password}</wsse:Password>
                                                <wsse:Nonce EncodingType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary"">{nonce}</wsse:Nonce>
                                                <wsu:Created>{timestamp}</wsu:Created>
                                            </wsse:UsernameToken>
                                        </wsse:Security>
                                    </soapenv:Header>
                                    <soapenv:Body>
                                        <mes:Request>
                                            <mes:Header>
                                                <mes:Verb>get</mes:Verb>
                                                <mes:Noun>TaskInfo</mes:Noun>
                                                <mes:AsyncReplyFlag>false</mes:AsyncReplyFlag>
                                                <mes:User>
                                                    <mes:UserID>{userId}</mes:UserID>
                                                </mes:User>
                                                <mes:Property>
                                                    <mes:Name>CompressFlag</mes:Name>
                                                    <mes:Value>false</mes:Value>
                                                </mes:Property>
                                            </mes:Header>
                                            <mes:Request>
                                                <!-- ID de la tarea a consultar -->
                                                <mes:ID>{taskId}</mes:ID>
                                            </mes:Request>
                                        </mes:Request>
                                    </soapenv:Body>
                                </soapenv:Envelope>";
            return dataXml;
        }

        /// <summary>
        /// </summary>
        /// <param name="meterIds">Code of meters. Example: LGZ0018251259, LGZ0018251251</param>
        /// <returns></returns>
        private string GetMeterReadingsSoapXml(List<string> meterIds, string usernameToken, string username, string password, string nonce, string userId,
            IsotrolPrimeReport primeReport, DateTime? startTime = null, DateTime? endTime = null)
        {
            string dataXml = null;
            if (primeReport != IsotrolPrimeReport.Undefined)
            {
                // <wsu:Created>2019-06-25T19:55:00.015Z</wsu:Created>
                string timestamp = $"{DateTime.Now.ToString("yyyy-MM-dd")}T{DateTime.Now.ToString("HH:mm:ss.fff")}Z";
                dataXml = $@"   <soapenv:Envelope xmlns:dev=""http://www.isotrol.com/STG-CIDE/2013/Devices#"" xmlns:end=""http://www.isotrol.com/STG-CIDE/2013/EndDeviceControls#"" xmlns:end1=""http://www.isotrol.com/STG-CIDE/2013/EndDeviceEvents#"" xmlns:mes=""http://iec.ch/TC57/2011/schema/message"" xmlns:met=""http://www.isotrol.com/STG-CIDE/2013/MeterReadings#"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tas=""http://www.isotrol.com/STG-CIDE/2013/TaskInfo#"">
                                    <soapenv:Header>
                                        <wsse:Security soapenv:mustUnderstand=""1"" xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"">
                                            <wsse:UsernameToken wsu:Id=""{usernameToken}"">
                                                <wsse:Username>{username}</wsse:Username>
                                                <wsse:Password Type=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText"">{password}</wsse:Password>
                                                <wsse:Nonce EncodingType=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary"">{nonce}</wsse:Nonce>
                                                <wsu:Created>{timestamp}</wsu:Created>
                                            </wsse:UsernameToken>
                                        </wsse:Security>
                                    </soapenv:Header>
                                    <soapenv:Body>
                                        <mes:Request>
                                            <mes:Header>
                                                <mes:Verb>get</mes:Verb>
                                                <mes:Noun>MeterReadings</mes:Noun>
                                                <mes:Revision>1</mes:Revision>
                                                <!-- Informacion para saber desde donde se realiza la peticion-->
                                                <mes:Source>Sitel TedisNet</mes:Source>
                                                <mes:AsyncReplyFlag>false</mes:AsyncReplyFlag>
                                                <mes:AckRequired>true</mes:AckRequired>
                                                <mes:User>
                                                    <mes:UserID>{userId}</mes:UserID>
                                                </mes:User>
                                                <mes:Property>
                                                    <mes:Name>CompressFlag</mes:Name>
                                                    <mes:Value>false</mes:Value>
                                                </mes:Property>
                                            </mes:Header>
                                            <mes:Request>{Environment.NewLine}";
                if (primeReport == IsotrolPrimeReport.S14)
                {
                    dataXml += $@"              <!-- FECHA DE INICIO Y FIN QUE QUEREMOS DE LOS DATOS -->
                                                <mes:StartTime>{startTime.Value.ToString("yyyy-MM-ddTHH:mm:ss")}</mes:StartTime>
                                                <mes:EndTime>{endTime.Value.ToString("yyyy-MM-ddTHH:mm:ss")}</mes:EndTime>{Environment.NewLine}"; // Time example format: 2019-03-01T01:00:00
                }
                string reportCode = null;
                switch (primeReport)
                {
                    case IsotrolPrimeReport.S14:
                        reportCode = "S14";
                        break;
                    case IsotrolPrimeReport.S21:
                        reportCode = "S21";
                        break;
                }
                dataXml += $@"                  <mes:Option>
                                                    <mes:name>RequestType</mes:name>
                                                    <mes:value>AUTO</mes:value>
                                                </mes:Option>
                                                <mes:Option>
                                                    <mes:name>PrimeReport</mes:name>
                                                    <mes:value>{reportCode}</mes:value>
                                                </mes:Option>
                                                <mes:MeterReadings>{Environment.NewLine}";
                foreach (string meterId in meterIds)
                {
                    dataXml += $@"                      <met:MeterReading>
                                                            <met:Meter>
                                                                <!-- SUPERVISOR AL CUAL VAMOS A REALIZAR LA PETICION -->
                                                                <dev:mRID>{meterId}</dev:mRID>
                                                            </met:Meter>
                                                        </met:MeterReading>{Environment.NewLine}";
                }
                dataXml += $@"                      </mes:MeterReadings>
                                            </mes:Request>
                                        </mes:Request>
                                    </soapenv:Body>
                                </soapenv:Envelope>";
            }
            return dataXml;
        }

        public (Exception error, string responseXML) PostSoapXmlAndGetResponseSoapXml(string url, string xml, string hostIp, ushort hostPort)
        {
            Exception error = null;
            string responseXML = null;
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(xml);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                request.ContentType = "text/xml;charset=UTF-8";
                request.Headers.Add("SOAPAction", "http://iec.ch/61968/Request");
                request.ContentLength = bytes.Length;
                request.Host = $"{hostIp}:{hostPort}";
                request.KeepAlive = true;
                request.UserAgent = "Apache-HttpClient/4.1.1 (java 1.5)";

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        string message = $"POST failed with HTTP {response.StatusCode}";
                        throw new ApplicationException(message);
                    }
                    else
                    {
                        OnLogWriteLine(false, $"POST Soap Xml state OK. " +
                            $"Content length is {response.ContentLength}. " +
                            $"Content type is {response.ContentType}", null);
                        // Get the stream associated with the response.
                        using (Stream receiveStream = response.GetResponseStream())
                        {
                            // Pipes the stream to a higher level stream reader with the required encoding format. 
                            using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                            {
                                OnLogWriteLine(false, $"Response stream received.", null);
                                responseXML = readStream.ReadToEnd();
                                OnLogWriteLine(false, $"Response XML:{Environment.NewLine}{responseXML}", null);
                                readStream.Close();
                            }
                            receiveStream.Close();
                        }
                    }
                    response.Close();
                }

                request.Abort();
            }
            catch (Exception ex)
            {
                error = ex;
            }
            return (error, responseXML);
        }
        #endregion
    }
}