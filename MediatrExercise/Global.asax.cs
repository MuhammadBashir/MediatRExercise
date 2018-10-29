using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using MediatrExercise.AutofacModules;
using MediatrExercise.Extensions;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace MediatrExercise
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            var configuration = new ConfigurationBuilder()
                .AddDefaultConfigurationFiles(Server.MapPath("~/"))
                .Build();

            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<MediatorModules>();

            builder.RegisterInstance(configuration).As<IConfiguration>();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            GlobalConfiguration.Configure(httpConfig => WebApiConfig.Register(httpConfig));
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

        }
    }
}
