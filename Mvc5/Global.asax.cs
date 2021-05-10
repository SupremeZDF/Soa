using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Mvc5
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();  //注册区域
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);  //注册全局Filters
            RouteConfig.RegisterRoutes(RouteTable.Routes); //注册路由
            BundleConfig.RegisterBundles(BundleTable.Bundles); //注册管道
        }
    }
}
