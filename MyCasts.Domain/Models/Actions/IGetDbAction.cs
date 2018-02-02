using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace MyCasts.Domain.Models.Commands
{
    public abstract class IGetDbAction<T> : IDbAction<T>
    {
        public abstract T Execute(DbConnection connection);
        public abstract Task<T> ExecuteAsync(DbConnection connection);
    }
}
