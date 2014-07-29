using Cardinal.IoC.UnitTests.Helpers;
using Cardinal.IoC.Windsor;
using Castle.MicroKernel.Registration;

namespace Cardinal.IoC.UnitTests.TestAdapters
{
    public class TestWindsorContainerAdapter : WindsorContainerAdapter
    {
        public override void RegisterComponents()
        {
            Container.Register(Component.For<IDependantClass>().ImplementedBy<DependantClass>());

            Container.Register(Component.For<IDependantClass>().ImplementedBy<DependantClass2>().Named("DependentClass2"));
        }
    }
}
