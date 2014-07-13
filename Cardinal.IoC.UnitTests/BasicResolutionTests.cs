using System;
using Cardinal.IoC.UnitTests.Helpers;
using NUnit.Framework;

namespace Cardinal.IoC.UnitTests
{
    [TestFixture]
    public class BasicResolutionTests
    {
        [Test]
        public void GetContainerFromFactory()
        {
            var containerManager = ContainerManagerFactory.GetContainerManager();
            Assert.AreEqual(String.Empty, containerManager.CurrentAdapter.Name);

            var containerManager2 = ContainerManagerFactory.GetContainerManager(TestConstants.UnityContainerName);
            Assert.AreEqual(TestConstants.UnityContainerName, containerManager2.CurrentAdapter.Name); 
        }
    }
}
