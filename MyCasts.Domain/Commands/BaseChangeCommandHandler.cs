using System.Threading;
using System.Threading.Tasks;
using BatMap;
using MediatR;
using MyCasts.Domain.Models;
using MyCasts.Domain.Models.Commands;

namespace MyCasts.Domain.Commands
{
    public abstract class BaseChangeCommandHandler<TRequest,TDbAction, KDbResult, KResponse> : IRequestHandler<TRequest, KResponse>
        where TRequest : IRequest<KResponse>
        where TDbAction : ADbChangeAction<KDbResult>, new()
    {
        protected IDb _db;

        public BaseChangeCommandHandler(IDb db)
        {
            _db = db;
        }

        public async virtual Task<KResponse> Handle(TRequest message, CancellationToken token)
        {
            var dbCommand = new TDbAction();
            dbCommand.Model = Convert(message);
            var result = await _db.ExecuteAsync(dbCommand, token);
            return Mapper.Map<KResponse>(result);
        }

        protected virtual KDbResult Convert(TRequest message)
        {
            return Mapper.Map<KDbResult>(message);
        }
    }
}