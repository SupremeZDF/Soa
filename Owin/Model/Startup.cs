using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Text;
using Hangfire;

[assembly: OwinStartup(typeof(MyWebApplication.Startup))]
namespace Owins.Model
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Map Dashboard to the `http://<your-app>/hangfire` URL.
            //app.UseHangfireDashboard("",0);
        }
    }
}
