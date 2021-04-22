using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTTaskDispatchService.Model.Dto
{
    public class Result
    {
        public int code { get; set; }

        public string Msg { get; set; }

        public dynamic data { get; set; }
    }
}
