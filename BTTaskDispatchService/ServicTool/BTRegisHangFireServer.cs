using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTTaskDispatchService.ServicTool
{
    public class BTRegisHangFireServer
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        public static void RegisterFireServer() 
        {
            var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget!"));

            var a = BackgroundJob.Schedule(() => Console.WriteLine("Delayed!"), TimeSpan.FromDays(7));

            RecurringJob.AddOrUpdate(() => Console.WriteLine("Recurring!"), Cron.Daily);

            BackgroundJob.ContinueWith(jobId, () => Console.WriteLine("Continuation!"));

            //var batchId = BatchJob.StartNew(x =>
            //{
            //    x.Enqueue(() => Console.WriteLine("Job 1"));
            //    x.Enqueue(() => Console.WriteLine("Job 2"));
            //})

            {
                //RecurringJob.AddOrUpdate(() => remindService.PushNoSubmittedDailyReportMsg(), Cron.Daily(8), TimeZoneInfo.Local, bgOption.Queues[0]); // 每天早8点
                //RecurringJob.AddOrUpdate(() => remindService.PushNoSubmittedWeeklyReportMsg(), Cron.Weekly(DayOfWeek.Monday, 8), TimeZoneInfo.Local, bgOption.Queues[0]); // 每周周一早八点
                //RecurringJob.AddOrUpdate(() => remindService.PushNoSubmittedMonthlyReportMsg(), Cron.Monthly(1, 8), TimeZoneInfo.Local, bgOption.Queues[0]); // 每月1日早8点
            }
        }
    }
}
