using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using MyCasts.Domain.Models.Commands;

namespace MyCasts.Domain.Models
{
    public class Db
    {
        public string _connectionString { get; }
        public Db(string connectionString)
        {
            _connectionString = connectionString;
        }

        public virtual T Execute<T>(IDbAction<T> command)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                return command.Execute(connection);
            }
        }

        public virtual Task<T> ExecuteAsync<T>(IDbAction<T> command)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                return command.ExecuteAsync(connection);
            }
        }
    }
}