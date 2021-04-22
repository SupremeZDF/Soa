using BTTaskDispatchService.AutoMapper;
using BTTaskDispatchService.HttpRequestServer;
using BTTaskDispatchService.Model;
using BTTaskDispatchService.Model.Dto;
using Hangfire;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace BTTaskDispatchService.ServicTool
{
    public static class HangFilejobService
    {

        public static List<ServerInfoTable> infoTables;

        public static List<ServerInfoTable> GetHangforeTab()
        {
            List<ServerInfoTable> serverInfoTables = new List<ServerInfoTable>();
            using (SqlSugarClient sqlSugarClient = SqlSugarService.GetInstance())
            {
                //查询数据库中所有数据
                var dt = sqlSugarClient.Ado.GetDataTable("select * from ServiceInfo", new List<SugarParameter>() { });
                if (dt != null && dt.Rows.Count > 0)
                {
                    serverInfoTables = (from i in dt.AsEnumerable()
                                        select new ServerInfoTable()
                                        {
                                            FID = i.Field<int>("FID"),
                                            FTaskName = i.Field<string>("FTaskName"),
                                            FIsUsable = i.Field<int>("FIsUsable") == 1 ? true : false,
                                            FTaskType = i.Field<int>("FTaskType"),
                                            FInterval = i.Field<int>("FInterval"),
                                            FExecTime = i.Field<string>("FExecTime"),
                                            FExecWhenStart = i.Field<int>("FExecWhenStart") == 1 ? true : false,
                                            FTaskUrlIP = i.Field<string>("FTaskUrlIP"),
                                            FTaskUrlRoute = i.Field<string>("FTaskUrlRoute"),
                                            FTaskSchedulingype = i.Field<int>("FTaskSchedulingype"),
                                            FAddPerson = i.Field<string>("FAddPerson"),
                                            FAddTime = i.Field<DateTime>("FAddTime"),
                                            FTaskKey = i.Field<string>("FTaskKey"),
                                            FCustomParam = i.Field<string>("FCustomParam"),
                                            FRemark = i.Field<string>("FRemark")
                                        }).ToList();
                }
            }
            return serverInfoTables;
        }

        /// <summary>
        /// 添加任务到调度器
        /// </summary>
        /// <param name="backgroundJobClient"></param>
        public static void HangforeAddJob(this IBackgroundJobClient backgroundJobClient)
        {
            HangforeAddJob();
        }
        public static Result HangforeAddJob()
        {
            Result result = new Result() { code = 1 };
            try
            {
                HangfireLogServer.WriteLog("开始读取数据库数据");
                List<ServerInfoTable> serverInfoTables = GetHangforeTab();
                infoTables = serverInfoTables;
                HangfireLogServer.WriteLog("读取数据库数据完成");
                HangfireLogServer.WriteLog("开始添加任务");
                if (serverInfoTables == null || serverInfoTables.Count <= 0)
                {
                    //添加测试任务
                    serverInfoTables.Add(new ServerInfoTable()
                    {
                        FTaskName = "任务测试",
                        FExecWhenStart = false,
                        FTaskKey = "TestMethod",
                        FIsUsable = true,
                        FTaskType = (int)TaskTypeEnum.FixpointTask,
                        FExecTime = "*/1 * * * *",
                        FTaskUrlIP = BTServiceFactory.GetSectionValyue("FTaskUrlIP"),
                        FTaskUrlRoute = BTServiceFactory.GetSectionValyue("FTaskUrlRoute"),
                        FTaskSchedulingype = 2,
                        FRemark = "测试任务",
                        FAddPerson = "zr",
                        FAddTime = DateTime.Now
                    });
                }
                foreach (var i in serverInfoTables)
                {
                    ServerInfo serverInfo = AutoMapperTool<ServerInfoTable, ServerInfo>.ToClass(i);
                    if (!serverInfo.FIsUsable)
                        continue;
                    serverInfo.FTaskMethod = i.FTaskUrlIP + i.FTaskUrlRoute;
                    HangforeAddJob(serverInfo);
                }
                HangfireLogServer.WriteLog("任务添加完成");
            }
            catch (Exception ex)
            {
                result.code = 0;
                result.Msg = ex.Message;
                HangfireLogServer.WriteLog($"数据库读取数据，添加任务异常:{ex.Message}");
            }
            return result;
        }


        /// <summary>
        /// 添加任务
        /// </summary>
        public static void HangforeAddJob(ServerInfo serverInfo)
        {
            try
            {
                RecurringJob.RemoveIfExists(serverInfo.FTaskName);
                var type = typeof(ServerMethod);
                MethodInfo method = type.GetMethod(serverInfo.FTaskMethod);
                if (method != null)
                {
                    HangFireAddService(serverInfo, () => ServerMethod.Execute(serverInfo.FTaskMethod, serverInfo));
                    #region 旧
                    //RecurringJob.AddOrUpdate(serverInfo.FTaskName,
                    //    () => ServerMethod.Execute(serverInfo.FTaskMethod, serverInfo),
                    //    serverInfo.FExecTime, TimeZoneInfo.Local);
                    #endregion
                }
                else if (serverInfo.FTaskMethod.ToLower().StartsWith("http://") || serverInfo.FTaskMethod.ToLower().StartsWith("https://"))
                {
                    HangFireAddService(serverInfo, () => ServerMethod.ExcuteApi(serverInfo));
                    #region 旧
                    //RecurringJob.AddOrUpdate(serverInfo.FTaskName,
                    //           () => ServerMethod.ExcuteApi(serverInfo),
                    //           serverInfo.FExecTime, TimeZoneInfo.Local);
                    #endregion
                }
                else
                {
                    HangfireLogServer.WriteLog($"加入任务队列失败，未查找到任务执行方法{serverInfo.FTaskName}");
                    return;
                }
                HangfireLogServer.WriteLog($"加入任务队列成功:{serverInfo.FTaskName}");
            }
            catch (Exception ex)
            {
                HangfireLogServer.WriteLog($"加入任务队列失败_{serverInfo.FTaskName}:{ex.Message}");
            }
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        public static void HangFireAddService(ServerInfo serverInfo, Expression<Action> methodCall)
        {
            if (serverInfo.FTaskSchedulingype == 1)
            {
                //后台方法
                BackgroundJob.Enqueue(methodCall);
            }
            else if (serverInfo.FTaskSchedulingype == 3)
            {
                //延时方法
                BackgroundJob.Schedule(methodCall, TimeSpan.FromMilliseconds(Convert.ToInt32(serverInfo.FExecTime)));
            }
            else if (serverInfo.FTaskSchedulingype == 2)
            {
                //周期方法
                RecurringJob.AddOrUpdate(serverInfo.FTaskName, methodCall, serverInfo.FExecTime, TimeZoneInfo.Local);
            }
        }

        /// <summary>
        /// 删除所有任务
        /// </summary>
        public static Result HangforeRemoveJob(List<ServerInfoTable> serverInfoTable)
        {
            Result result = new Result() { code = 1 };
            try
            {
                List<ServerInfoTable> serverInfoTables = serverInfoTable;
                HangfireLogServer.WriteLog("开始删除任务队列");
                foreach (var i in serverInfoTables)
                {
                    ServerInfo serverInfo = AutoMapperTool<ServerInfoTable, ServerInfo>.ToClass(i);
                    HangforeRemoveJob(serverInfo);
                }
                HangfireLogServer.WriteLog("完成删除任务队列");
            }
            catch (Exception ex)
            {
                HangfireLogServer.WriteLog($"删除任务队列失败，删除任务异常:{ex.Message}");
                result.code = 0;
                result.Msg = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 移出作业
        /// </summary>
        /// <param name="serverInfo"></param>
        public static void HangforeRemoveJob(ServerInfo serverInfo)
        {
            if (serverInfo == null) return;
            RecurringJob.RemoveIfExists(serverInfo.FTaskName);
            HangfireLogServer.WriteLog($"删除任务，{serverInfo.FTaskName}_任务已停止执行。");
        }
    }
}
