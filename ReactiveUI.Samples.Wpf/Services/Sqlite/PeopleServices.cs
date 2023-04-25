using Dapper;
using Microsoft.Data.Sqlite;
using ReactiveUI.Samples.Wpf.Extensions;
using ReactiveUI.Samples.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.Samples.Wpf.Services.Sqlite
{
    public class PeopleServices : IPeopleServices
    {
        public async Task<int> AddAsync(PeopleModel model)
        {
            using var connector = ConnectionFactory.CreateSqlConnection();
            var insert = @"INSERT INTO People (Name, Age, Sex, Phone)
                        VALUES (@Name, @Age, @Sex, @Phone);";
            return await connector.ExecuteAsync(insert, model);
        }

        public async Task<IEnumerable<PeopleModel>> GetAllAsync()
        {
            using var connector = ConnectionFactory.CreateSqlConnection();
            return await connector.QueryAsync<PeopleModel>("select * from People");
        }

        public async Task<int> RemoveAsync(int id)
        {
            using var connector = ConnectionFactory.CreateSqlConnection();
            return await connector.ExecuteAsync($"delete from People where ID={id}");
        }

        public async Task<PeopleModel> SearchAsync(int id)
        {
            using var connector = ConnectionFactory.CreateSqlConnection();
            return await connector.QueryFirstOrDefaultAsync<PeopleModel>(@"select * from table where Id =@Id", id);
        }

        public async Task<int> UpDateAsync(PeopleModel model)
        {
            using var connector = ConnectionFactory.CreateSqlConnection();
            var update = @"update People set Name = @Name,
                                            Age = @Age,
                                            Sex = @Sex,
                                            Phone = @Phone 
                                            where Id=@Id";
            return await connector.ExecuteAsync(update, model);
        }
    }
}
