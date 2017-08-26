using System;
using Autofac;
using GraphQL;
using GraphQL.Http;
using Ricky.Infrastructure.Core.ObjectContainer;
using Ricky.Infrastructure.Core.ObjectContainer.Autofac.DependencyManagement;
using Ricky.Services.GraphQl.Global;
using Ricky.Services.GraphQl.Types;


namespace Biz.WebInfratructure.Dependency
{
    /// <summary>
    /// 
    /// </summary>
    public class GraphQlDependency : IAutofacDependencyRegistrar
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="typeFinder"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //builder.RegisterType<Security.Hash.HashIds>().As<IHashKey>()
            //    .SingleInstance();

            //var container = new SimpleContainer();
            //container.Singleton<IDocumentExecuter>(new DocumentExecuter());
            //container.Singleton<IDocumentWriter>(new DocumentWriter(true));

            //container.Singleton(new StarWarsData());
            //container.Register<StarWarsQuery>();
            //container.Register<HumanType>();
            //container.Register<DroidType>();
            //container.Register<CharacterInterface>();
            //container.Singleton(new StarWarsSchema(type => (GraphType)container.Get(type)));

            builder.RegisterType<DocumentExecuter>().As<IDocumentExecuter>()
                .SingleInstance();

            builder.RegisterType<DocumentWriter>().As<IDocumentWriter>()
                .WithParameter("indent", true)
                .SingleInstance();

            builder.RegisterGeneric(typeof(PaginationType<,>)).AsSelf().InstancePerDependency();

            builder.RegisterType<DataContext>().AsSelf()
               .InstancePerRequest();

            builder.RegisterType<Ricky.Services.GraphQl.BeauteApp.DataContext>().AsSelf()
               .InstancePerRequest();

            builder.RegisterType<Ricky.Services.GraphQl.Admin.DataContext>().AsSelf()
               .InstancePerRequest();

            builder.RegisterType<Ricky.Services.GraphQl.Market.DataContext>().AsSelf()
               .InstancePerRequest();

            //builder.RegisterType<GraphQuery>().As<GraphQuery>()
            //   .InstancePerRequest();



            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                   .Where(t => 
                        typeof(Ricky.Services.GraphQl.IServiceGraphType).IsAssignableFrom(t)
                        || typeof(Ricky.Services.GraphQl.IServiceGraphQuery).IsAssignableFrom(t)
                        || typeof(Ricky.Services.GraphQl.IServiceGraphSchema).IsAssignableFrom(t)
                   )
                   .AsSelf()
                   .InstancePerRequest();

            //var types = assembly.ExportedTypes.Where(t => typeof(GraphType).IsAssignableFrom(t) && !ExcludedTypes.Contains(t)).ToDictionary(t => t.Name, t => t);

            //container.Singleton(new StarWarsSchema(type => (GraphType)container.Get(type)));

        }

        /// <summary>
        /// 
        /// </summary>
        public int Order => 3;
    }
}