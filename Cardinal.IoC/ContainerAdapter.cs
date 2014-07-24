using System;
using System.Collections;
using Cardinal.IoC.Registration;

namespace Cardinal.IoC
{
    public abstract class ContainerAdapter<TContainer> : IContainerAdapter<TContainer>
    {
        protected ContainerAdapter()
        {
            
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

        public abstract void Register<TRegisteredAs, TResolvedTo>(IRegistrationDefinition<TRegisteredAs, TResolvedTo> registrationDefinition) where TRegisteredAs : class where TResolvedTo : TRegisteredAs;
        
        public TContainer Container { get; protected set; }

        protected void Initialize()
        {
            Setup();
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

        public abstract void Setup();

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
