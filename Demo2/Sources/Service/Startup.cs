using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Business.Repositories.Blog;
using Business.Repositories.Users;
using Microsoft.Owin.Cors;
using Owin;

namespace Service
{
    /// <summary>
    /// The owin startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// This method will setup and store the configuration
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            // Register the routes to listen for
            config.Routes.MapHttpRoute("Reads", "api/{controller}/{user}");

            var containerBuilder = new ContainerBuilder();

            //The repositories needs to be updated with all the controller in this assembly
            //  Add the controllers to the container
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            containerBuilder.RegisterWebApiFilterProvider(config);

            //Register businness repositories
            containerBuilder.RegisterType<BlogRepository>().AsImplementedInterfaces();
            containerBuilder.RegisterType<UserRepository>().AsImplementedInterfaces();

            var container = containerBuilder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Allow cross origin scripting from everyone since this is an internal service
            app.UseCors(CorsOptions.AllowAll);
            // Add the AutoFac, AutoFac Web API and Web API middleware.
            // The order here is important, because otherwise the dependencies are not automatically injected.
            // Read more about it here: http://autofac.readthedocs.org/en/latest/integration/webapi.html#owin-integration
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            // Let the Web Api use the created configuration
            app.UseWebApi(config);
        }
    }
}
