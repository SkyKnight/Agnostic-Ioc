using System;
using Castle.Windsor;

namespace Cardinal.IoC.Windsor
{
    public abstract class WindsorContainerAdapter : ContainerAdapter<IWindsorContainer>
    {
        private static readonly IWindsorContainer container = new WindsorContainer();

        public override T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public override T Resolve<T>(string name)
        {
            return container.Resolve<T>(name);
        }

        public override IWindsorContainer Container 
        {
            get { return container; }
        }
    }
}
