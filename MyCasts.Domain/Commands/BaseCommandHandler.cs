using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BatMap;
using MediatR;
using MyCasts.Domain.Models;
using MyCasts.Domain.Models.Commands;

namespace MyCasts.Domain.Commands
{
    public abstract class BaseCommandHandler<T,K, TK> : IAsyncRequestHandler<T, TK>
        where T : IRequest<TK>
        where K : IDbAction<TK>
    {
        protected Db _db;

        public BaseCommandHandler(Db db)
        {
            _db = db;
        }

        public async virtual  Task<TK> Handle(T message)
        {
            var dbCommand = Mapper.Map<K>(message);
            var result = await _db.ExecuteAsync(dbCommand);
            return result;
        }
    }

    public abstract class BaseCommandHandler<T,K, L, KL> : IAsyncRequestHandler<T, KL>
        where T : IRequest<KL>
        where K : IDbAction<L>
    {
        protected Db _db;
        
        public BaseCommandHandler(Db db)
        {
            _db = db;
        }

        public async virtual Task<KL> Handle(T message)
        {
            var dbCommand = Mapper.Map<K>(message);
            var result = await _db.ExecuteAsync(dbCommand);
            return Mapper.Map<KL>(result);
        }
    }
}