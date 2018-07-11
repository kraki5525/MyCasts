using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BatMap;
using MediatR;
using MyCasts.Domain.Models;
using MyCasts.Domain.Models.Commands;

namespace MyCasts.Domain.Commands
{
    public abstract class BaseCommandHandler<TRequest,TDbAction, KResponse> : IAsyncRequestHandler<TRequest, KResponse>
        where TRequest : IRequest<KResponse>
        where TDbAction : IDbAction<KResponse>
    {
        protected Db _db;

        public BaseCommandHandler(Db db)
        {
            _db = db;
        }

        public async virtual  Task<KResponse> Handle(TRequest message)
        {
            var dbCommand = Mapper.Map<TDbAction>(message);
            var result = await _db.ExecuteAsync(dbCommand);
            return result;
        }
    }

    public abstract class BaseCommandHandler<TRequest,TDbAction, KDbResult, KResponse> : IAsyncRequestHandler<TRequest, KResponse>
        where TRequest : IRequest<KResponse>
        where TDbAction : IDbAction<KDbResult>
    {
        protected Db _db;

        public BaseCommandHandler(Db db)
        {
            _db = db;
        }

        public async virtual Task<KResponse> Handle(TRequest message)
        {
            var dbCommand = Mapper.Map<TDbAction>(message);
            var result = await _db.ExecuteAsync(dbCommand);
            return Mapper.Map<KResponse>(result);
        }
    }
}