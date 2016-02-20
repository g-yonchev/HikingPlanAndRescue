namespace HikingPlanAndRescue.Web
{
    using System.Data.Entity;
    using System.Globalization;
    using System.Reflection;
    using System.Threading;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Data;
    using Data.Migrations;
    using Infrastructure.Binders;
    using Infrastructure.Mapping;

#pragma warning disable SA1649 // File name must match first type name
    public class MvcApplication : HttpApplication
#pragma warning restore SA1649 // File name must match first type name
    {
        protected void Application_Start()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            DatabaseConfig.Config();
            AutofacConfig.RegisterAutofac();

            ModelBinders.Binders.Add(typeof(double), new DoubleModelBinder());
            ModelBinders.Binders.Add(typeof(double?), new NullableDoubleModelBinder());

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(Assembly.GetExecutingAssembly());

        }
    }
}
