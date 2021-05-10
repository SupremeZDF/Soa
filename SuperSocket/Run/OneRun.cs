using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperSocket.Run
{
    public class OneRun
    {
        public static void SuperSocketConfigStart() 
        {
            //配置文件启动 supersocket
            IBootstrap bootstrap = BootstrapFactory.CreateBootstrap();

            if (!bootstrap.Initialize()) 
            {
                Console.WriteLine("初始化失败");
                Console.ReadKey();
                return;
            }
            var result 
        }
    }
}