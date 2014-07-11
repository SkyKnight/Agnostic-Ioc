using System;
using NUnit.Framework;

namespace Cardinal.IoC.UnitTests
{
    [TestFixture]
    public class BasicResolutionTests
    {
        [Test]
        public void ResolveItemByInterfaceOnly()
        {
            ContainerManager containerManager = new ContainerManager();
            IDependantClass dependency = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependency);
        }

        [Test]
        public void ResolveItemByName()
        {
            ContainerManager containerManager = new ContainerManager();
            IDependantClass dependency = containerManager.Resolve<IDependantClass>("DependentClass2");
            Assert.IsNotNull(dependency);
        }

        [Test]
        public void ResolveItemWithParameters()
        {
            throw new NotImplementedException("Implement This");
        }

        [Test]
        public void LocateIndependentServices()
        {
            throw new NotImplementedException("Implement This");
        }
    }
}
