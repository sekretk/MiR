using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiRAPI.DataModel
{
    public partial class IR2016DB : LinqToDB.Data.DataConnection    
    {
        public IR2016DB() : base("MiR")
        {

        }
    }
}
