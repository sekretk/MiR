using MiRAPI.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiRAPI.DTO
{
    public class OperationsResult: PageResult<OperationAggregation>
    {
        /// <summary>
        /// День продаж
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Суммарная наличка
        /// </summary>
        public decimal Cash { get; set; }

        /// <summary>
        /// Суммарный безнал
        /// </summary>
        public decimal Card { get; set; }

        /// <summary>
        /// Средний чек
        /// </summary>
        public decimal Average { get; set; }    
    }
}
