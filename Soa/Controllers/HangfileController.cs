using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HangfileController : ControllerBase
    {
        public static IConfigurationRoot iConfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

        [HttpGet]
        public string ConsologRIZHI() 
        {
            var json = iConfig.GetSection("LogFile").Value;
            if (!System.IO.File.Exists(json)) 
            {
                var i = System.IO.File.Create(json);
                i.Dispose();
            }
            using (StreamWriter fileStream = System.IO.File.AppendText(json))
            {
                try
                {
                    var val = $@"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} : 执行成功" +"\r\n";
                    //StreamWriter streamWriter = new StreamWriter(fileStream);
                    var by = Encoding.UTF8.GetBytes(val);
                    //streamWriter.Write(by, 0, by.Length);
                    fileStream.Write(val);
                }
                catch (Exception ex)
                {
                    var val = $@"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} : {ex.Message}"+ "\r\n";
                    fileStream.Write(val);
                }
               
            }
            return json;
        }
    }
}
