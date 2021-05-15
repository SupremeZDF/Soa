using SuperSocket.Models;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperSocket.Run
{
    public class Chat : CommandBase<OneModel, StringRequestInfo>
    {
        public override void ExecuteCommand(OneModel session, StringRequestInfo requestInfo)
        {
            //throw new NotImplementedException();
            session.Send("你来了");
        }

    }
}