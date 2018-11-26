using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BatMap;
using MediatR;
using MyCasts.Domain.Models;
using MyCasts.Domain.Models.Commands;

namespace MyCasts.Domain.Commands
{
    public abstract class BaseCommandHandler<TRequest,TDbAction, KResponse> : IRequestHandler<TRequest, KResponse>
        where TRequest : IRequest<KResponse>
        where TDbAction : IDbAction<KResponse>
    {
        protected Db _db;

        public BaseCommandHandler(Db db)
        {
            _db = db;
        }

        public async virtual  Task<KResponse> Handle(TRequest message, CancellationToken token)
        {
            var dbCommand = Mapper.Map<TDbAction>(message);
            var result = await _db.ExecuteAsync(dbCommand, token);
            return result;
        }
    }

    public abstract class BaseCommandHandler<TRequest,TDbAction, KDbResult, KResponse> : IRequestHandler<TRequest, KResponse>
        where TRequest : IRequest<KResponse>
        where TDbAction : IDbAction<KDbResult>
    {
        protected IDb _db;

        public BaseCommandHandler(IDb db)
        {
            _db = db;
        }

        public async virtual Task<KResponse> Handle(TRequest message, CancellationToken token)
        {
            var dbCommand = Mapper.Map<TDbAction>(message);
            var result = await _db.ExecuteAsync(dbCommand, token);
            return Mapper.Map<KResponse>(result);
        }
    }
}