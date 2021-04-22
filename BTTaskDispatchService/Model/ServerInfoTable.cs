using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTTaskDispatchService.Model
{
    [SugarTable("ServiceInfo")]
    public class ServerInfoTable
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int FID { get; set; }

        public string FTaskName { get; set; }

        public bool FIsUsable { get; set; }

        public int FTaskType { get; set; }

        public int FInterval { get; set; }

        public string FExecTime { get; set; }

        /// <summary>
        /// 是否启动执行
        /// </summary>
        public bool FExecWhenStart { get; set; }

        /// <summary>
        /// 执行任务的方法 IP
        /// </summary>
        public string FTaskUrlIP { get; set; }

        public string FAddPerson { get; set; }

        public DateTime FAddTime { get; set; }

        public string FTaskKey { get; set; }

        public string FCustomParam { get; set; }

        /// <summary>
        /// 任务路由
        /// </summary>
        public string FTaskUrlRoute { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public int? FTaskSchedulingype { get; set; }

        /// <summary>
        /// 任务备注
        /// </summary>
        public string FRemark { get; set; }
    }
}
