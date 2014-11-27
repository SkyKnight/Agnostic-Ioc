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
using Agnostic.IoC.Registration;
using StructureMap;
using StructureMap.Pipeline;

namespace Agnostic.IoC.StructureMap
{
    public class StructureMapContainerAdapter : ContainerAdapter<IContainer>
    {
        protected StructureMapContainerAdapter()
            : this(new Container())
        {
        }

        public StructureMapContainerAdapter(IContainer container)
            : this(String.Empty, container)
        {
        }

        public StructureMapContainerAdapter(string name, IContainer container)
            : base(name, container)
        {
        }

        public override T Resolve<T>()
        {
            return Container.GetInstance<T>();
        }

        public override T Resolve<T>(string name)
        {
            return Container.GetInstance<T>(name);
        }

        public override T Resolve<T>(IDictionary<string, object> arguments)
        {
            ExplicitArguments explicitArguments = new ExplicitArguments(arguments);
            return Container.GetInstance<T>(explicitArguments);
        }

        public override T Resolve<T>(string name, IDictionary<string, object> arguments)
        {
            ExplicitArguments explicitArguments = new ExplicitArguments(arguments);
            return Container.GetInstance<T>(explicitArguments, name);
        }

        public override void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope)
        {
            if (TryResolve<TRegisteredAs>() != null)
            {
                Container.Configure(x => x.For<TRegisteredAs>().Add<TResolvedTo>().SetLifeStyle(lifetimeScope));
                return;
            }

            Container.Configure(x => x.For<TRegisteredAs>().Use<TResolvedTo>().SetLifeStyle(lifetimeScope));
        }

        public override void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope, string name)
        {
            if (TryResolve<TRegisteredAs>() != null)
            {
                Container.Configure(x => x.For<TRegisteredAs>().Add<TResolvedTo>().Named(name).SetLifeStyle(lifetimeScope));
                return;
            }

            Container.Configure(x => x.For<TRegisteredAs>().Use<TResolvedTo>().Named(name).SetLifeStyle(lifetimeScope));
        }

        public override void Register<TRegisteredAs>(TRegisteredAs instance)
        {
            if (TryResolve<TRegisteredAs>() != null)
            {
                Container.Configure(x => x.For<TRegisteredAs>().AddInstance(new ObjectInstance(instance)));
                return;
            }

            Container.Configure(x => x.For<TRegisteredAs>().UseInstance(new ObjectInstance(instance)));
        }

        public override void Register<TRegisteredAs>(string name, TRegisteredAs instance)
        {
            if (TryResolve<TRegisteredAs>() != null)
            {
                Container.Configure(x => x.For<TRegisteredAs>().AddInstance(new ObjectInstance(instance).Named(name)));
                return;
            }

            Container.Configure(x => x.For<TRegisteredAs>().UseInstance(new ObjectInstance(instance).Named(name)));
        }

        public override object Resolve(Type t)
        {
            return Container.GetInstance(t);
        }

        public override object Resolve(Type t, string name)
        {
            return Container.GetInstance(t, name);
        }

        protected override void Register(Type componentType, object target, string name)
        {
            if (TryResolve(componentType) != null)
            {
                Container.Configure(x => x.For(componentType).Add(target));
                return;
            }

            Container.Configure(x => x.For(componentType).Use(target));
        }

        protected override void Register(Type[] componentTypes, object target, string name)
        {
            foreach (var componentType in componentTypes)
            {
                Register(componentType, target, name);
            }
        }

        protected override void Register(Type componentType, Type targetType, LifetimeScope lifetimeScope, string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                Container.Configure(x => x.For(componentType).Add(targetType).Named(name).SetLifeStyle(lifetimeScope));
                return;
            }

            Container.Configure(x => x.For(componentType).Use(targetType).SetLifeStyle(lifetimeScope));
        }

        protected override void Register(Type[] componentTypes, Type targetType, LifetimeScope lifetimeScope, string name)
        {
            foreach (var componentType in componentTypes)
            {
                Register(componentType, targetType, lifetimeScope, name);
            }
        }

        public override IEnumerable<TResolvedTo> ResolveAll<TResolvedTo>()
        {
            return Container.GetAllInstances<TResolvedTo>();
        }
    }
}
