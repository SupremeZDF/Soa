using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperSocket.Models
{
    public class OneModel : AppSession<OneModel>
    {
        public string ID { get; set; }

        public string Password { get; set; }

        public bool IsLOgin { get; set; }

        protected override void OnSessionStarted()
        {
            base.OnSessionStarted();
            this.Send("Welcome to SuperSocket Telnet Server");
        }

        public override void Send(string message)
        {
            base.Send(message + "\r\n");
        }

        protected override void HandleUnknownRequest(StringRequestInfo requestInfo)
        {
            this.Send("Unknow request");
        }

        protected override void HandleException(Exception e)
        {
            this.Send("Application error: {0}", e.Message);
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            //add you logics which will be executed after the session is closed
            base.OnSessionClosed(reason);
        }

        //protected override void OnSessionStarted() 
        //{
        //    base.OnSessionStarted();
        //}
    }
}