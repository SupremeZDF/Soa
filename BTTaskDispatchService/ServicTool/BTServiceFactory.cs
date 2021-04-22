using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using SqlSugar;

namespace BTTaskDispatchService.ServicTool
{
    public class BTServiceFactory
    {
        public static IConfiguration _configuration;

        static BTServiceFactory()
        {
            CreteInstance();
        }

        /// <summary>
        /// 创建实列
        /// </summary>
        public static void CreteInstance()
        {
            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }

        public static IConfiguration GetConfiguration()
        {
            if (_configuration == null)
            {
                CreteInstance();
            }
            return _configuration;
        }

        public static string GetSectionValyue(string Str)
        {
            if (_configuration == null)
            {
                CreteInstance();
            }
            return _configuration.GetSection(Str).Value;
        }
    }
}
