using Dapper;
using Microsoft.Data.Sqlite;
using ReactiveUI.Samples.Wpf.Common;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.Samples.Wpf.Services.Sqlite
{
    internal class DatabaseBootstrap : IDatabaseBootstrap
    {
        public IPeopleServices PeopleServices => Locator.Current.GetService<IPeopleServices>();

        public void Build()
        {
            RegisterServices();
            using var connection = new SqliteConnection(DataSource.ConnectString);
            var createTableSqlStr = @"CREATE TABLE IF NOT EXISTS People (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            Age INTEGER NOT NULL,
                            Sex TEXT,
                            Phone TEXT
                            );";
            var result = connection.Execute(createTableSqlStr);
            if (result == 0)
            {
                LogHost.Default.Info("dataBase build success.");
            }
            else
            {
                LogHost.Default.Fatal("dataBase build failure.");
            }
        }

        private void RegisterServices()
        {
            Locator.CurrentMutable.Register<IPeopleServices, PeopleServices>();
        }
    }
}
