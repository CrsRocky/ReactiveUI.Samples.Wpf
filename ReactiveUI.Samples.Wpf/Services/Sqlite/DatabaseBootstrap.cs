using Dapper;
using Microsoft.Data.Sqlite;
using ReactiveUI.Samples.Wpf.Common;
using Splat;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI.Samples.Wpf.Extensions;

namespace ReactiveUI.Samples.Wpf.Services.Sqlite
{
    internal class DatabaseBootstrap : IDatabaseBootstrap
    {
        public void Build()
        {
            RegisterServices();
            CreateTable();
        }

        private void RegisterServices()
        {
            Locator.CurrentMutable.Register<IPeopleServices, PeopleServices>();
        }

        private void CreateTable()
        {
            using var connection = ConnectionFactory.CreateSqlConnection();
            var createTableSqlStr = @"CREATE TABLE IF NOT EXISTS People (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            Age INTEGER NOT NULL,
                            Sex TEXT,
                            Phone TEXT);";
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
    }
}
