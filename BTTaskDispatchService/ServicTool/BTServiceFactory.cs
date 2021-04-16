using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BTTaskDispatchService.ServicTool
{
    public class BTServiceFactory
    {
        public static IConfiguration _configuration;

        public static void CreateConfiguration() 
        {
            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }

        public static IConfiguration GetConfiguration() 
        {
            if (_configuration == null) 
            {
                CreateConfiguration();
            }
            return _configuration;
        }

        public static string GetSectionValyue(string Str) 
        {
            if (_configuration == null)
            {
                CreateConfiguration();
            }
            return _configuration.GetSection("ConnectionString").Value;
        }
    }
}
