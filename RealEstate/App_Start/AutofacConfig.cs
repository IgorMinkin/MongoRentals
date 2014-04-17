using System.Configuration;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using RealEstate.Database;
using RealEstate.Messaging;
using RealEstate.Security;

namespace RealEstate
{
    public class AutofacConfig
    {
        public static void RegisterBindings()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof (MvcApplication).Assembly);
            builder.RegisterFilterProvider();
            AddBindings(builder);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void AddBindings(ContainerBuilder builder)
        {
            string sqlConnectionString = ConfigurationManager.ConnectionStrings["RealEstateSql"].ConnectionString;

            builder.Register(d => MongoDbFactory.Instance);
            builder.RegisterType<RealEstateContext>().AsSelf();
            builder.RegisterType<IdentityContext>().AsSelf();
            builder.RegisterGeneric(typeof (UserManager<>)).AsSelf();
            builder.RegisterType<UserStore<IdentityUser>>().As<IUserStore<IdentityUser>>();
            builder.RegisterType<MessageBus>().AsSelf().InstancePerHttpRequest();
            builder.RegisterType<MessageLogger>()
                .AsSelf()
                .WithParameter("connectionString", sqlConnectionString);

        }
    }
}