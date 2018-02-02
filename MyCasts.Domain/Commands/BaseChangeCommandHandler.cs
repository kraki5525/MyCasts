using System.Threading.Tasks;
using BatMap;
using MediatR;
using MyCasts.Domain.Models;
using MyCasts.Domain.Models.Commands;

namespace MyCasts.Domain.Commands
{
    public abstract class BaseChangeCommandHandler<T,K, L, KL> : IAsyncRequestHandler<T, KL>
        where T : IRequest<KL>
        where K : IChangeDbAction<L>, new()
    {
        protected Db _db;

        public BaseChangeCommandHandler(Db db)
        {
            _db = db;
        }

        public async virtual Task<KL> Handle(T message)
        {
            var dbCommand = new K();
            dbCommand.Model = Convert(message);
            var result = await _db.ExecuteAsync(dbCommand);
            return Mapper.Map<KL>(result);
        }

        protected virtual L Convert(T message)
        {
            return Mapper.Map<L>(message);
        }
    }
}