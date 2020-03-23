using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiRAPI.DataModel
{
    public class OperationAggregation
    {
        public int? Acct { get; set; }

        public DateTime? Date { get; set; }

        public int PositionsCount { get; set; }

        public int GoodsAmount { get; set; }

        public decimal Cash { get; set; }

        public decimal Card { get; set; }
    }
}
