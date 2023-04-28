using Microsoft.Data.Sqlite;
using ReactiveUI.Samples.Wpf.Common;
using System.Data;

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