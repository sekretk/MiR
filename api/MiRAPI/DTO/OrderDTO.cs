using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiRAPI.DTO
{
    public class OrderItem
    {
        public int GoodId { get; set; }

        public int Qtty { get; set; }
    }

    public class OrderDTO
    {
        public IEnumerable<OrderItem> Items { get; set; }

        public int ObjectId { get; set; }
    }
}
