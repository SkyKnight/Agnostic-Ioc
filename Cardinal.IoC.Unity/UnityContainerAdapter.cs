using System;
using System.Collections;
using Microsoft.Practices.Unity;

namespace Cardinal.IoC.Unity
{
    public abstract class UnityContainerAdapter : ContainerAdapter<IUnityContainer>
    {
        protected UnityContainerAdapter() : this(new UnityContainer())
        {
        }

        protected UnityContainerAdapter(IUnityContainer container) : base(container)
        {
        }

        public override T Resolve<T>(string name)
        {
            return Container.Resolve<T>(name);
        }

        public override T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public override T Resolve<T>(string name, IDictionary arguments)
        {
            throw new NotImplementedException();
        }

        public override T Resolve<T>(IDictionary arguments)
        {
            throw new NotImplementedException();
        }
    }
}
