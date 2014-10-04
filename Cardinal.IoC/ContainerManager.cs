// --------------------------------------------------------------------------------------------------------------------
// Copyright (c) 2014, Simon Proctor and Nathanael Mann
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Cardinal.IoC
{
    public class ContainerManager : IContainerManager
    {
        private IContainerAdapter adapter;

        /// <summary>
        /// Default constructor, always self scans.
        /// </summary>
        public ContainerManager()
            : this(String.Empty, true)
        {
        }

        /// <summary>
        /// Container adapter constructor,  Never Self Scans
        /// </summary>
        /// <param name="containerAdapter">The container adapter</param>
        public ContainerManager(IContainerAdapter containerAdapter) : this(containerAdapter, new ContainerAdapterAccessor())
        {
        }

        /// <summary>
        /// Container adapter & accessor constructor,  Never Self Scans
        /// </summary>
        /// <param name="containerAdapter">The container adapter</param>
        /// <param name="adapterAccessor">The adapter accessor</param>
        public ContainerManager(IContainerAdapter containerAdapter, IContainerAdapterAccessor adapterAccessor)
        {
            AdapterAccessor = adapterAccessor;
            AdapterAccessor.AddAdapter(containerAdapter.Name, containerAdapter);
            Adapter = containerAdapter; 
        }

        /// <summary>
        /// String key constructor, always self scans
        /// </summary>
        /// <param name="adapterName">The adapter name</param>
        public ContainerManager(string adapterName) : this(adapterName, true)
        {
            
        }

        /// <summary>
        /// String key constructor, optional self scan
        /// </summary>
        /// <param name="adapterName">The adapter name</param>
        /// <param name="scanAssemblies">Whether to scan the assemblies</param>
        public ContainerManager(string adapterName, bool scanAssemblies)
            : this(adapterName, new ContainerAdapterAccessor(scanAssemblies))
        {
        }

        public ContainerManager(string adapterName, IContainerAdapterAccessor adapterAccessor)
        {
            AdapterAccessor = adapterAccessor;
            Adapter = AdapterAccessor.GetAdapter(adapterName); ;
        }

        /// <summary>
        /// Gets the adapter accessor
        /// </summary>
        internal IContainerAdapterAccessor AdapterAccessor { get; private set; }
        
        /// <summary>
        /// Attempts to resolve a component
        /// </summary>
        /// <typeparam name="T">The component type</typeparam>
        /// <param name="name">The name</param>
        /// <returns></returns>
        public T TryResolve<T>(string name)
        {
            return Adapter.TryResolve<T>(name);
        }

        public T TryResolve<T>(IDictionary<string, object> parameters)
        {
            return Adapter.TryResolve<T>(parameters);
        }

        public object Resolve(Type t)
        {
            return Adapter.Resolve(t);
        }

        public object TryResolve(Type t)
        {
            return Adapter.TryResolve(t);
        }

        public object Resolve(Type t, string name)
        {
            return Adapter.Resolve(t, name);
        }

        public object TryResolve(Type t, string name)
        {
            return Adapter.TryResolve(t, name);
        }

        public IContainerAdapter Adapter
        {
            get { return adapter; }
            protected set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("adapter", "The current adapter was not located correctly.");
                }

                adapter = value;
            }
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return Adapter.ResolveAll<T>();
        }

        public T Resolve<T>()
        {
            return Adapter.Resolve<T>();
        }

        public T TryResolve<T>()
        {
            return Adapter.TryResolve<T>();
        }

        public T Resolve<T>(string name)
        {
            return Adapter.Resolve<T>(name);
        }

        public T TryResolve<T>(string name, IDictionary<string, object> arguments)
        {
            return Adapter.TryResolve<T>(name, arguments);
        }

        public T Resolve<T>(IDictionary<string, object> arguments)
        {
            return Adapter.Resolve<T>(arguments);
        }

        public T Resolve<T>(string name, IDictionary<string, object> arguments)
        {
            return Adapter.Resolve<T>(name, arguments);
        }

        protected IContainerAdapter GetAdapter(string adapterName)
        {
            return AdapterAccessor.GetAdapter(adapterName);
        }
    }
}
