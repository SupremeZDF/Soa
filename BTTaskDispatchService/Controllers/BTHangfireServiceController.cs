using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BTTaskDispatchService.AutoMapper;
using BTTaskDispatchService.Model;
using BTTaskDispatchService.Model.Dto;
using BTTaskDispatchService.ServicTool;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTTaskDispatchService.Controllers
{
    public class BTHangfireServiceController : BTApiControllerBase
    {
        /// <summary>
        /// 重启服务
        /// </summary>
        [HttpGet]
        public Result HangfireService()
        {
            //删除上次注册得所有服务
            var remove = HangFilejobService.HangforeRemoveJob(HangFilejobService.infoTables);
            if (remove.code == 0)
                return remove;
            //重新从数据库注册服务
            return HangFilejobService.HangforeAddJob();
        }

        /// <summary>
        /// 添加服务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result HangFireAddService([FromBody]ServerInfo serverInfo)
        {
            Result result = new Result() { code = 1 };
            try
            {
                if (serverInfo == null)
                {
                    result.code = 0;
                    return result;
                }
                HangFilejobService.HangforeAddJob(serverInfo);
                var data = AutoMapperTool<ServerInfo, ServerInfoTable>.ToClass(serverInfo);
                HangFilejobService.infoTables.Add(data);
            }
            catch (Exception ex)
            {
                result.code = 1;
                result.Msg = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 删除服务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Result HangFireDeleteService([FromBody] ServerInfo serverInfo)
        {
            Result result = new Result() { code = 1 };
            try
            {
                if (serverInfo == null)
                {
                    result.code = 0;
                    return result;
                }
                HangFilejobService.HangforeRemoveJob(serverInfo);
            }
            catch (Exception ex)
            {
                result.code = 1;
                result.Msg = ex.Message;
            }
            return result;
        }
    }
}
