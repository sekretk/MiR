using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiRAPI.DataModel
{
    public partial class MiRDB : LinqToDB.Data.DataConnection    
    {
        public MiRDB() : base("MiR")
        {

        }
    }
}
