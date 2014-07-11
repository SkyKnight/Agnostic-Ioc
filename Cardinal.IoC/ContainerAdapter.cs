using System;

namespace Cardinal.IoC
{
    public abstract class ContainerAdapter<TContainer> : IContainerAdapter<TContainer>, IContainerAdapter
    {
        protected ContainerAdapter()
        {
            Initialize();
        }

        public virtual string Name
        {
            get { return String.Empty; }
        }

        public abstract TContainer Container { get; }

        protected void Initialize()
        {
            Setup();
        }

        public abstract void Setup();

        public abstract T Resolve<T>();

        public abstract T Resolve<T>(string name);
    }
}
