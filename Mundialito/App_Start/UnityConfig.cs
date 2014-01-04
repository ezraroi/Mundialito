using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Practices.Unity;
using Mundialito.Controllers;
using Mundialito.DAL.Games;
using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Teams;
using Mundialito.Models;
using System.Web.Http;
using Unity.WebApi;

namespace Mundialito
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<TeamsController>();
            container.RegisterType<GamesController>();
            container.RegisterType<StadiumsController>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            //container.RegisterType<RolesAdminController>();
            container.RegisterType<ITeamsRepository, TeamsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IGamesRepository, GamesRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IStadiumsRepository, StadiumsRepository>(new HierarchicalLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}