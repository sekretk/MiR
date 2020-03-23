using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiRAPI.DTO
{
    public class PageResult<T>
    {
        /// <summary>
        /// День продаж
        /// </summary>
        public DateTime Date { get; set; }

        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Сколько пропущено
        /// </summary>
        public int Skiped { get; set; }

        /// <summary>
        /// Количество продаж
        /// </summary>
        public int TotalAmount { get; set; }
    }
}
