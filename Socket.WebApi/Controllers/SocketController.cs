using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Socket.WebApi.SocketModel;

namespace Socket.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SocketController : ControllerBase
    {
        /// <summary>
        /// 任务测试
        /// </summary>
        [HttpGet]
        public void MethodTest() 
        {
            SockeProccess.Process();
        }
    }
}
