using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Cardinal.IoC.Registration;

namespace Cardinal.IoC
{
    public class ContainerManager : IContainerManager
    {
        private static readonly Dictionary<string, IContainerAdapter> adapters = new Dictionary<string, IContainerAdapter>();

        private static bool hasScanned;

        private readonly bool preventAssemblyScan;

        private IContainerAdapter currentAdapter;

        public ContainerManager() : this(String.Empty)
        {
        }
        
        public ContainerManager(IContainerAdapter containerAdapter)
        {
            preventAssemblyScan = true;
            CurrentAdapter = containerAdapter;
        }

        public ContainerManager(string adapterName)
        {
            CurrentAdapter = GetAdapter(adapterName);
        }

        protected Dictionary<string, IContainerAdapter> Adapters
        {
            get
            {
                PopulateAdapters();
                return adapters;
            }
        }

        public T TryResolve<T>(string name)
        {
            return CurrentAdapter.TryResolve<T>(name);
        }

        public T TryResolve<T>(IDictionary parameters)
        {
            return CurrentAdapter.TryResolve<T>(parameters);
        }

        public IContainerAdapter CurrentAdapter
        {
            get { return currentAdapter; }
            protected set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("adapter", "The current adapter was not located correctly.");
                }

                currentAdapter = value;
            }
        }

        public void Register<TRegisteredAs, TResolvedTo>(IRegistrationDefinition<TRegisteredAs, TResolvedTo> registrationDefinition) where TRegisteredAs : class where TResolvedTo : TRegisteredAs
        {
            INamedRegistrationDefinition namedRegistrationDefinition = registrationDefinition as INamedRegistrationDefinition;
            if (namedRegistrationDefinition != null)
            {
                CurrentAdapter.RegisterNamed(registrationDefinition, namedRegistrationDefinition.Name);
            }
            else
            {
                CurrentAdapter.Register(registrationDefinition);
            }
        }

        public static void AddAdapter(string name, IContainerAdapter adapter)
        {
            if (adapters.ContainsKey(name))
            {
                return;
            }

            adapters.Add(name, adapter);
        }

        public T Resolve<T>()
        {
            return CurrentAdapter.Resolve<T>();
        }

        public T TryResolve<T>()
        {
            return CurrentAdapter.TryResolve<T>();
        }

        public T Resolve<T>(string name)
        {
            return CurrentAdapter.Resolve<T>(name);
        }

        public T TryResolve<T>(string name, IDictionary arguments)
        {
            return CurrentAdapter.TryResolve<T>(name, arguments);
        }

        public T Resolve<T>(IDictionary arguments)
        {
            return CurrentAdapter.Resolve<T>(arguments);
        }

        public T Resolve<T>(string name, IDictionary arguments)
        {
            return CurrentAdapter.Resolve<T>(name, arguments);
        }

        protected IContainerAdapter GetAdapter(string adapterName)
        {
            return !Adapters.ContainsKey(adapterName) ? null : Adapters[adapterName];
        }

        protected void PopulateAdapters()
        {
            if (hasScanned || preventAssemblyScan)
            {
                return;
            }

            hasScanned = true;

            // go fishing ;)
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                PopulateAdaptersInAssembly(assembly);   
            }
        }

        private static void PopulateAdaptersInAssembly(Assembly assembly)
        {
            IEnumerable<Type> potentialAdapters;
            try
            {
                potentialAdapters = assembly.GetTypes().Where(IsPotentialAdapter);
            }
            catch
            {
                return;
            }

            foreach (Type potentialAdapter in potentialAdapters)
            {
                if (potentialAdapter.IsAbstract)
                {
                    continue;
                }

                IContainerAdapter adapter = Activator.CreateInstance(potentialAdapter) as IContainerAdapter;
                if (adapter == null)
                {
                    continue;
                }

                adapters.Add(adapter.Name, adapter);
            }
        }

        private static bool IsPotentialAdapter(Type type)
        {
            try
            {
                // todo: IsAssignableFrom doesn't work - need a better way.
                return type.GetInterface(typeof(IContainerAdapter).FullName.ToString(CultureInfo.InvariantCulture)) != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
