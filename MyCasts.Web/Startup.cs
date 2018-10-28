using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MediatR;

using SimpleInjector;
using SimpleInjector.Lifestyles;
using SimpleInjector.Integration.AspNetCore;
using SimpleInjector.Integration.AspNetCore.Mvc;

using MyCasts.Domain.Commands;
using MyCasts.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using MediatR.Pipeline;
using MyCasts.Domain.Services;
using MyCasts.Domain;
using System.IO;

namespace MyCasts.Web
{
    public class Startup
    {
        private Container _container;

        public Startup(IHostingEnvironment env)
        {
            _container = new Container();

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            IntegrationSimpleInjector(services);

            // services.AddMediatR(typeof(GetPodcastsCommand).GetTypeInfo().Assembly);
            // services.AddScoped<Db>(provider => new Db(Configuration.GetConnectionString("DefaultConnection")));   
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            InitializeContainer(app);

            _container.Verify();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            // {
            //     app.UseDeveloperExceptionPage();
            // }
            // else
            // {
            //     app.UseExceptionHandler("/Home/Error");
            // }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            // app.UseMvc();
            // app.Run(async (context) =>
            // {
            //     await context.Response.WriteAsync("Test");
            // });
        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            var assemblies = GetAssemblies();
            var modules = GetAllCommandModules(assemblies);

            // Add application presentation components:
            _container.RegisterMvcControllers(app);
            _container.RegisterMvcViewComponents(app);

            // Add application services. For instance:

            _container.RegisterSingleton<IMediator, Mediator>();
            // _container.Register(typeof(IRequestHandler<,>), assemblies);
            // _container.Register(typeof(IRequestHandler<>), assemblies);

            // //Pipeline
            // _container.RegisterCollection(typeof(IPipelineBehavior<,>), Enumerable.Empty<Type>());
            // _container.RegisterCollection(typeof(IRequestPreProcessor<>), Enumerable.Empty<Type>());
            // _container.RegisterCollection(typeof(IRequestPostProcessor<,>), Enumerable.Empty<Type>());
            // _container.RegisterSingleton(new SingleInstanceFactory(_container.GetInstance));
            // _container.RegisterSingleton(new MultiInstanceFactory(_container.GetAllInstances));

            _container.Register(typeof(IRequestHandler<,>), assemblies);

            // we have to do this because by default, generic type definitions (such as the Constrained Notification Handler) won't be registered
            var notificationHandlerTypes = _container.GetTypesToRegister(typeof(INotificationHandler<>), assemblies, new TypesToRegisterOptions
            {
                IncludeGenericTypeDefinitions = true,
                IncludeComposites = false,
            });
            _container.Collection.Register(typeof(INotificationHandler<>), notificationHandlerTypes);

            //Pipeline
            _container.Collection.Register(typeof(IPipelineBehavior<,>), new []
            {
                typeof(RequestPreProcessorBehavior<,>),
                typeof(RequestPostProcessorBehavior<,>),
            });

            _container.Register(() => new ServiceFactory(_container.GetInstance), Lifestyle.Singleton);

            foreach (var module in modules)
            {
                module.Initialize(_container, Configuration);
            }

            // Cross-wire ASP.NET services (if any). For instance:
            _container.CrossWire<ILoggerFactory>(app);
        }

        private void IntegrationSimpleInjector(IServiceCollection services)
        {
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(_container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(_container));

            services.EnableSimpleInjectorCrossWiring(_container);
            services.UseSimpleInjectorAspNetRequestScoping(_container);
        }

        private static List<IModule> GetAllCommandModules(IEnumerable<Assembly> assemblies)
        {
            return assemblies
                .SelectMany(x => x.GetExportedTypes())
                .Where(x => !x.IsAbstract && typeof(IModule).IsAssignableFrom(x))
                .Select(x => (IModule)Activator.CreateInstance(x))
                .ToList();
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(IModule).GetTypeInfo().Assembly;
        }
    }
}
