using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using TaskManagement.Infrastructure;
using TaskManagement.Repository;

namespace TaskManagment.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            container.Register<ICurrentUserRepository, CurrentUserRepository>(Lifestyle.Scoped);
            container.Register<ITaskRepository, TaskRepository>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =  new SimpleInjectorWebApiDependencyResolver(container);

            // Web API configuration and services
            var core = new EnableCorsAttribute("http://localhost:4200,http://localhost:90", "*", "*");
            core.SupportsCredentials = true;
            config.EnableCors(core);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
