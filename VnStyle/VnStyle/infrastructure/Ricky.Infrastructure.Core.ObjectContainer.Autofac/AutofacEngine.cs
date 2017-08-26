using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Ricky.Infrastructure.Core.ObjectContainer.Dependency;
using Autofac.Integration.WebApi;
using System.Web.Http;
using Ricky.Infrastructure.Core.ObjectContainer;
using Ricky.Infrastructure.Core.ObjectContainer.Autofac.DependencyManagement;
using Ricky.Infrastructure.Core.ObjectContainer.Dependency;

namespace Ricky.Infrastructure.Core.ObjectContainer.Autofac
{
    /// <summary>
    /// </summary>
    public class AutofacEngine : IEngine
    {
        #region Fields

        /// <summary>
        /// The _container manager.
        /// </summary>
        private IContainerManager _containerManager;

        private ITypeFinder _typeFinder;

        #endregion

        #region Constructors and Destructors

        public AutofacEngine()
            : this(new AppDomainTypeFinder())
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacEngine"/> class.
        /// </summary>
        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacEngine"/> class.
        /// </summary>
        public AutofacEngine(ITypeFinder typeFinder)
        {

            this._typeFinder = typeFinder;
            InitializeContainer();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the container manager.
        /// </summary>
        public IContainerManager ContainerManager
        {
            get
            {
                return this._containerManager;
            }
        }

        public T Resolve<T>(params Parameter[] parameters) where T : class
        {
            if (parameters == null) throw new ArgumentNullException("parameters");
            return this.ContainerManager.Resolve<T>(string.Empty, parameters);
        }

        public T Resolve<T>(string name, params Parameter[] parameters) where T : class
        {
            return this.ContainerManager.Resolve<T>(name, parameters);
        }

        public object Resolve(Type type, params Parameter[] parameters)
        {
            return this.ContainerManager.Resolve(type, string.Empty, parameters);
        }

        public object Resolve(Type type, string name, params Parameter[] parameters)
        {
            return this.ContainerManager.Resolve(type, name, parameters);
        }

        public object ResolveGeneric(Type genericType, params Type[] genericTypeParameters)
        {
            return this.ContainerManager.ResolveGeneric(genericType, genericTypeParameters);
        }

        public T TryResolve<T>(params Parameter[] parameters) where T : class
        {
            return this.ContainerManager.TryResolve<T>(String.Empty, parameters);
        }

        public T TryResolve<T>(string name, params Parameter[] parameters) where T : class
        {
            return this.ContainerManager.TryResolve<T>(name, parameters);
        }

        public object TryResolve(Type type, params Parameter[] parameters)
        {
            return this.ContainerManager.TryResolve(type, String.Empty, parameters);
        }

        public object TryResolve(Type type, string name, params Parameter[] parameters)
        {
            return this.ContainerManager.TryResolve(type, name, parameters);
        }

        public IEnumerable<object> ResolveAll(Type serviceType)
        {
            return this.ContainerManager.ResolveAll(serviceType, string.Empty);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return this.ContainerManager.ResolveAll<T>();

        }

        public void InjectProperties(object instance)
        {
            this.ContainerManager.InjectProperties(instance);
        }

        #endregion

        #region Public Methods and Operators

        protected virtual void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            //dependencies
            //var typeFinder = new WebAppTypeFinder(config);
            builder = new ContainerBuilder();
            //builder.RegisterInstance(config).As<NopConfig>().SingleInstance();
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            builder.RegisterInstance(_typeFinder).As<ITypeFinder>().SingleInstance();
            builder.Update(container);

            //register dependencies provided by other assemblies
            builder = new ContainerBuilder();
            var drTypes = _typeFinder.FindClassesOfType<IAutofacDependencyRegistrar>();
            var drInstances = new List<IAutofacDependencyRegistrar>();
            foreach (var drType in drTypes)
                drInstances.Add((IAutofacDependencyRegistrar)Activator.CreateInstance(drType));
            //sort
            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
            {
                dependencyRegistrar.Register(builder, _typeFinder);
            }

            builder.Update(container);
            this._containerManager = new AutofacContainerManager(container);

            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;

            //set dependency resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            
            
        }

        /// <summary>
        /// The initialize.
        /// </summary>
        public void Initialize()
        {
        }

        /// <summary>
        /// The resolve.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T Resolve<T>() where T : class
        {
            return this.ContainerManager.Resolve<T>();
        }

        /// <summary>
        /// The resolve.
        /// </summary>
        /// <param name="serviceType">
        /// The service type.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object Resolve(Type serviceType)
        {
            return this.ContainerManager.Resolve(serviceType);
        }


        #endregion

        #region Methods

        /// <summary>
        /// The resolve all.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T[]"/>.
        /// </returns>
        /// <summary>
        /// The initialize container.
        /// </summary>
        private void InitializeContainer()
        {
            RegisterDependencies();
        }

        #endregion
    }
}