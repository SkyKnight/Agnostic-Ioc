using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Cardinal.IoC
{
    public class ContainerManager : IContainerManager
    {
        private static Dictionary<string, IContainerAdapter> adapters;

        private IContainerAdapter currentAdapter;

        public ContainerManager() : this(String.Empty)
        {
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

        protected IContainerAdapter CurrentAdapter
        {
            get { return currentAdapter; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("adapter", "The current adapter was not located correctly.");
                }

                currentAdapter = value;
            }
        }

        public T Resolve<T>()
        {
            return CurrentAdapter.Resolve<T>();
        }

        public T Resolve<T>(string name)
        {
            return CurrentAdapter.Resolve<T>(name);
        }

        protected IContainerAdapter GetAdapter(string adapterName)
        {
            return !Adapters.ContainsKey(adapterName) ? null : Adapters[adapterName];
        }

        protected void PopulateAdapters()
        {
            if (adapters != null)
            {
                return;
            }

            adapters = new Dictionary<string, IContainerAdapter>();

            // go fishing ;)
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                PopulateAdaptersInAssembly(assembly);   
            }
        }

        private static void PopulateAdaptersInAssembly(Assembly assembly)
        {
            // todo: IsAssignableFrom doesn't work - need a better way.
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
                return type.GetInterface(typeof(IContainerAdapter).FullName.ToString(CultureInfo.InvariantCulture)) != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
