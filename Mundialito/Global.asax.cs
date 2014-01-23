using Mundialito.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Mundialito
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            BundleTable.EnableOptimizations = false;

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();

            Database.SetInitializer(new MundialitoContextInitializer());

            // Fix for Database Initializer, see http://stackoverflow.com/questions/12573927/database-initializer-not-being-called-in-ef-5-mvc-3
            MundialitoContext c = new MundialitoContext();
            c.Database.Initialize(true);
        }
    }
}
