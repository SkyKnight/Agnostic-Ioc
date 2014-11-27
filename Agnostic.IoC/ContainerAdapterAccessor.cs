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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Agnostic.IoC
{
    public class ContainerAdapterAccessor : IContainerAdapterAccessor
    {
        private static bool hasScanned;

        private static Dictionary<string, IContainerAdapter> adapters = new Dictionary<string, IContainerAdapter>();

        public ContainerAdapterAccessor()
        {
            
        }

        public ContainerAdapterAccessor(bool scanAssemblies)
        {
            ScanAssemblies = scanAssemblies;
        }

        public bool ScanAssemblies { get; set; }

        public IContainerAdapter GetAdapter(string key)
        {
            if (adapters.ContainsKey(key))
            {
                return adapters[key];
            }

            if (ScanAssemblies)
            {
                PopulateAdapters();
            }

            return adapters.ContainsKey(key) ? adapters[key] : null;
        }

        public static void Clear()
        {
            adapters = new Dictionary<string, IContainerAdapter>();
            hasScanned = false;
        }

        /// <summary>
        /// Adds an adapter for serving
        /// </summary>
        /// <param name="key">The adapter key</param>
        /// <param name="adapter">The adapter</param>
        public void AddAdapter(string key, IContainerAdapter adapter)
        {
            if (adapters.ContainsKey(key))
            {
                throw new InvalidDataException("An adapter has already been registered with this key");
            }

            adapters.Add(key, adapter);
        }

        public void PopulateAdapters()
        {
            if (hasScanned)
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

        private void PopulateAdaptersInAssembly(Assembly assembly)
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
                ConstructorInfo constructor = potentialAdapter.GetConstructor(Type.EmptyTypes);
                if (potentialAdapter.IsAbstract || constructor == null)
                {
                    continue;
                }

                IContainerAdapter adapter = Activator.CreateInstance(potentialAdapter) as IContainerAdapter;

                if (adapter == null || !adapter.AllowSelfRegistration)
                {
                    continue;
                }

                AddAdapter(adapter.Name, adapter);
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
