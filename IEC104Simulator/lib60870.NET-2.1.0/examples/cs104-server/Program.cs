using lib60870.CS101;
using lib60870.CS104;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace cs104_server
{

    class MainClass
    {
        private static JObject _data;
        private static readonly Random _rd = new Random();
        private static bool _debugOn = false;
        private static ApplicationLayerParameters _cp;

        private const int _originatorAddress = 2;
        private const int _commonAddress = 1;

        private static bool InterrogationHandler(object parameter, IMasterConnection connection, ASDU asdu, byte qoi)
        {
            if (_debugOn) Console.WriteLine("Interrogation for group " + qoi);

            _cp = connection.GetApplicationLayerParameters();

            connection.SendACT_CON(asdu, false);

            // send information objects
            // AI
            var tagsAI = GetTags("AI");
            if (tagsAI.Count > 0)
            {
                ASDU newAsdu = new ASDU(_cp, CauseOfTransmission.INTERROGATED_BY_STATION, false, false, _originatorAddress, _commonAddress, false);

                foreach (var tagAI in tagsAI)
                {
                    GetTagAI(tagAI, out int ioa, out int value);
                    newAsdu.AddInformationObject(new MeasuredValueScaled(ioa, value, new QualityDescriptor()));
                }

                connection.SendASDU(newAsdu);
            }
            // send sequence of information objects
            // DI
            var tagsDI = GetTags("DI");
            if (tagsDI.Count > 0)
            {
                ASDU newAsdu = new ASDU(_cp, CauseOfTransmission.INTERROGATED_BY_STATION, false, false, _originatorAddress, _commonAddress, true);
                foreach (var tagDI in tagsDI)
                {
                    GetTagDI(tagDI, out int ioa, out bool value);
                    newAsdu.AddInformationObject(new SinglePointInformation(ioa, value, new QualityDescriptor()));
                }
                connection.SendASDU(newAsdu);
            }
            // CDI
            var tagsCDI = GetTags("CDI");
            if (tagsCDI.Count > 0)
            {
                ASDU newAsdu = new ASDU(_cp, CauseOfTransmission.INTERROGATED_BY_STATION, false, false, _originatorAddress, _commonAddress, true);
                foreach (var tagCDI in tagsCDI)
                {
                    GetTagCDI(tagCDI, out int ioa, out DoublePointValue value);
                    newAsdu.AddInformationObject(new DoublePointInformation(ioa, value, new QualityDescriptor()));
                }
                connection.SendASDU(newAsdu);
            }
            // END
            connection.SendACT_TERM(asdu);

            return true;
        }

        private static bool AsduHandler(object parameter, IMasterConnection connection, ASDU asdu)
        {

            if (asdu.TypeId == TypeID.C_SC_NA_1)
            {
                if (_debugOn) Console.WriteLine("Single command");
                SingleCommand sc = (SingleCommand)asdu.GetElement(0);
                if (_debugOn) Console.WriteLine($"DO.{sc.ObjectAddress} select:{sc.Select} state:{sc.State}");
            }
            else if (asdu.TypeId == TypeID.C_DC_NA_1)
            {
                if (_debugOn) Console.WriteLine("Double command");
                DoubleCommand sc = (DoubleCommand)asdu.GetElement(0);
                if (_debugOn) Console.WriteLine($"CDO.{sc.ObjectAddress} select:{sc.Select} state:{sc.State}");
                if (!sc.Select) // Case execution
                {
                    Task.Run(() => CopyStatesCDI(sc, connection));
                }
            }
            else if (asdu.TypeId == TypeID.C_CS_NA_1)
            {
                ClockSynchronizationCommand qsc = (ClockSynchronizationCommand)asdu.GetElement(0);
                if (_debugOn) Console.WriteLine("Received clock sync command with time " + qsc.NewTime.ToString());
            }

            return true;
        }

        private static void CopyStatesCDI(DoubleCommand sc, IMasterConnection connection)
        {
            int numCopies = 0;
            IEnumerable<JToken> tokens;
            List<JToken> changedTags = new List<JToken>();
            do
            {
                numCopies++;
                string copystateProperty = "copystate";
                if (numCopies > 1)
                {
                    copystateProperty += numCopies.ToString();
                }
                tokens = _data.SelectTokens($"$.CDO.[?(@.IOA == {sc.ObjectAddress})].{copystateProperty}");
                if (tokens != null)
                {
                    foreach (var token in tokens)
                    {
                        // token like "CDI.15001" for change .state
                        string[] parts = token.ToString().Split('.');
                        string typeIoa = parts[0];
                        string ioa = parts[1];
                        string selector = $"$.{typeIoa}.[?(@.IOA == {ioa})]"; // Ex.: "$.CDI.[?(@.IOA == 15001)]"
                        var tokenCopy = _data.SelectToken(selector);
                        tokenCopy["state"] = sc.State;
                        changedTags.Add(tokenCopy);
                    }
                }
            } while (tokens != null && tokens.GetEnumerator().MoveNext());
            ChangeLaunch(changedTags, connection);
        }

        public static void ChangeLaunch(List<JToken> changedTags, IMasterConnection connection)
        {
            if (changedTags != null && changedTags.Count > 0)
            {
                // Start
                ApplicationLayerParameters cp = connection.GetApplicationLayerParameters();
                // ASDU asdu = new ASDU(cp, CauseOfTransmission.SPONTANEOUS, false, false, 0, 1, false);
                ASDU newAsdu = new ASDU(cp, CauseOfTransmission.SPONTANEOUS, false, false, _originatorAddress, _commonAddress, false);
                foreach (var tag in changedTags)
                {
                    GetTagCDI(tag, out int ioa, out DoublePointValue value);
                    newAsdu.AddInformationObject(new DoublePointInformation(ioa, value, new QualityDescriptor()));
                }
                connection.SendASDU(newAsdu);
            }
        }

        public static void Main(string[] args)
        {
            bool running = true;

            Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e)
            {
                e.Cancel = true;
                running = false;
            };

            var jsonStr = File.ReadAllText(@"RTU\rtu.1.json");
            _data = (JObject)JsonConvert.DeserializeObject(jsonStr);

            Server server = new Server();
            server.DebugOutput = _debugOn = _data["Debug"] is null ? true : (bool)_data["Debug"];
            server.MaxQueueSize = 10;
            server.SetInterrogationHandler(InterrogationHandler, null);
            server.SetASDUHandler(AsduHandler, null);
            server.Start();
            Console.WriteLine("Start server");

            ASDU newAsdu = new ASDU(server.GetApplicationLayerParameters(), CauseOfTransmission.INITIALIZED, false, false, _originatorAddress, _commonAddress, false);
            EndOfInitialization eoi = new EndOfInitialization(0);
            newAsdu.AddInformationObject(eoi);
            server.EnqueueASDU(newAsdu);

            int waitTime = 1000;
            var tagsAI = GetTags("AI");
            while (running)
            {
                Thread.Sleep(100);

                if (waitTime > 0)
                    waitTime -= 100;
                else
                {

                    newAsdu = new ASDU(server.GetApplicationLayerParameters(), CauseOfTransmission.PERIODIC, false, false, _originatorAddress, _commonAddress, false);

                    foreach (var tagAI in tagsAI)
                    {
                        GetTagAI(tagAI, out int ioa, out int value);
                        newAsdu.AddInformationObject(new MeasuredValueScaled(ioa, value, new QualityDescriptor()));
                    }

                    server.EnqueueASDU(newAsdu);

                    waitTime = 1000;
                }
            }

            Console.WriteLine("Stop server");
            server.Stop();
        }

        private static JToken GetTag(string tagsType, int ioa)
        {
            var tag = _data.SelectToken($"$.{tagsType}.[?(@.IOA == {ioa})]");
            return tag;
        }

        private static void GetTagCDI(JToken tag, out int ioa, out DoublePointValue value)
        {
            ioa = (int)tag["IOA"];
            value = tag["state"] is null ? DoublePointValue.INDETERMINATE : (DoublePointValue)(int)tag["state"];
        }

        public static void GetTagDI(JToken tag, out int ioa, out bool value)
        {
            ioa = (int)tag["IOA"];
            value = tag["state"] is null ? false : (int)tag["state"] == 1;
        }

        public static void GetTagAI(JToken tag, out int ioa, out int value)
        {
            ioa = (int)tag["IOA"];
            int min = tag["min"] is null ? 0 : (int)tag["min"];
            int max = tag["max"] is null ? 1 : (int)tag["max"];
            value = _rd.Next(min, max);
        }

        /// <summary>
        /// tagType: AI, DI, CDI, CDO, DO, AO
        /// </summary>
        public static List<JToken> GetTags(string tagType)
        {
            List<JToken> tags = new List<JToken>();
            var tagsAI = _data[tagType];
            if (tagsAI.HasValues)
            {
                foreach (var tagAI in tagsAI)
                {
                    if (tagAI["IOA"] is null) continue;
                    tags.Add(tagAI);
                }
            }
            return tags;
        }
    }
}
