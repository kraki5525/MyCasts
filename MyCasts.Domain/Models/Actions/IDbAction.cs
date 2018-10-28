using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace MyCasts.Domain.Models.Commands
{
    public interface IDbAction<T>
    {
        T Execute(DbConnection connection);
        Task<T> ExecuteAsync(DbConnection connection, CancellationToken token);
    }
}