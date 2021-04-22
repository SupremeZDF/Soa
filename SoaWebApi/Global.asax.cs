using Microsoft.Owin.Hosting;
using SoaWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SoaWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            string baseAddress = "http://localhost:9000/";
            //string baseAddress = "http://+:9000/"; //绑定所有地址，外网可以用ip访问 需管理员权限
            // 启动 OWIN host 
            WebApp.Start<Startup>(url: baseAddress);
            //Console.WriteLine("程序已启动,按任意键退出");
            //Console.ReadLine();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
