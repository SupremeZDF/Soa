using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;

namespace Mvc5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        
        {
            Session["aa"] = 134;
            var uus = Session["aa"] == null ? "" : Session["aa"].ToString();
            //Task.Run(()=> {
            //    uus = Session["aa"] == null ? "" : Session["aa"].ToString();
            //});
           // Names();
            for (var i =0;i<10 ;i++ )
            {
                Thread.Sleep(100);
            }
            //TaskFactory
            return View();
        }

        public async Task Names() 
        {
            var uus = Session["aa"] == null ? "" : Session["aa"].ToString();
            await Task.Run(() => {
                for (var i = 0; i < 10; i++)
                {
                    Thread.Sleep(100);
                }
            });
            uus = Session["aa"] == null ? "" : Session["aa"].ToString();
            await Task.Run(() => {
                for (var i = 0; i < 10; i++)
                {
                    Thread.Sleep(100);
                }
            });
        }

        public static List<WebSocket> webSockets = new List<WebSocket>();
        public static List<string> Vs = new List<string>();

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            //ViewData["Message"] = "ewewqdsdasd";
            //ViewData.Add("","");
            var a = View();
            
            return a;
        }

        public void Name() 
        {
            var uus = Session["aa"] == null ? "" : Session["aa"].ToString();
            Func<AspNetWebSocketContext, Task> func = AnsyWebRequest;
            try
            {
                var j = HttpContext.IsWebSocketRequest;
                if (HttpContext.IsWebSocketRequest)
                {
                    var intj = Thread.CurrentThread.ManagedThreadId; //7
                    uus = Session["aa"] == null ? "" : Session["aa"].ToString();
                    HttpContext.AcceptWebSocketRequest(AnsyWebRequest);
                    uus = Session["aa"] == null ? "" : Session["aa"].ToString();
                    var jj = Thread.CurrentThread.ManagedThreadId; //7
                }
            }
            catch (Exception x)
            {
                var ex = x.Message;
            }
            //func(null);
        }

        public async Task AnsyWebRequest(AspNetWebSocketContext aspNetWebSocketContext)
        {
            //var gg = Session["aa"] == null ? "" : Session["aa"].ToString();
            //return new Task(()=> { });
            var j = Thread.CurrentThread.ManagedThreadId; //7
            System.Net.WebSockets.WebSocket webSocket = aspNetWebSocketContext.WebSocket;
            if (webSocket.State == WebSocketState.Open)
            {
                webSockets.Add(webSocket);
                var message = "你已经链接成功";
                ArraySegment<byte> vs = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
                CancellationToken cancellationToken = new CancellationToken(false);
                await webSocket.SendAsync(vs, WebSocketMessageType.Text, true, cancellationToken);
                j = Thread.CurrentThread.ManagedThreadId; //8
                while (true)
                {
                    Vs.Clear();
                    foreach (var i in webSockets) 
                    {
                        Vs.Add(i.GetHashCode().ToString());
                    }

                    //var uus = HttpContext.Session["aa"] == null;
                    var machineCodeKey = Guid.NewGuid().ToString();  //fe37f8a8-3ed5-4d4d-8224-46beff530cae   96d8716a-2304-4ce3-abbe-677b2280d983
                    ArraySegment<byte> vs1 = new ArraySegment<byte>(new byte[1024]);
                    CancellationToken cancellationToken1 = new CancellationToken(false);
                    //cancellation = new CancellationToken(false);
                    j = Thread.CurrentThread.ManagedThreadId;  //8
                    WebSocketReceiveResult recerveSocket = await webSocket.ReceiveAsync(vs1, cancellationToken1);
                    j = Thread.CurrentThread.ManagedThreadId; //8
                    if (recerveSocket.MessageType == WebSocketMessageType.Close) 
                    {
                        //var uu = Session["aa"] == null ? "" : Session["aa"].ToString();
                        if (webSocket.State != WebSocketState.Open) 
                        { 
                            var hhh = machineCodeKey;  //fe37f8a8-3ed5-4d4d-8224-46beff530cae   96d8716a-2304-4ce3-abbe-677b2280d983
                        }
                    }
                    j = Thread.CurrentThread.ManagedThreadId; //8
                    var dd = Encoding.UTF8.GetString(vs1.ToArray());
                    message = "接收消息成功";
                    vs = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
                    j = Thread.CurrentThread.ManagedThreadId; //8
                    await webSocket.SendAsync(vs, WebSocketMessageType.Text, true, cancellationToken);
                    j = Thread.CurrentThread.ManagedThreadId; //8
                }
            }
        }

        public JsonResult JsonResult() => new JsonResult();

        public FileResult fileResult() => null;

    }
}