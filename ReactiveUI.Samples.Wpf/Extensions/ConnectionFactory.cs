using Microsoft.Data.Sqlite;
using ReactiveUI.Samples.Wpf.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.Samples.Wpf.Extensions
{
    public static class ConnectionFactory
    {
        public static IDbConnection CreateSqlConnection()
        {
            return new SqliteConnection(DataSource.ConnectString);
        }
    }
}
