using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SocKet.Client.Model
{
    public class SocketOneModel
    {
        /// <summary>
        /// socket
        /// </summary>
        public static void ProCessRocket() 
        {
            //Thread.Sleep(10000);
            
            Console.WriteLine("启动一个Socket客户端链接");

            int port = 2020;
            string host = "192.168.131.162"; //服务端ip地址
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint iPEndPoint = new IPEndPoint(ip, port);

            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(iPEndPoint);
            while (true) 
            {
                Console.WriteLine("请输入发送到服务器得信息:");
                string sendStr = Console.ReadLine();
                if (sendStr == "exit")
                    break;
                byte[] sendBytes = Encoding.ASCII.GetBytes(sendStr);
                clientSocket.Send(sendBytes);
                string recStr = "";
                byte[] recBytes = new byte[4096];
                int bytes = clientSocket.Receive(recBytes, recBytes.Length, 0);
                string str = Encoding.ASCII.GetString(recBytes, 0, bytes);
                Console.WriteLine($"服务器返回 ：{str}");
            }
        }

        public async static void Name()
        {
            Thread.Sleep(1000);
            Console.WriteLine("1");
            Thread.Sleep(1000);
            Console.WriteLine("1");
            Console.WriteLine($"线程ID:{Thread.CurrentThread.ManagedThreadId}");
            Names();
            Console.WriteLine($"线程ID:{Thread.CurrentThread.ManagedThreadId}");
        }

        public async static void Names()
        {
            Thread.Sleep(1000);
            Console.WriteLine("1");
            await Task.Run(() => { Console.WriteLine("哈哈"); });
            Console.WriteLine($"线程ID:{Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(3000);
            Console.WriteLine($"线程ID:{Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("1");
        }
    }
}
