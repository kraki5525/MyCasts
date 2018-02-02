using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace MyCasts.Domain.Models.Commands
{
    public abstract class IQueryDbAction<T> : IDbAction<IEnumerable<T>>
    {
        public abstract IEnumerable<T> Execute(DbConnection connection);
        public abstract Task<IEnumerable<T>> ExecuteAsync(DbConnection connection);
    }
}