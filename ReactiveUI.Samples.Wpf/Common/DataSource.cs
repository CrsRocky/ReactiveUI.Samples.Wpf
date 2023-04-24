using Microsoft.Data.Sqlite;

namespace ReactiveUI.Samples.Wpf.Common
{
    public class DataSource
    {
        public static string DataSourceString => "Reactive.db";

        public static string ConnectString => new SqliteConnectionStringBuilder()
        {
            Mode = SqliteOpenMode.ReadWriteCreate,
            DataSource = DataSourceString,
            Cache = SqliteCacheMode.Shared,
            //Password = "123qwe"
        }.ToString();
    }
}