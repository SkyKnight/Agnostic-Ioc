using System;
using Agnostic.Ioc.Autofac;
using Agnostic.IoC.UnitTests.Helpers;
using NUnit.Framework;

namespace Agnostic.IoC.UnitTests.ContainerIsolation
{
    [TestFixture]
    public class ContainerIsolationTests
    {
        [Test]
        public void SelfScanningContainerAdapters()
        {
            ContainerAdapterAccessor.Clear();
            IContainerManager containerManager = new ContainerManager(TestConstants.AutofacContainerName);
            Assert.IsNotNull(containerManager.Resolve<IDependantClass>());
        }

        [Test]
        //[ExpectedException(typeof(ArgumentNullException))]
        public void NonSelfScanningContainerAdapters()
        {
            ContainerAdapterAccessor.Clear();
            IContainerManager containerManager = new ContainerManager();
            Assert.IsNull(containerManager.Resolve<IDependantClass>());
        }

        [Test]
        public void PreRegisteredContainerAdapter()
        {
            ContainerAdapterAccessor.Clear();
            var adapter = new AutofacContainerAdapter();
            adapter.Register<IDependantClass, DependantClass2>();
            IContainerManager containerManager = new ContainerManager(adapter);
            Assert.IsNotNull(containerManager.Resolve<IDependantClass>());
            var containerManager2 = new ContainerManager();
            Assert.IsNotNull(containerManager2.Resolve<IDependantClass>());
        }

    }
}
