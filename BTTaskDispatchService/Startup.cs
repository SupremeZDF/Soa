using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using Hangfire;
using Hangfire.SqlServer;
using Hangfire.AspNetCore;
using BTTaskDispatchService.ServicTool;

namespace BTTaskDispatchService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(services =>
            {
                services.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Version = "v1.1.0",
                    Description = "hangfire任务调度服务",
                    Title = "任务调度",
                    TermsOfService = null
                });
                var path = AppDomain.CurrentDomain.BaseDirectory;
                var filePath = System.IO.Path.Combine(path, "BTTaskDispatchService.xml");
                services.IncludeXmlComments(filePath);
            });
            HangfireLogServer.WriteLog("服务初始化开始");
            services.AddHangfire(configuration => configuration
                             .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                             .UseSimpleAssemblyNameTypeSerializer()
                             .UseRecommendedSerializerSettings()
                             //使用sqlserver
                             .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                             {
                                 CommandBatchMaxTimeout = TimeSpan.FromMinutes(1),
                                 SlidingInvisibilityTimeout = TimeSpan.FromMinutes(1),
                                 QueuePollInterval = TimeSpan.Zero,
                                 UseRecommendedIsolationLevel = true,
                                 DisableGlobalLocks = true
                             }));
            services.AddHangfireServer();
            HangfireLogServer.WriteLog("服务初始化结束");
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJob ,IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "任务调度服务");
            });
            //配置授权
            app.UseHangfireDashboard("/hangfire",new DashboardOptions { Authorization = new[] { new MyAuthorizationFilter()} });
            var options = new BackgroundJobServerOptions() { WorkerCount = Environment.ProcessorCount * 5 };
            app.UseHangfireServer(options);
            //注册定时服务
            backgroundJob.HangforeAddJob();
            //backgroundJob.Schedule(() => Console.WriteLine("Delayed!"), TimeSpan.FromSeconds(30));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
        }
    }
}
