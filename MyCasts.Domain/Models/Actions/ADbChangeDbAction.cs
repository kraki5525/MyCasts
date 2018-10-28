using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace MyCasts.Domain.Models.Commands
{
    public abstract class ADbChangeAction<T> : IDbAction<T>
    {
        public T Model { get; set; }
        public abstract T Execute(DbConnection connection);
        public abstract Task<T> ExecuteAsync(DbConnection connection, CancellationToken token);
    }
}