using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperSocket.messageTool
{
    public class MessageModel
    {
        public string FID { get; set; }

        public string fromID { get; set; }

        public string toID { get; set; }

        public string msg { get; set; }

        public string dateTime { get; set; }

        public int state { get; set; }
    }
}