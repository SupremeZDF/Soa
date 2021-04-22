using Microsoft.Extensions.Configuration;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTTaskDispatchService.ServicTool
{
    public class SqlSugarService
    {
        
        public static SqlSugarClient GetInstance() 
        {
            //创建数据库对象
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = BTServiceFactory.GetConfiguration().GetConnectionString("HangfireConnection"),//连接符字串
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            });

            //添加Sql打印事件，开发中可以删掉这个代码
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql);
            };
            return db;
        }
    }
}
