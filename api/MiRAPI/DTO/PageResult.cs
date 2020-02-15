using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiRAPI.DTO
{
    public class PageResult<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int Skiped { get; set; }

        public int TotalAmount { get; set; }
    }
}
