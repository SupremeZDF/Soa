using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SuperSocket.Models
{
    public class ChatServer : AppServer<OneModel>
    {
        //public ChatServer(): base(new CommandLineReceiveFilterFactory(encoding:Encoding.UTF8)) { }

        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            return base.Setup(rootConfig, config);
        }

        protected override void OnStartup()
        {
            base.OnStartup();
        }

        protected override void OnStopped()
        {
            base.OnStopped();
        }

        protected override void OnNewSessionConnected(OneModel session)
        {
            base.OnNewSessionConnected(session);
        }
    }
}