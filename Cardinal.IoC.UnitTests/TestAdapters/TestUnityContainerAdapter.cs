using Cardinal.IoC.UnitTests.Helpers;
using Cardinal.IoC.Unity;
using Microsoft.Practices.Unity;

namespace Cardinal.IoC.UnitTests.TestAdapters
{
    public class TestUnityContainerAdapter : UnityContainerAdapter
    {
        public override string Name
        {
            get { return TestConstants.UnityContainerName; }
        }

        public override void RegisterComponents()
        {
            Container.RegisterType(typeof(IDependantClass), typeof (DependantClass2), null, new ContainerControlledLifetimeManager());

            Container.RegisterType(typeof(IDependantClass), typeof(DependantClass), "DependentClass2", new ContainerControlledLifetimeManager());
        }
    }
}
