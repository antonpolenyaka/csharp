using Sally7;
using Sally7.Infrastructure;
using Sally7.Internal;
using Sally7.Protocol;
using Sally7.Protocol.Cotp;
using Sally7.Protocol.IsoOverTcp;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MyTestIccpSitel
{
    class Program
    {
        public const int _udpPort = 102;

        static void Main(string[] args)
        {
            try
            {
                MyTcpListener listener = new MyTcpListener();
                listener.UdpReceived += Listener_UdpReceived;
                listener.ListenICCPBroadcast();


                //Connect("192.168.23.87", "TESjl hjsñldjfk sñdlgjlñsdjfg lñsdjfglñsdjfglñkdjsgslkT", 102);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Listener_UdpReceived(object sender, EventArgs e)
        {
            // Received UDP from server
            // Connect to server
            (TcpClient client, NetworkStream stream) = Connect("192.168.23.87", _udpPort);

            ConnectCotpAsync(stream);

            CloseConnection(client, stream);
        }

        /// <summary>
        /// Send TPKT + ISO
        /// </summary>
        private static async void ConnectCotpAsync(NetworkStream stream)
        {
            string host = "192.168.23.87";
            Tsap sourceTsap = new Tsap(0x00, 0x01);
            Tsap destinationTsap = new Tsap(0x00, 0x01);
            S7Connection s7Connection = new S7Connection(host,sourceTsap,destinationTsap);
            bool isCCConnectConfirm = await s7Connection.OpenAsync();
            Console.WriteLine($"Conectados al CC por el ICCP: {isCCConnectConfirm}");
        }

        private static async Task<int> ReadTpktAsync(NetworkStream stream, byte[] buffer)
        {
            var len = await stream.ReadAsync(buffer, 0, 4).ConfigureAwait(false);
            if (len < 4)
                throw new Exception($"Error while reading TPKT header, expected 4 bytes but received {len}.");

            var tpkt = buffer.AsSpan().Struct<Tpkt>(0);
            tpkt.Assert();
            var msgLen = tpkt.MessageLength();
            len = await stream.ReadAsync(buffer, 4, msgLen).ConfigureAwait(false);
            if (len != msgLen)
            {
                throw new Exception($"Error while reading TPKT data, expected {msgLen} bytes but received {len}.");
            }

            return tpkt.Length;
        }

        private static void CloseConnection(TcpClient client, NetworkStream stream)
        {
            try
            {
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        static (TcpClient, NetworkStream) Connect(string server, int port)
        {
            NetworkStream stream;
            TcpClient client;
            try
            {
                client = new TcpClient(server, port);
                stream = client.GetStream();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
                stream = null;
                client = null;
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                stream = null;
                client = null;
            }
            return (client, stream);
        }


        static void Connect2(string server, string message, int port)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                //int port = 13000;
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                //client.Client.Send(data);

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer.
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new byte[256];

                // String to store the response ASCII representation.
                string responseData = string.Empty;

                // Read the first batch of the TcpServer response bytes.
                int bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}
