using System.Web.Http;
using System.Web.Mvc;
using SailorsWebApi.BLL;
using SailorsWebApi.BLL_Interfaces;
using SailorsWebApi.DAL;
using SailorsWebApi.DAL_Interfaces;
using Unity;
using Unity.Mvc5;
using Unity.WebApi;

namespace SailorsWebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IFunctionsRepository, FunctionsRepository>();
            container.RegisterType<IHarbourRentRepository, HarbourRentRepository>();
            container.RegisterType<IEquipmentRentRepository, EquipmentRentRepository>();
            container.RegisterType<IEquipmentTypeRepository, EquipmentTypeRepository>();
            container.RegisterType<IEquipmentRepository, EquipmentRepository>();

            container.RegisterType<IUsersServices, UsersServices>();
            container.RegisterType<IRentingServices, RentingServices>();


            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}