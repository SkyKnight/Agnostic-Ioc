using System;
using System.Collections;
using Cardinal.IoC.Registration;

namespace Cardinal.IoC
{
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

        public virtual string Name
        {
            get { return String.Empty; }
        }

        public TContainer Container { get; protected set; }

        public abstract void Register<TRegisteredAs, TResolvedTo>(IRegistrationDefinition<TRegisteredAs, TResolvedTo> registrationDefinition) where TRegisteredAs : class where TResolvedTo : TRegisteredAs;

        public abstract void Register<TRegisteredAs, TResolvedTo>(
            IRegistrationDefinition<TRegisteredAs, TResolvedTo> registrationDefinition, string name)
            where TRegisteredAs : class
            where TResolvedTo : TRegisteredAs;

        public abstract void Register<TRegisteredAs, TResolvedTo>(
            IRegistrationDefinition<TRegisteredAs, TResolvedTo> registrationDefinition, TResolvedTo instance)
            where TRegisteredAs : class
            where TResolvedTo : TRegisteredAs;

        public abstract void Register<TRegisteredAs, TResolvedTo>(
            IRegistrationDefinition<TRegisteredAs, TResolvedTo> registrationDefinition, string name,
            TResolvedTo instance) where TRegisteredAs : class where TResolvedTo : TRegisteredAs;

        public virtual void RegisterComponents()
        {
        }

        protected void Initialize()
        {
            InitializeAdapter();
            RegisterComponents();
            InitializeContainer();
        }

        protected virtual void InitializeContainer()
        {
        }

        protected virtual void InitializeAdapter()
        {
        }

        public T TryResolve<T>(string name, IDictionary arguments)
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

        public abstract T Resolve<T>();

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

        public abstract T Resolve<T>(IDictionary arguments);

        public T TryResolve<T>(IDictionary arguments)
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

        public abstract T Resolve<T>(string name);

        public abstract T Resolve<T>(string name, IDictionary arguments);
    }
}
