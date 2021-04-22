using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTTaskDispatchService.Model.Dto
{
    public class ServerInfo
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int FID { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string FTaskName { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool FIsUsable { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public int FTaskType { get; set; }

        /// <summary>
        /// 执行任务时间间隔(秒)
        /// </summary>
        public int FInterval { get; set; }

        /// <summary>
        /// 执行时间【例如：00:00:01】
        /// </summary>
        public string FExecTime { get; set; }

        /// <summary>
        /// 是否启动执行
        /// </summary>
        public bool FExecWhenStart { get; set; }

        /// <summary>
        /// 执行任务的方法
        /// </summary>
        public string FTaskMethod { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        public string FAddPerson { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public int? FTaskSchedulingype { get; set; }

        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime FAddTime { get; set; }

        /// <summary>
        /// 任务标识
        /// </summary>
        public string FTaskKey { get; set; }
        /// <summary>
        /// 自定义参数
        /// </summary>
        public string FCustomParam { get; set; }

        /// <summary>
        /// 任务备注
        /// </summary>
        public string FRemark { get; set; }
    }

    public enum TaskTypeEnum
    {
        //定点任务
        FixpointTask = 0,
        //定时任务
        TimingTask = 1
    }
}
