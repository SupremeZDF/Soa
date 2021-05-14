using SuperSocket.messageTool;
using SuperSocket.Run;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SuperSocket
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            OneRun.SuperSocketConfigStart();

            //List<MessageModel> messages =
            //    new List<MessageModel>() 
            //    {
            //        new MessageModel { FID="1"}, 
            //        new MessageModel { FID = "2" },
            //        new MessageModel { FID = "3" } 
            //    };
            //Dictionary<string, List<MessageModel>> keyValuePairs = new Dictionary<string, List<MessageModel>>();
            //keyValuePairs.Add("1",messages);
            //var data = keyValuePairs["1"];
            //var value = data.FirstOrDefault(x => x.FID == "1");
            //value.msg = "¹þ¹þ";
            //data.Remove(value);
        }
    }
}
