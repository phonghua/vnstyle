﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Ricky.Infrastructure.Core.ObjectContainer
{
    /// <summary>
    /// Provides access to the singleton instance of the engine.
    /// </summary>
    public class EngineContext
    {
        public static ITypeFinder DefaultTypeFinder = new AppDomainTypeFinder();
        #region Initialization Methods

        /// <summary>Initializes a static instance of the factory.</summary>
        /// <param name="forceRecreate">Creates a new factory instance even though the factory has been previously initialized.</param>
        /// <param name="typeFinder"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(bool forceRecreate, ITypeFinder typeFinder)
        {
            if (Singleton<IEngine>.Instance == null || forceRecreate)
            {
                Debug.WriteLine("Constructing engine " + DateTime.Now);
                Singleton<IEngine>.Instance = CreateEngineInstance(typeFinder);
                Debug.WriteLine("Initializing engine " + DateTime.Now);
                Singleton<IEngine>.Instance.Initialize();
            }
            return Singleton<IEngine>.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forceRecreate"></param>
        /// <param name="engineType"></param>
        /// <param name="typeFinder"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(bool forceRecreate, Type engineType, ITypeFinder typeFinder)
        {
            if (Singleton<IEngine>.Instance == null || forceRecreate)
            {
                Debug.WriteLine("Constructing engine " + DateTime.Now);
                Singleton<IEngine>.Instance = (IEngine)Activator.CreateInstance(engineType, typeFinder);

                Debug.WriteLine("Initializing engine " + DateTime.Now);
                Singleton<IEngine>.Instance.Initialize();
            }
            return Singleton<IEngine>.Instance;
        }


        /// <summary>Sets the static engine instance to the supplied engine. Use this method to supply your own engine implementation.</summary>
        /// <param name="engine">The engine to use.</param>
        /// <remarks>Only use this method if you know what you're doing.</remarks>
        public static void Replace(IEngine engine)
        {
            Singleton<IEngine>.Instance = engine;
        }

        /// <summary>
        /// Creates a factory instance and adds a http application injecting facility.
        /// </summary>
        /// <returns>A new factory</returns>
        public static IEngine CreateEngineInstance(ITypeFinder typeFinder)
        {
            var engines = typeFinder.FindClassesOfType<IEngine>().ToArray();
            //engines = engines.Where(it => it != typeof(MEF.MEFEngine)).ToArray();
            if (engines.Length > 0)
            {
                var defaultEngine = (IEngine)Activator.CreateInstance(engines[0], typeFinder);
                return defaultEngine;
            }
            else
            {
                //return new MEF.MEFEngine(typeFinder);
                throw new NotImplementedException();
            }
        }

        #endregion

        /// <summary>Gets the singleton engine used to access services.</summary>
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Initialize(false, DefaultTypeFinder);
                }
                return Singleton<IEngine>.Instance;
            }
        }
    }
}
