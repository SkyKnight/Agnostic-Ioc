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
using System.Collections;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using Cardinal.IoC;
using Cardinal.IoC.Registration;

namespace Cardinal.Ioc.Autofac
{
    /// <summary>
    /// The Autofac container adapter.
    /// </summary>
    public class AutofacContainerAdapter : ContainerAdapter<IContainer>
    {
        protected ContainerBuilder Builder { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacContainerAdapter"/> class.
        /// </summary>
        protected AutofacContainerAdapter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacContainerAdapter"/> class with an existing container.
        /// Allows the API that uses the IOC abstraction layer to use something configured elsewhere. If this is done, 
        /// the RegisterComponents method is not  called. Any additional work must be done by separate Register calls 
        /// which in turn modify the container via an Update call. Though this is not recommended.
        /// </summary>
        /// <param name="container">The existing container</param>
        public AutofacContainerAdapter(IContainer container)
            : base(container)
        {
        }

        protected override void InitializeAdapter()
        {
            base.InitializeAdapter();
            if (Container != null)
            {
                return;
            }

            Builder = new ContainerBuilder();
        }

        protected override void InitializeContainer()
        {
            base.InitializeContainer();
            if (Container != null)
            {
                return;
            }

            Container = Builder.Build();
        }

        /// <summary>
        /// Resolves a component by type definition.
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <returns>The registered component</returns>
        public override T Resolve<T>()
        {
            if (!Container.IsRegistered<T>())
            {
                throw new ArgumentException("Component not registered");
            }
            return Container.Resolve<T>();
        }

        /// <summary>
        /// Resolves a component by type definition and name
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <param name="name">The name of the component registration</param>
        /// <returns>The registered component</returns>
        public override T Resolve<T>(string name)
        {
            if (!Container.IsRegisteredWithName<T>(name))
            {
                throw new ArgumentException("Component not registered with provided name", name);
            }

            return Container.ResolveNamed<T>(name);
        }

        /// <summary>
        /// Resolves a component by type definition and constructor with the arguments matching those provided.
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <param name="arguments">The constructor arguments</param>
        /// <returns>The registered component</returns>
        public override T Resolve<T>(IDictionary<string, object> arguments)
        {
            if (!Container.IsRegistered<T>())
            {
                throw new ArgumentException("Component not registered");
            }

            Parameter[] array = GetParametersFromDictionary(arguments);

            return Container.Resolve<T>(array);
        }

        /// <summary>
        /// Resolves a named component by type definition and constructor with the arguments matching those provided.
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <param name="name">The name of the component registration</param>
        /// <param name="arguments">The constructor arguments</param>
        /// <returns>The registered component</returns>
        public override T Resolve<T>(string name, IDictionary<string, object> arguments)
        {
            if (!Container.IsRegisteredWithName<T>(name))
            {
                throw new ArgumentException("Component not registered with provided name", name);
            }

            Parameter[] array = GetParametersFromDictionary(arguments);

            return Container.ResolveNamed<T>(name, array);
        }

        /// <summary>
        /// Add a late bound registration. Where possible use <see cref="RegisterComponents(Autofac.ContainerBuilder)"/> instead
        /// as this is done on startup rather than during application execution.
        /// </summary>
        /// <typeparam name="TRegisteredAs">The type definition of the component to register</typeparam>
        /// <typeparam name="TResolvedTo">How the type is resolved in the container</typeparam>
        /// <param name="registrationDefinition">The registration definition providing further options</param>
        public override void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope)
        {
            // Http is the default for Autofac ASP.NET integrations. Transient per dependency otherwise.
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<TResolvedTo>().As<TRegisteredAs>();
            builder.Update(Container);
        }

        public override void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope, string name)
        {
            // Http is the default for Autofac ASP.NET integrations. Instance per dependency otherwise.
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<TResolvedTo>().As<TRegisteredAs>();
            builder.Update(Container);
        }

        public override void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope, TResolvedTo instance)
        {
            // Http is the default for Autofac ASP.NET integrations. Instance per dependency otherwise.
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<TResolvedTo>().As<TRegisteredAs>();
            builder.Update(Container);
        }

        public override void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope, string name, TResolvedTo instance)
        {
            // Http is the default for Autofac ASP.NET integrations. Instance per dependency otherwise.
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<TResolvedTo>().Named<TRegisteredAs>(name);
            builder.Update(Container);
        }

        /// <summary>
        /// Converts the generic IDictionary to an array of named parameters for the container to
        /// use in constructor resolution. See <see cref="NamedParameter"/>.
        /// </summary>
        /// <param name="arguments">The dictionary of arguments</param>
        /// <returns>The named parameter array</returns>
        internal static Parameter[] GetParametersFromDictionary(IDictionary<string, object> arguments)
        {
            List<Parameter> parameters = new List<Parameter>(arguments.Keys.Count);

            foreach (string key in arguments.Keys)
            {
                NamedParameter p = new NamedParameter(key, arguments[key]);
                parameters.Add(p);
            }

            return parameters.ToArray();
        }
    }
}