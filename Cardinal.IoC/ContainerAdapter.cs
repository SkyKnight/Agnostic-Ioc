using System;
using System.Collections;

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

        public TContainer Container { get; protected set; }

        protected void Initialize()
        {
            Setup();
        }

        public abstract void Setup();

        public abstract T Resolve<T>();

        public abstract T Resolve<T>(IDictionary arguments);

        public abstract T Resolve<T>(string name);

        public abstract T Resolve<T>(string name, IDictionary arguments);
    }
}
