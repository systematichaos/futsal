using Futsal.Data;
using Futsal.Services;
using Futsal.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Web;
using Unity;
using Unity.Injection;

namespace Futsal.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();


            //register authentication manager 
            container.RegisterType<IAuthenticationManager>(
               new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));


            //register iuser store becoz auth manager needs it so bad
            //container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
            // new InjectionConstructor(typeof(Identity.ApplicationDbContext)));

            container.RegisterType<IUserStore<ApplicationUser, int>, CustomUserStore>(
             new InjectionConstructor(typeof(Identity.ApplicationDbContext)));


            // TODO: Register your type's mappings here.          
            container.RegisterType<IUserAccountManager, UserAccountManager>();
            container.RegisterType<IUserAccountRepository, UserAccountRepository>();

            container.RegisterType<IIdentityManager, IdentityManager>();
            container.RegisterType<IIdentityRepository, IdentityRepository>();

            container.RegisterType<IApplicationManager, ApplicationManager>();
            container.RegisterType<IApplicationRepository, ApplicationRepository>();
            container.RegisterType<IScheduleManager, ScheduleManager>();
            container.RegisterType<IScheduleRepository, ScheduleRepository>();


            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
        }
    }
}