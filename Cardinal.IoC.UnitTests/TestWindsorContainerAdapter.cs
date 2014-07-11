using Cardinal.IoC.Windsor;
using Castle.MicroKernel.Registration;

namespace Cardinal.IoC.UnitTests
{
    public class TestWindsorContainerAdapter : WindsorContainerAdapter
    {
        public override void Setup()
        {
            Container.Register(Component.For<IDependantClass>().ImplementedBy<DependantClass>());

            Container.Register(Component.For<IDependantClass>().ImplementedBy<DependantClass>().Named("DependentClass2"));
        }
    }
}
