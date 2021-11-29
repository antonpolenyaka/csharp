using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPAddress serverAddr = IPAddress.Parse("192.168.8.246");

            IPEndPoint endPoint = new IPEndPoint(serverAddr, 6653);

            string text = "Hello";

            //byte[] send_buffer = Encoding.ASCII.GetBytes(text);

            byte[] send_buffer = Encoding.ASCII.GetBytes("442332303223342331233023302a4d23352f");

            sock.SendTo(send_buffer, endPoint);

            send_buffer = Encoding.ASCII.GetBytes("442332303223342331233023302a4d2331352f");

            sock.SendTo(send_buffer, endPoint);

            //Src: 49502, Dst: 6653
            //1 (LEN18): 442332303223342331233023302a4d23352f
            //2 (LEN19): 442332303223342331233023302a4d2331352f

            Console.WriteLine("Hello World!");
        }
    }
}
