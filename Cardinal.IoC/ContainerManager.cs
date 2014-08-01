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
        private IContainerAdapter currentAdapter;

        public ContainerManager() : this(String.Empty)
        {
        }

        public ContainerManager(IContainerAdapterFactory adapterFactory)
        {
            AdapterFactory = adapterFactory;
        }

        public ContainerManager(IContainerAdapter containerAdapter) : this(containerAdapter, new ContainerAdapterFactory())
        {
        }

        public ContainerManager(IContainerAdapter containerAdapter, IContainerAdapterFactory adapterFactory) : this(adapterFactory)
        {
            adapterFactory.AddAdapter(containerAdapter.Name, containerAdapter);
            CurrentAdapter = containerAdapter; 
        }

        public ContainerManager(IContainerManager masterContainerManager) : this(masterContainerManager.CurrentAdapter)
        {
            
        }

        public ContainerManager(string adapterName) : this(adapterName, new ContainerAdapterFactory())
        {
        }

        public ContainerManager(string adapterName, IContainerAdapterFactory adapterFactory) : this(adapterFactory)
        {
            CurrentAdapter = GetAdapter(adapterName);
        }

        protected IContainerAdapterFactory AdapterFactory { get; private set; }

        public T TryResolve<T>(string name)
        {
            return CurrentAdapter.TryResolve<T>(name);
        }

        public T TryResolve<T>(IDictionary<string, object> parameters)
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

        public IEnumerable<T> ResolveAll<T>()
        {
            return CurrentAdapter.ResolveAll<T>();
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

        public T TryResolve<T>(string name, IDictionary<string, object> arguments)
        {
            return CurrentAdapter.TryResolve<T>(name, arguments);
        }

        public T Resolve<T>(IDictionary<string, object> arguments)
        {
            return CurrentAdapter.Resolve<T>(arguments);
        }

        public T Resolve<T>(string name, IDictionary<string, object> arguments)
        {
            return CurrentAdapter.Resolve<T>(name, arguments);
        }

        protected IContainerAdapter GetAdapter(string adapterName)
        {
            return AdapterFactory.GetAdapter(adapterName);
        }
    }
}
