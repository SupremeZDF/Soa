using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsService
{
    public partial class MyService1 : ServiceBase
    {
        public MyService1()
        {
            InitializeComponent();
        }

        private string filePath = @"D:\公司项目\巴厘车服项目\日志\MyServiceLog.txt";

        protected override void OnStart(string[] args)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate)) 
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine($"{DateTime.Now},服务启动！");
                }
            }
           
        }

        protected override void OnStop()
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Append)) 
            {
                using (StreamWriter writer = new StreamWriter(stream)) 
                {
                    writer.WriteLine($"{DateTime.Now},服务停止！");
                }
            }
        }
    }
}
