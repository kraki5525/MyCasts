using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SimpleInjector;

namespace MyCasts.Domain.Validation
{
    public class ValidationAsyncHandler<T,TK> : IRequestHandler<T, TK>
        where T : IRequest<TK>, INeedValidation
    {
        private readonly IRequestHandler<T, TK> _decoratee;
        private readonly IValidator<T> _validator;

        public ValidationAsyncHandler(IRequestHandler<T, TK> decoratee, IValidator<T> validator)
        {
            _decoratee = decoratee;
            _validator = validator;
        }

        public Task<TK> Handle(T message, CancellationToken token)
        {
            _validator.ValidateAndThrow(message);
            return _decoratee.Handle(message, token);
        }
    }
}