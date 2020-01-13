using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiRAPI.DTO
{
    public class ErrorDetails
    {
        public int ErrorCode { get; set; }

        public int ServerCode { get; set; }

        public string Message { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
