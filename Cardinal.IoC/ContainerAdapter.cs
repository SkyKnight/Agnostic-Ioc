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

namespace Cardinal.IoC
{
    /// <summary>
    /// A basic container adapter
    /// </summary>
    /// <typeparam name="TContainer">The resulting container type</typeparam>
    public abstract class ContainerAdapter<TContainer> : IContainerAdapter<TContainer>
    {
        private string containerAdapterName;

        protected ContainerAdapter()
        {
            Initialize();
        }

        protected ContainerAdapter(string name)
        {
            Name = name;
            Initialize();
        }

        protected ContainerAdapter(string name, TContainer container)
        {
            Container = container;
            Initialize();
            Name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public virtual string Name
        {
            get { return containerAdapterName ?? String.Empty; }
            private set { containerAdapterName = value; }
        }

        public bool AllowSelfRegistration { get; protected set; }

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        public TContainer Container { get; protected set; }

        public void Register<TRegisteredAs, TResolvedTo>() where TRegisteredAs : class where TResolvedTo : TRegisteredAs
        {
            Register<TRegisteredAs, TResolvedTo>(LifetimeScope.Transient);
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
            Register<TRegisteredAs, TResolvedTo>(LifetimeScope.Transient, name);
        }

        public abstract void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope, string name) where TRegisteredAs : class where TResolvedTo : TRegisteredAs;

        public abstract void Register<TRegisteredAs>(TRegisteredAs instance)
            where TRegisteredAs : class;

        /// <summary>
        /// Registers a component using a simple instance registration definition
        /// </summary>
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
        public abstract void Register<TRegisteredAs>(string name, TRegisteredAs instance)
            where TRegisteredAs : class;

        public void RegisterGroup(IContainerManagerGroupRegistration groupRegistration)
        {
            groupRegistration.RegisterComponents(this);
        }

        public abstract object Resolve(Type t);

        public abstract object Resolve(Type t, string name);

        protected abstract void Register(Type componentType, object target, string name);

        protected abstract void Register(Type[] componentTypes, object target, string name);

        protected abstract void Register(Type componentType, Type targetType, LifetimeScope lifetimeScope, string name);
        
        protected abstract void Register(Type[] componentTypes, Type targetType, LifetimeScope lifetimeScope, string name);

        public void Register(IComponentRegistration registration)
        {
            if (registration.Definition.Types.Length == 1)
            {
                if (registration.Definition.Instance != null)
                {
                    Register(registration.Definition.Types.First(), registration.Definition.Instance, registration.Definition.Name);
                    return;
                }

                Register(registration.Definition.Types.First(), registration.Definition.ReturnType, registration.Definition.LifetimeScope, registration.Definition.Name);
                return;
            }

            if (registration.Definition.Instance != null)
            {
                Register(registration.Definition.Types, registration.Definition.Instance, registration.Definition.Name);
                return;
            }

            Register(registration.Definition.Types, registration.Definition.ReturnType, registration.Definition.LifetimeScope, registration.Definition.Name);
        }

        public virtual TComponentRegistrationType CreateComponentRegistration<TComponentRegistrationType>() where TComponentRegistrationType : IComponentRegistration, new()
        {
            return new TComponentRegistrationType();
        }

        /// <summary>
        /// Resolves all registrations of the given type
        /// </summary>
        /// <typeparam name="TResolvedTo"></typeparam>
        /// <returns></returns>
        public abstract IEnumerable<TResolvedTo> ResolveAll<TResolvedTo>();

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

        public object TryResolve(Type t)
        {
            try
            {
                return Resolve(t);
            }
            catch
            {
                return null;
            }
        }

        public object TryResolve(Type t, string name)
        {
            try
            {
                return Resolve(t, name);
            }
            catch
            {
                return null;
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
