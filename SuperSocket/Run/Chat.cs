using SuperSocket.messageTool;
using SuperSocket.Models;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperSocket.Run
{
    public class Login : CommandBase<OneModel, StringRequestInfo>
    {
        public override void ExecuteCommand(OneModel session, StringRequestInfo requestInfo)
        {
            string[] vs = requestInfo.Parameters;
            //条件 
            if (vs.Length != 2) 
            {
                session.Send("登录失败");
            }
            var userID = vs[0];
            //单点登录
            var d = session.AppServer.GetAllSessions();
            if (d.FirstOrDefault(x => x.ID == userID) != null) 
            {
                var dd = d.FirstOrDefault(x => x.ID == userID);
                dd.Send("你已经被强迫下线");
                dd.Close();
            }
            session.ID = userID;
            session.Password = vs[1];
            session.IsLOgin = true;
            session.Send("登录成功");
            //接受离线消息
            var message = DiirectoryMessage.keyValuePairs[session.ID];
            if (message != null) 
            {
                var xx = message.Where(x => x.state == 0);
                foreach (var x in xx) 
                {

                }
            }
        }
    }
}