using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Ricky.Infrastructure.Core.ObjectContainer.Dependency;

namespace Ricky.Infrastructure.Core.ObjectContainer.Autofac.DependencyManagement
{
    public class AutofacContainerManager : IContainerManager
    {
        private IContainer _container;

        public IContainer Container { get { return _container; } }

        public AutofacContainerManager(IContainer container)
        {
            this._container = container;
        }


        public void Dispose()
        {
            if (_container != null)
            {
                _container.Dispose();
                _container = null;
            }
        }

        public void AddResolvingObserver(IResolvingObserver observer)
        {

        }

        public void AddComponent<TService>(string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Transient)
        {
            AddComponent<TService, TService>(key, lifeStyle);
        }

        public void AddComponent(Type service, string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Transient)
        {
            AddComponent(service, service, key, lifeStyle);
        }

        public void AddComponent<TService, TImplementation>(string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Transient)
        {
            UpdateContainer(x =>
            {
                var serviceTypes = new List<Type> { typeof(TService) };

                if ((typeof(TService)).IsGenericType)
                {
                    var temp = x.RegisterGeneric(typeof(TImplementation)).As(serviceTypes.ToArray()).PerLifeStyle(lifeStyle);
                    if (!string.IsNullOrEmpty(key))
                    {
                        temp.Keyed(key, typeof(TService));
                    }
                }
                else
                {
                    var temp = x.RegisterType(typeof(TImplementation)).As(
                        serviceTypes.ToArray()).PerLifeStyle(lifeStyle);
                    if (!string.IsNullOrEmpty(key))
                    {
                        temp.Keyed(key, typeof(TService));
                    }
                }
            });
        }

        public void AddComponent(Type service, Type implementation, string key = "", ComponentLifeStyle lifeStyle = ComponentLifeStyle.Singleton,
            params Parameter[] parameters)
        {

            UpdateContainer(x =>
            {
                var serviceTypes = new List<Type> { service };

                if (service.IsGenericType)
                {
                    var temp = x.RegisterGeneric(implementation).As(serviceTypes.ToArray()).PerLifeStyle(lifeStyle);
                    if (parameters != null && parameters.Any())
                    {
                        temp = temp.WithParameters(parameters.Select(p => new NamedParameter(p.Name, p.ValueCallback())));
                    }
                    if (!string.IsNullOrEmpty(key))
                    {
                        temp.Keyed(key, service);
                    }
                }
                else
                {
                    var temp = x.RegisterType(implementation).As(serviceTypes.ToArray()).PerLifeStyle(lifeStyle);
                    if (parameters != null && parameters.Any())
                    {
                        temp = temp.WithParameters(parameters.Select(p => new NamedParameter(p.Name, p.ValueCallback())));
                    }
                    if (!string.IsNullOrEmpty(key))
                    {
                        temp.Keyed(key, service);
                    }
                }
            });


            //    UpdateContainer(x =>
            //    {
            //        var serviceTypes = new List<Type> { service };

            //        var temp = x.RegisterType(implementation).As(serviceTypes.ToArray()).
            //            WithParameters(properties.Select(y => new NamedParameter(y.Key, y.Value)));
            //        if (!string.IsNullOrEmpty(key))
            //        {
            //            temp.Keyed(key, service);
            //        }
            //    });

        }

        public void AddComponentInstance<TService>(object instance, string key = "")
        {
            AddComponentInstance(typeof(TService), instance, key);
        }

        public void AddComponentInstance(object instance, string key = "")
        {
            AddComponentInstance(instance.GetType(), instance, key);
        }

        public void AddComponentInstance(Type service, object instance, string key = "")
        {
            UpdateContainer(x =>
            {
                var registration = x.RegisterInstance(instance).Keyed(key, service).As(service).PerLifeStyle(ComponentLifeStyle.Transient);
            });
        }

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

        public ILifetimeScope Scope()
        {
            try
            {
                return AutofacRequestLifetimeHttpModule.GetLifetimeScope(Container, null);
            }
            catch (Exception)
            {
                return Container;
            }

            //try
            //{
            //    if (HttpContext.Current != null)
            //        return AutofacDependencyResolver.Current.RequestLifetimeScope;

            //    //when such lifetime scope is returned, you should be sure that it'll be disposed once used (e.g. in schedule tasks)
            //    return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            //}
            //catch (Exception)
            //{
            //    //we can get an exception here if RequestLifetimeScope is already disposed
            //    //for example, requested in or after "Application_EndRequest" handler
            //    //but note that usually it should never happen

            //    //when such lifetime scope is returned, you should be sure that it'll be disposed once used (e.g. in schedule tasks)
            //    return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            //}
        }

    }
}
