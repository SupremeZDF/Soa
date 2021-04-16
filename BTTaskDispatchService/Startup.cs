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
            services.AddHangfire(configuration => configuration
                             .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                             .UseSimpleAssemblyNameTypeSerializer()
                             .UseRecommendedSerializerSettings()
                             .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                             {
                                 CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                                 SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                                 QueuePollInterval = TimeSpan.Zero,
                                 UseRecommendedIsolationLevel = true,
                                 DisableGlobalLocks = true
                             }));
            services.AddHangfireServer();
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
            }); app.UseSwagger();
            app.UseHangfireDashboard();
            backgroundJob.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
        }
    }
}
