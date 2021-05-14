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
    public class SendMessage : CommandBase<OneModel, StringRequestInfo>
    {
        public override void ExecuteCommand(OneModel session, StringRequestInfo requestInfo)
        {
            if (session.ID == "")
                session.Send("你未登录");
            if (requestInfo.Parameters.Length != 2) 
            {
                session.Send("参数有误");
            }
            string[] msg = requestInfo.Parameters;
            var toFid = msg[0];
            //保存消息 
            var g = 0;
            var see = session.AppServer.GetAllSessions().FirstOrDefault(x => x.ID == toFid);
            if (see != null) 
            {
                see.Send(msg[1]);
                g = 2;
            }
            //lxxx
            var message = DiirectoryMessage.keyValuePairs[toFid];
            var guid = Guid.NewGuid().ToString();
            if (message == null)
            {
                DiirectoryMessage.keyValuePairs.Add(toFid, new List<MessageModel>() { new MessageModel() {
                    FID = guid ,dateTime = DateTime.Now.ToString(),fromID = session.ID,toID = toFid ,msg ="" , state = g} });
            }
            else
            {
                message.Add(new MessageModel()
                {
                    FID = guid,
                    dateTime = DateTime.Now.ToString(),
                    fromID = session.ID,
                    toID = toFid,
                    msg = "",
                    state = g
                });
            }
        }
    }
}