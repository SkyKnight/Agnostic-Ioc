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
using System.Linq;
using Cardinal.IoC.Registration;
using Microsoft.Practices.Unity;

namespace Cardinal.IoC.Unity
{
    public class UnityContainerAdapter : ContainerAdapter<IUnityContainer>
    {
        protected UnityContainerAdapter() : this(new UnityContainer())
        {
        }

        public UnityContainerAdapter(IUnityContainer container) : this(String.Empty, container)
        {
        }

        public UnityContainerAdapter(string name, IUnityContainer container)
            : base(name, container)
        {
        }

        public override T Resolve<T>(string name)
        {
            return Container.Resolve<T>(name);
        }

        public override T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public override T Resolve<T>(string name, IDictionary<string, object> arguments)
        {
            ParameterOverrides resolverOverride = GetParametersOverrideFromDictionary<T>(arguments);
            return Container.Resolve<T>(name, resolverOverride);
        }

        public override T Resolve<T>(IDictionary<string, object> arguments)
        {
            ParameterOverrides resolverOverride = GetParametersOverrideFromDictionary<T>(arguments);
            return Container.Resolve<T>(resolverOverride);
        }

        public override void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope)
        {
            Container.RegisterType<TRegisteredAs, TResolvedTo>(GetLifetimeManager(lifetimeScope));
        }

        public override void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope, string name)
        {
            Container.RegisterType<TRegisteredAs, TResolvedTo>(name, GetLifetimeManager(lifetimeScope));
        }

        public override void Register<TRegisteredAs, TResolvedTo>(TResolvedTo instance)
        {
            Container.RegisterInstance<TRegisteredAs>(instance);
        }

        public override void Register<TRegisteredAs, TResolvedTo>(string name, TResolvedTo instance)
        {
            Container.RegisterInstance<TRegisteredAs>(name, instance);
        }

        public override IEnumerable<TResolvedTo> ResolveAll<TResolvedTo>()
        {
            return Container.ResolveAll<TResolvedTo>();
        }

        private LifetimeManager GetLifetimeManager(LifetimeScope lifetimeScope)
        {
            switch (lifetimeScope)
            {
                case LifetimeScope.Unowned:
                    return new TransientLifetimeManager();
                case LifetimeScope.Singleton:
                    // todo: check this
                    return new ContainerControlledLifetimeManager();
                case LifetimeScope.PerHttpRequest:
                    return new TransientLifetimeManager();
                case LifetimeScope.PerThread:
                    return new  PerThreadLifetimeManager();
                default:
                    return new TransientLifetimeManager();
            }
        }

        private static ParameterOverrides GetParametersOverrideFromDictionary<T>(IDictionary<string, object> arguments)
        {
            ParameterOverrides resolverOverride = new ParameterOverrides();
            foreach (string key in arguments.Keys)
            {
                resolverOverride.Add(key, arguments[key]);
            }
            return resolverOverride;
        }
    }
}
