using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiRAPI.DTO
{
    public class GoodResult
    {
        public bool IsGroup { get; set; }

        public int ID { get; set; }

        public string Name { get; set; }

        public double? Quantity { get; set; } 

        public GoodResult(string name, int id, double? qtty, bool isGroup)
        {
            Name = name;

            ID = id;

            Quantity = qtty;

            IsGroup = isGroup;
        }
    }
}
