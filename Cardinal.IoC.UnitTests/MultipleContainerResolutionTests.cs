using System;
using NUnit.Framework;

namespace Cardinal.IoC.UnitTests
{
    [TestFixture]
    public class MultipleContainerResolutionTests
    {
        [Test]
        public void ResolveItemByInterfaceOnly()
        {
            ContainerManager containerManager = new ContainerManager();
            IDependantClass dependency = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass), dependency.GetType());

            ContainerManager unityContainerManager = new ContainerManager(TestConstants.UnityContainerName);
            IDependantClass unityDependency = unityContainerManager.Resolve<IDependantClass>();
            Assert.AreEqual(typeof(DependantClass2), unityDependency.GetType());
        }
    }
}
