using LinqToDB.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiRAPI.DataModel
{
    public class MiRDataSettings : ILinqToDBSettings
    {
        private string _connectionString;

        public MiRDataSettings(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();

        public string DefaultConfiguration => "SqlServer";
        public string DefaultDataProvider => "SqlServer";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "MiR",
                        ProviderName = "SqlServer",
                        ConnectionString = _connectionString
                    };
            }
        }
    }
}
