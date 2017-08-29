using System.Data.Entity;
using Autofac;
using Microsoft.AspNet.Identity;
using Ricky.Infrastructure.Core;
using Ricky.Infrastructure.Core.Caching;
using Ricky.Infrastructure.Core.ObjectContainer;
using Ricky.Infrastructure.Core.ObjectContainer.Autofac.DependencyManagement;
using VnStyle.Services.Business;
using VnStyle.Services.Data;

namespace VnStyle.Web.Infrastructure.Dependency
{
    /// <summary>
    /// 
    /// </summary>
    public class AppDependency : IAutofacDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //OrmLiteConfig.DialectProvider = new SqlServerOrmLiteDialectProvider {UseUnicode = true};

            builder.RegisterType<NullCache>().As<ICacheManager>()
                .InstancePerRequest();

            builder.RegisterType<Security.Hash.HashIds>().As<IHashKey>()
                .SingleInstance();

            builder.RegisterType<WebHelper>().As<IWebHelper>()
                //.WithParameter("httpContext", httpContextBase)
                .InstancePerRequest();

            builder.RegisterType<VnStyleContext>().As<IDbContext>()
                //.WithParameter("connectionString", ApplicationConfiguration.BizConnectionString)
                .InstancePerRequest();


            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();

            //builder.RegisterAssemblyTypes(System.AppDomain.CurrentDomain.GetAssemblies())
            //       .Where(t => t.Name.EndsWith("Repository"))
            //       .AsImplementedInterfaces()
            //       .InstancePerRequest();

            builder.RegisterAssemblyTypes(System.AppDomain.CurrentDomain.GetAssemblies())
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                   .InstancePerRequest();

            builder.RegisterType<WorkContext>().As<IWorkContext>().InstancePerRequest();

            //builder.RegisterType<UserStore<ApplicationUser>>().As<IUserStore<ApplicationUser, long>>()
            //    .InstancePerRequest();

            builder.RegisterType<AppContextUrlRouting>().As<IAppContextUrlRouting>()
                .InstancePerRequest();



        }

        /// <summary>
        /// 
        /// </summary>
        public int Order => 2;
    }
}