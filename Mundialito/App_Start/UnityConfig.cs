using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Practices.Unity;
using Mundialito.Controllers;
using Mundialito.DAL.Accounts;
using Mundialito.DAL.ActionLogs;
using Mundialito.DAL.Bets;
using Mundialito.DAL.Games;
using Mundialito.DAL.GeneralBets;
using Mundialito.DAL.Stadiums;
using Mundialito.DAL.Teams;
using Mundialito.Logic;
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

            container.RegisterType<ITeamsRepository, TeamsRepository>();
            container.RegisterType<IGamesRepository, GamesRepository>();
            container.RegisterType<IStadiumsRepository, StadiumsRepository>();
            container.RegisterType<IBetsRepository, BetsRepository>();
            container.RegisterType<IGeneralBetsRepository, GeneralBetsRepository>();
            container.RegisterType<IUsersRepository, UsersRepository>();
            container.RegisterType<IBetValidator, BetValidator>();
            container.RegisterType<IBetsResolver, BetsResolver>();
            container.RegisterType<ILoggedUserProvider, LoggedUserProvider>();
            container.RegisterType<IUsersRetriver, UsersRetriver>();
            container.RegisterType<IDateTimeProvider, DateTimeProvider>();
            container.RegisterType<IActionLogsRepository, ActionLogsRepository>();
            container.RegisterType<IAdminManagment, AdminManagment>();
            
         
            container.RegisterType<AccountController>(new InjectionConstructor());
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}