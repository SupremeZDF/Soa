using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Socket.WebApi.SocketModel
{
    public class SockeProccess
    {
        public static void Process() 
        {
            int port = 2018;
            string host = "127.0.0.1";

            IPAddress iP = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(iP, port);
            //socket地址 服务端
            System.Net.Sockets.Socket socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipe);
            socket.Listen(0);
            Console.WriteLine("监听已经打开,请等待");
            //收到消息 接受一个socket 链接
            System.Net.Sockets.Socket socket1 = socket.Accept();
            Console.WriteLine("链接已经确立....");
            //收到消息 接受一个Socket链接
            while (true) 
            {
                string recStr = "";
                byte[] recByte = new byte[4096];
                int bytes = socket1.Receive(recByte, recByte.Length, 0);
                recStr += Encoding.ASCII.GetString(recByte, 0, bytes);
                Console.WriteLine($"服务器端获得信息：{recStr}");
                if (recStr.Equals("stop")) 
                {
                    socket1.Close();// 关闭链接
                    Console.WriteLine("关闭链接....");
                    break;
                }

                //返回信息
                Console.WriteLine("请输入回发消息。。。");
                string sendStr = Console.ReadLine();
                byte[] sendByte = Encoding.ASCII.GetBytes(sendStr);
                socket1.Send(sendByte, sendByte.Length, 0);
            }
            socket.Close();
        }
    }
}
