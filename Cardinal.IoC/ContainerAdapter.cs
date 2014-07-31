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

namespace Cardinal.IoC
{
    /// <summary>
    /// A basic container adapter
    /// </summary>
    /// <typeparam name="TContainer">The resulting container type</typeparam>
    public abstract class ContainerAdapter<TContainer> : IContainerAdapter<TContainer>
    {
        protected ContainerAdapter()
        {
            Initialize();
        }

        protected ContainerAdapter(TContainer container)
        {
            Container = container;
            Initialize();
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public virtual string Name
        {
            get { return String.Empty; }
        }

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        public TContainer Container { get; protected set; }

        public void Register<TRegisteredAs, TResolvedTo>() where TRegisteredAs : class where TResolvedTo : TRegisteredAs
        {
            Register<TRegisteredAs, TResolvedTo>(LifetimeScope.Default);
        }

        /// <summary>
        /// Registers a component using a simple registration definition
        /// </summary>
        /// <param name="lifetimeScope">
        /// The lifetime scope.
        /// </param>
        /// <typeparam name="TRegisteredAs">
        /// The type to register as
        /// </typeparam>
        /// <typeparam name="TResolvedTo">
        /// The type it resolves to
        /// </typeparam>
        public abstract void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope) where TRegisteredAs : class where TResolvedTo : TRegisteredAs;

        public void Register<TRegisteredAs, TResolvedTo>(string name) where TRegisteredAs : class where TResolvedTo : TRegisteredAs
        {
            Register<TRegisteredAs, TResolvedTo>(LifetimeScope.Default, name);
        }

        /// <summary>
        /// Registers a component using a simple named registration definition
        /// </summary>
        /// <param name="lifetimeScope">
        /// The lifetime scope.
        /// </param>
        /// <param name="name">
        /// The name
        /// </param>
        /// <typeparam name="TRegisteredAs">
        /// The type to register as
        /// </typeparam>
        /// <typeparam name="TResolvedTo">
        /// The type it resolves to
        /// </typeparam>
        public abstract void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope, string name)
            where TRegisteredAs : class
            where TResolvedTo : TRegisteredAs;

        public void Register<TRegisteredAs, TResolvedTo>(TResolvedTo instance) where TRegisteredAs : class where TResolvedTo : TRegisteredAs
        {
            Register<TRegisteredAs, TResolvedTo>(LifetimeScope.Default, instance);
        }

        /// <summary>
        /// Registers a component using a simple instance registration definition
        /// </summary>
        /// <param name="lifetimeScope">
        /// The lifetime scope.
        /// </param>
        /// <param name="instance">
        /// The instance to resolve as
        /// </param>
        /// <typeparam name="TRegisteredAs">
        /// The type to register as
        /// </typeparam>
        /// <typeparam name="TResolvedTo">
        /// The type it resolves to
        /// </typeparam>
        public abstract void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope, TResolvedTo instance)
            where TRegisteredAs : class
            where TResolvedTo : TRegisteredAs;

        public void Register<TRegisteredAs, TResolvedTo>(string name, TResolvedTo instance) where TRegisteredAs : class where TResolvedTo : TRegisteredAs
        {
            Register<TRegisteredAs, TResolvedTo>(LifetimeScope.Default, name, instance);
        }

        /// <summary>
        /// Registers a component using a simple name and instance registration definition
        /// </summary>
        /// <param name="lifetimeScope">
        /// The lifetime scope.
        /// </param>
        /// <param name="name">
        /// The name
        /// </param>
        /// <param name="instance">
        /// The instance to resolve as
        /// </param>
        /// <typeparam name="TRegisteredAs">
        /// The type to register as
        /// </typeparam>
        /// <typeparam name="TResolvedTo">
        /// The type it resolves to
        /// </typeparam>
        public abstract void Register<TRegisteredAs, TResolvedTo>(
            LifetimeScope lifetimeScope, 
            string name,
            TResolvedTo instance) where TRegisteredAs : class where TResolvedTo : TRegisteredAs;

        public void Register(IContainerManagerGroupRegistration groupRegistration)
        {
            groupRegistration.RegisterComponents(this);
        }

        /// <summary>
        /// Use this method to register components if you are creating the container
        /// </summary>
        public virtual void RegisterComponents()
        {
        }

        /// <summary>
        /// Attempts to resolve the requested type with arguments
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        /// <typeparam name="T">
        /// The requested type
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T TryResolve<T>(string name, IDictionary<string, object> arguments)
        {
            try
            {
                return Resolve<T>(name, arguments);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Attempts to resolve the requested type
        /// </summary>
        /// <typeparam name="T">
        /// The requested type
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T TryResolve<T>()
        {
            try
            {
                return Resolve<T>();
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Attempts to resolve the requested type by name
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <typeparam name="T">
        /// The requested type
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T TryResolve<T>(string name)
        {
            try
            {
                return Resolve<T>(name);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Attempts to resolve the requested type with arguments
        /// </summary>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        /// <typeparam name="T">
        /// The requested type
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T TryResolve<T>(IDictionary<string, object> arguments)
        {
            try
            {
                return Resolve<T>(arguments);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Attempts to resolve the requested type
        /// </summary>
        /// <typeparam name="T">
        /// The requested type
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public abstract T Resolve<T>();

        /// <summary>
        /// Attempts to resolve the requested type with arguments
        /// </summary>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        /// <typeparam name="T">
        /// The requested type
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public abstract T Resolve<T>(IDictionary<string, object> arguments);

        /// <summary>
        /// Attempts to resolve the requested type by name
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <typeparam name="T">
        /// The requested type
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public abstract T Resolve<T>(string name);
        
        /// <summary>
        /// Attempts to resolve the requested type by name with arguments
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        /// <typeparam name="T">
        /// The requested type
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public abstract T Resolve<T>(string name, IDictionary<string, object> arguments);

        /// <summary>
        /// Initializes the Container Manager
        /// </summary>
        protected void Initialize()
        {
            InitializeAdapter();
            RegisterComponents();
            InitializeContainer();
        }

        /// <summary>
        /// Initializes the Container
        /// </summary>
        protected virtual void InitializeContainer()
        {
        }

        /// <summary>
        /// Initializes the Container Adapter if required
        /// </summary>
        protected virtual void InitializeAdapter()
        {
        }
    }
}
