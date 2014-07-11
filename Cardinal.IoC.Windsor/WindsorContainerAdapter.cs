using Castle.Windsor;

namespace Cardinal.IoC.Windsor
{
    public abstract class WindsorContainerAdapter : ContainerAdapter<IWindsorContainer>
    {
        protected WindsorContainerAdapter() : this(new WindsorContainer())
        {
            
        }

        protected WindsorContainerAdapter(IWindsorContainer container) : base(container)
        {
        }

        public override T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public override T Resolve<T>(string name)
        {
            return Container.Resolve<T>(name);
        }
    }
}
