using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiRAPI.DTO
{
    public class GoodsPageResult : PageResult<GoodResult>
    {
        public int? GroupId { get; set; }

        public int? ParentGroupId { get; set; }
    }   
}
