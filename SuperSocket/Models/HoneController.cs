using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web.WebSockets;
using System.Threading;
using System.Net.WebSockets;
using System.Text;

namespace SuperSocket.Models
{
    public class HoneController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public void NameContro()
        {
            //var i = HttpContext.Current.IsWebSocketRequest;
            // if (i) 
            // {
            //     HttpContext.Current.AcceptWebSocketRequest(c => {
            //         var g = c.WebSocket;
            //         //var h = g.ReceiveAsync();
            //         return new System.Threading.Tasks.Task(() => { });
            //     });
            // }
            Func<AspNetWebSocketContext, Task> func = AnsyWebRequest;
            try
            {
                var j = HttpContext.Current.IsWebSocketRequest;
                if (HttpContext.Current.IsWebSocketRequest)
                {
                    var intj = Thread.CurrentThread.ManagedThreadId; //11
                  
                    HttpContext.Current.AcceptWebSocketRequest(AnsyWebRequest);
                    
                    var jj = Thread.CurrentThread.ManagedThreadId; //11
                }
            }
            catch (Exception x)
            {
                var ex = x.Message;
            }
            func(null);
        }

        public async  Task AnsyWebRequest(AspNetWebSocketContext aspNetWebSocketContext)
        {
            //return new Task(()=> { });
            var j = Thread.CurrentThread.ManagedThreadId;
            System.Net.WebSockets.WebSocket webSocket = aspNetWebSocketContext.WebSocket;
            if (webSocket.State == WebSocketState.Open) 
            {
                var message = "你已经链接成功";
                ArraySegment<byte> vs = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
                CancellationToken cancellationToken = new CancellationToken(false);
                await webSocket.SendAsync(vs, WebSocketMessageType.Text, true,cancellationToken);
                j = Thread.CurrentThread.ManagedThreadId;
                while (true) 
                {
                    ArraySegment<byte> vs1 = new ArraySegment<byte>();
                    CancellationToken cancellationToken1 = new CancellationToken();
                    j = Thread.CurrentThread.ManagedThreadId;
                    WebSocketReceiveResult recerveSocket = await webSocket.ReceiveAsync(vs1, cancellationToken1);
                    j = Thread.CurrentThread.ManagedThreadId;
                    var dd = Encoding.UTF8.GetString(vs1.ToArray());
                    message = "接收消息成功";
                    vs = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
                    j = Thread.CurrentThread.ManagedThreadId;
                    await webSocket.SendAsync(vs, WebSocketMessageType.Text, true, cancellationToken);
                    j = Thread.CurrentThread.ManagedThreadId;
                }
            }
        }
    }
}
