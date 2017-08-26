using Autofac;
using CQRSlite.Bus;
using CQRSlite.Commands;
using CQRSlite.Config;
using CQRSlite.Domain;
using CQRSlite.Events;
using Ken.Business.Bizsol;
using Ken.Business.Bizsol.Catalog.ReadModel.Handlers;
using Ken.Business.Bizsol.Catalog.WriteModel.Handlers;
using Ken.Infrastructure.Core.ObjectContainer;
using Ken.Infrastructure.Core.ObjectContainer.Autofac.DependencyManagement;

namespace Bizsol.WebFramework.Dependency
{
    public class CqrsDependency : IAutofacDependencyRegistrar
    {

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<InProcessBus>().As<ICommandSender>().As<IEventPublisher>().As<IHandlerRegistrar>().SingleInstance();

            builder.RegisterType<Session>().As<ISession>().InstancePerRequest();
            builder.RegisterType<InMemoryEventStore>().As<IEventStore>().InstancePerRequest();
            //builder.RegisterType(typeof(Repository)).Named("cqrs_repository", typeof(IRepository)).InstancePerRequest();

            builder.RegisterType<Repository>().As<IRepository>().InstancePerRequest();

            //builder.RegisterDecorator<IRepository>((c, inner) => new CacheRepository(inner, c.Resolve<IEventStore>()), "cqrs_repository");



            builder.RegisterAssemblyTypes(System.AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.IsSubclassOf(typeof(BaseCommandHandler))).InstancePerRequest();

            builder.RegisterAssemblyTypes(System.AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.IsSubclassOf(typeof(BaseEventHandler))).InstancePerRequest();



            //builder.RegisterType<CacheRepository>().As<IRepository>().InstancePerRequest();

            builder.RegisterAssemblyTypes(System.AppDomain.CurrentDomain.GetAssemblies())
                   .Where(t => t.Name.EndsWith("Facade"))
                   .AsImplementedInterfaces();

            builder.RegisterType<DependencyResolver>().As<IServiceLocator>().InstancePerRequest();

            //ISnapshotStore
            //ISnapshotStore snapshotStore, ISnapshotStrategy snapshotStrategy, IRepository repository, IEventStore eventStore

            //InventoryCommandHandlers


            //var serviceLocator = EngineContext.Current.Resolve<IServiceLocator>();
            //var registrar = new BusRegistrar(serviceLocator);
            //registrar.Register(typeof(InventoryCommandHandlers));


            //s.AssemblyContainingType<SmDependencyResolver>();

            //s.AssemblyContainingType<SmDependencyResolver>();
        }

        public int Order { get { return 1; } }
    }


}
