using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Core.Lifetime;
using Autofac.Integration.Mvc;
using Ricky.Infrastructure.Core.ObjectContainer.Dependency;

namespace Ricky.Infrastructure.Core.ObjectContainer.Autofac.DependencyManagement
{
    public class ContainerManager : IContainerManager
    {
        private IContainer _container;

        public ContainerManager(IContainer container)
        {
            _container = container;
        }

        public IContainer Container
        {
            get { return _container; }
        }

        #region "Private functions"

        //public T Resolve<T>(string key = "", ILifetimeScope scope = null) where T : class
        //{
        //    if (scope == null)
        //    {
        //        //no scope specified
        //        scope = Scope();
        //    }
        //    if (string.IsNullOrEmpty(key))
        //    {
        //        return scope.Resolve<T>();
        //    }
        //    return scope.ResolveKeyed<T>(key);
        //}

        //public object Resolve(Type type, ILifetimeScope scope = null)
        //{
        //    if (scope == null)
        //    {
        //        //no scope specified
        //        scope = Scope();
        //    }
        //    return scope.Resolve(type);
        //}

        //public T[] ResolveAll<T>(string key = "", ILifetimeScope scope = null)
        //{
        //    if (scope == null)
        //    {
        //        //no scope specified
        //        scope = Scope();
        //    }
        //    if (string.IsNullOrEmpty(key))
        //    {
        //        return scope.Resolve<IEnumerable<T>>().ToArray();
        //    }
        //    return scope.ResolveKeyed<IEnumerable<T>>(key).ToArray();
        //}

        //public T ResolveUnregistered<T>(ILifetimeScope scope = null) where T : class
        //{
        //    return ResolveUnregistered(typeof(T), scope) as T;
        //}

        //public object ResolveUnregistered(Type type, ILifetimeScope scope = null)
        //{
        //    if (scope == null)
        //    {
        //        //no scope specified
        //        scope = Scope();
        //    }
        //    var constructors = type.GetConstructors();
        //    foreach (var constructor in constructors)
        //    {
        //        try
        //        {
        //            var parameters = constructor.GetParameters();
        //            var parameterInstances = new List<object>();
        //            foreach (var parameter in parameters)
        //            {
        //                var service = Resolve(parameter.ParameterType, scope);
        //                if (service == null) throw new Exception("Unkown dependency");
        //                parameterInstances.Add(service);
        //            }
        //            return Activator.CreateInstance(type, parameterInstances.ToArray());
        //        }
        //        catch (Exception)
        //        {

        //        }
        //    }
        //    throw new Exception("No contructor was found that had all the dependencies satisfied.");
        //}

        //public bool TryResolve(Type serviceType, ILifetimeScope scope, out object instance)
        //{
        //    if (scope == null)
        //    {
        //        //no scope specified
        //        scope = Scope();
        //    }
        //    return scope.TryResolve(serviceType, out instance);
        //}

        //public bool IsRegistered(Type serviceType, ILifetimeScope scope = null)
        //{
        //    if (scope == null)
        //    {
        //        //no scope specified
        //        scope = Scope();
        //    }
        //    return scope.IsRegistered(serviceType);
        //}

        //public object ResolveOptional(Type serviceType, ILifetimeScope scope = null)
        //{
        //    if (scope == null)
        //    {
        //        //no scope specified
        //        scope = Scope();
        //    }
        //    return scope.ResolveOptional(serviceType);
        //}

        public ILifetimeScope Scope()
        {
            try
            {
                if (HttpContext.Current != null)
                    return AutofacDependencyResolver.Current.RequestLifetimeScope;

                //when such lifetime scope is returned, you should be sure that it'll be disposed once used (e.g. in schedule tasks)
                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
            catch (Exception)
            {
                //we can get an exception here if RequestLifetimeScope is already disposed
                //for example, requested in or after "Application_EndRequest" handler
                //but note that usually it should never happen

                //when such lifetime scope is returned, you should be sure that it'll be disposed once used (e.g. in schedule tasks)
                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
        }
        #endregion

        public void Dispose()
        {
            if (_container != null)
            {
                _container.Dispose();
                _container = null;
            }
        }

        #region "Add components"
        public void AddResolvingObserver(IResolvingObserver observer)
        {
            throw new NotImplementedException();
        }

        public void AddComponent<TService>(string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Transient)
        {
            throw new NotImplementedException();
        }

        public void AddComponent(Type service, string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Transient)
        {
            throw new NotImplementedException();
        }

        public void AddComponent<TService, TImplementation>(string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Transient)
        {
            throw new NotImplementedException();
        }

        public void AddComponent(Type service, Type implementation, string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton,
            params Parameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public void AddComponentInstance<TService>(object instance, string key = "")
        {
            throw new NotImplementedException();
        }

        public void AddComponentInstance(object instance, string key = "")
        {
            throw new NotImplementedException();
        }

        public void AddComponentInstance(Type service, object instance, string key = "")
        {
            throw new NotImplementedException();
        }

        #endregion

        

        public T Resolve<T>(string key = "", params Parameter[] parameters) where T : class
        {
            if (string.IsNullOrEmpty(key))
            {
                return Scope().Resolve<T>();
            }
            return Scope().ResolveKeyed<T>(key);
        }

        public object Resolve(Type type, string key = "", params Parameter[] parameters)
        {
            if (String.IsNullOrEmpty(key))
            {
                return Scope().Resolve(type);
            }
            return Scope().ResolveKeyed(key, type);
        }

        public object ResolveGeneric(Type genericType, params Type[] genericTypeParameters)
        {
            throw new NotImplementedException();
        }

        public T[] ResolveAll<T>(string key = "")
        {
            if (string.IsNullOrEmpty(key))
            {
                return Scope().Resolve<IEnumerable<T>>().ToArray();
            }
            return Scope().ResolveKeyed<IEnumerable<T>>(key).ToArray();
        }

        public object[] ResolveAll(Type type, string key = "")
        {
            return null;
        }

        public T TryResolve<T>(string key = "", params Parameter[] parameters)
        {
            return (T)TryResolve(typeof(T), key, parameters);
        }

        public object TryResolve(Type type, string key = "", params Parameter[] parameters)
        {
            return Resolve(type, key, parameters);
        }

        public T ResolveUnregistered<T>() where T : class
        {
            return null;
        }

        public object ResolveUnregistered(Type type)
        {
            return null;
        }

        public void InjectProperties(object instance)
        {
            this.Container.InjectProperties(instance);
        }

        private void UpdateContainer(Action<ContainerBuilder> action)
        {
            var builder = new ContainerBuilder();
            action.Invoke(builder);
            builder.Update(_container);
        }
    }
}
