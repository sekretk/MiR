using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiRAPI.DTO
{
    public class GoodResult
    {
        public bool IsGroup { get; set; }

        public string Name { get; set; }

        public GoodResult(Good good)
        {
            Name = good.Name;
        }

        public GoodResult(GoodsGroup goodGroup)
        {
            IsGroup = true;

            Name = goodGroup.Name;
        }
    }
}
