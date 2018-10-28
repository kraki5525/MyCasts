using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using BatMap;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using MyCasts.Domain.Commands;
using MyCasts.Domain.Models;
using MyCasts.Domain.Services;
using MyCasts.Domain.Validation;
using SimpleInjector;

namespace MyCasts.Domain
{
    public class Module : IModule
    {
        public void Initialize(Container container, IConfigurationRoot configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            Mapper.RegisterMap<Uri, Uri>((uri, context) => new Uri(uri, ""));

            container.RegisterSingleton(typeof(Db), () => new Db(connectionString));
            container.RegisterDecorator(typeof(IRequestHandler<,>), 
                                         typeof(ValidationAsyncHandler<,>));
            
            foreach (var validator in GetValidatorTypes())
            {
                container.RegisterSingleton(validator.Item1, validator.Item2);
            }
        }

        public IEnumerable<Tuple<Type, Type>> GetValidatorTypes()
        {
            // var types = this.GetType().Assembly.GetTypes().ToArray();
            // var typesAndInterfaces = types.Select(x => new {Type = x, Interfaces = x.GetInterfaces()}).ToArray();
            // var typesIVald = typesAndInterfaces.Select(x => x.Interfaces.GetInterfaces().Where(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof))

            return (from type in this.GetType().Assembly.GetTypes()
                    let intf = type.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IValidator<>))
                    where intf != null
                    select Tuple.Create(intf, type));
        }
    }
}