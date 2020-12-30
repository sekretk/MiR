using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiRAPI.DataModels
{
    public partial class MiRDB : LinqToDB.Data.DataConnection
    {
        public MiRDB() : base("MiR")
        {
            InitDataContext();
            InitMappingSchema();
        }
    }
}
