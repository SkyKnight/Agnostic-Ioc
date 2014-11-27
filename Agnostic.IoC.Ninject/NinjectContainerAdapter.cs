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
using Agnostic.IoC.Registration;
using Ninject;
using Ninject.Parameters;

namespace Agnostic.IoC.Ninject
{
    public class NinjectContainerAdapter : ContainerAdapter<IKernel>
    {
        private const string DefaultBindingName = "";

        protected NinjectContainerAdapter() : this(new StandardKernel())
        {
        }

        public NinjectContainerAdapter(IKernel container) : this(String.Empty, container)
        {
        }

        public NinjectContainerAdapter(string name, IKernel container)
            : base(name, container)
        {
        }

        public override T Resolve<T>()
        {
            return Container.Get<T>(DefaultBindingName);
        }

        public override object Resolve(Type t)
        {
            return Container.Get(t, DefaultBindingName);
        }

        public override object Resolve(Type t, string name)
        {
            return Container.Get(t, name);
        }

        public override T Resolve<T>(string name)
        {
            return Container.Get<T>(name);
        }

        public override T Resolve<T>(IDictionary<string, object> arguments)
        {
            IParameter[] parameters = arguments.Select(x => new Parameter(x.Key, x.Value, false) as IParameter).ToArray();

            return Container.Get<T>(DefaultBindingName, parameters);
        }

        public override T Resolve<T>(string name, IDictionary<string, object> arguments)
        {
            IParameter[] parameters = arguments.Select(x => new Parameter(x.Key, x.Value, false) as IParameter).ToArray();

            return Container.Get<T>(name, parameters);
        }

        public override void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope)
        {
            if (TryResolve<TRegisteredAs>() != null)
            {
                Register<TRegisteredAs, TResolvedTo>(Guid.NewGuid().ToString());
                return;
            }

            Container.Bind<TRegisteredAs>().To<TResolvedTo>().SetLifeStyle(lifetimeScope).Named(DefaultBindingName);
        }

        public override void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope, string name)
        {
            Container.Bind<TRegisteredAs>().To<TResolvedTo>().SetLifeStyle(lifetimeScope).Named(name);
        }

        public override void Register<TRegisteredAs>(TRegisteredAs instance)
        {
            Container.Bind<TRegisteredAs>().ToMethod(x => instance).Named(DefaultBindingName);
        }

        public override void Register<TRegisteredAs>(string name, TRegisteredAs instance)
        {
            Container.Bind<TRegisteredAs>().ToMethod(x => instance).Named(name);
        }

        protected override void Register(Type componentType, object target, string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                name = DefaultBindingName;
            }

            Container.Bind(componentType).ToMethod(x => target).Named(name);
        }

        protected override void Register(Type[] componentTypes, object target, string name)
        {
            Container.Bind(componentTypes).ToMethod(x => target).Named(name);
        }

        protected override void Register(Type componentType, Type targetType, LifetimeScope lifetimeScope, string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                name = DefaultBindingName;
            }


            Container.Bind(componentType).To(targetType).SetLifeStyle(lifetimeScope).Named(name);
        }

        protected override void Register(Type[] componentTypes, Type targetType, LifetimeScope lifetimeScope, string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                name = DefaultBindingName;
            }

            Container.Bind(componentTypes).To(targetType).SetLifeStyle(lifetimeScope).Named(name);
        }

        public override IEnumerable<TResolvedTo> ResolveAll<TResolvedTo>()
        {
            return Container.GetAll<TResolvedTo>();
        }
    }
}
