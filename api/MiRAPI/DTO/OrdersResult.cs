using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiRAPI.DTO
{
    public class OrdersResult
    {
        public int ID { get; set; }

        public DateTime Created { get; set; }

        public OrdersResult(int id, DateTime created)
        {
            ID = id;
            Created = created; 
        }
    }
}
