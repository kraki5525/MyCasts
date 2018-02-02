using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SimpleInjector;

namespace MyCasts.Domain.Validation
{
    public class ValidationAsyncHandler<T,TK> : IAsyncRequestHandler<T, TK>
        where T : IRequest<TK>, INeedValidation
    {
        private readonly IAsyncRequestHandler<T, TK> _decoratee;
        private readonly IValidator<T> _validator;

        public ValidationAsyncHandler(IAsyncRequestHandler<T, TK> decoratee, IValidator<T> validator)
        {
            _decoratee = decoratee;
            _validator = validator;
        }

        public Task<TK> Handle(T message)
        {
            _validator.ValidateAndThrow(message);
            return _decoratee.Handle(message);
        }
    }
}