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
using Cardinal.IoC.Registration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Cardinal.IoC.Windsor
{
    public class WindsorContainerAdapter : ContainerAdapter<IWindsorContainer>
    {
        protected WindsorContainerAdapter() : this(new WindsorContainer())
        {
        }

        public WindsorContainerAdapter(IWindsorContainer container) : this(String.Empty, container)
        {
        }

        public WindsorContainerAdapter(string name, IWindsorContainer container)
            : base(name, container)
        {
        }

        public override T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public override T Resolve<T>(string name)
        {
            return Container.Resolve<T>(name);
        }

        public override T Resolve<T>(IDictionary<string, object> arguments)
        {
            return Container.Resolve<T>(arguments);
        }

        public override T Resolve<T>(string name, IDictionary<string, object> arguments)
        {
            return Container.Resolve<T>(name, arguments);
        }

        public override void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope)
        {
            Container.Register(Component.For<TRegisteredAs>().ImplementedBy<TResolvedTo>());
        }

        public override void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope, string name)
        {
            Container.Register(Component.For<TRegisteredAs>().ImplementedBy<TResolvedTo>().Named(name));
        }

        public override void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope, TResolvedTo instance)
        {
            Container.Register(Component.For<TRegisteredAs>().Instance(instance));
        }

        public override void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope, string name, TResolvedTo instance)
        {
            Container.Register(SetLifeStyle(Component.For<TRegisteredAs>().Instance(instance).Named(name), lifetimeScope));
        }

        public override void RegisterAll<TRegisteredAs>()
        {
            Container.Register(Classes.FromAssemblyContaining<TRegisteredAs>().BasedOn<TRegisteredAs>().WithServiceAllInterfaces());
        }

        public override void RegisterAll<TRegisteredAs>(string assemblyName)
        {
            Container.Register(Classes.FromAssemblyNamed(assemblyName).BasedOn<TRegisteredAs>().WithServiceAllInterfaces());
        }

        public override IEnumerable<TResolvedTo> ResolveAll<TResolvedTo>()
        {
            return Container.ResolveAll<TResolvedTo>();
        }

        public ComponentRegistration<T> SetLifeStyle<T>(ComponentRegistration<T> registration, LifetimeScope lifeTimeKey) where T : class
        {
            switch (lifeTimeKey)
            {
                default:
                    return registration.LifestyleTransient();
            }
        }
    }
}
