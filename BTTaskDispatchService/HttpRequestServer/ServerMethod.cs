using BTTaskDispatchService.Model.Dto;
using BTTaskDispatchService.ServicTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTTaskDispatchService.HttpRequestServer
{
    public class ServerMethod
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="serverInfo"></param>
        public static void Execute(string methodName, ServerInfo serverInfo)
        {
            HangfireLogServer.WriteLog("--------------------------------------分割线任务开始-------------------------------------", serverInfo.FTaskName);
            var type = typeof(ServerMethod);
            var method = type.GetMethod(methodName);
            if (method != null)
            {
                method.Invoke(null, new object[] { serverInfo });
            }
            else
            {
                HangfireLogServer.WriteLog("未查找到该任务执行方法", serverInfo.FTaskName);
            }
            HangfireLogServer.WriteLog("--------------------------------------分割线任务执行结束-------------------------------------", serverInfo.FTaskName);
        }

        /// <summary>
        /// 执行api调度
        /// </summary>
        /// <param name="methodServerInfo"></param>
        public static void ExcuteApi(ServerInfo methodServerInfo)
        {
            HangfireLogServer.WriteLog("--------------------------------------分割线任务开始-------------------------------------", methodServerInfo.FTaskName);
            var result = HttpOpration.HttpRequestGet(methodServerInfo.FTaskMethod);
            HangfireLogServer.WriteLog("接口调用结果:" + result, methodServerInfo.FTaskName);
            HangfireLogServer.WriteLog("--------------------------------------分割线任务开始-------------------------------------", methodServerInfo.FTaskName);
        }

        /// <summary>
        /// 定时测试方法
        /// </summary>
        public static void TestMethod(object serverInfo)
        {
            HangfireLogServer.WriteLog("测试任务执行完成", ((ServerInfo)serverInfo).FTaskName);
        }
    }
}
