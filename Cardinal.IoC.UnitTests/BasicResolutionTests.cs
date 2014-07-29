using System;
using System.Collections;
using System.Collections.Generic;
using Cardinal.IoC.Registration;
using Cardinal.IoC.UnitTests.Helpers;
using Cardinal.IoC.UnitTests.TestAdapters;
using Cardinal.IoC.Windsor;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Moq;
using NUnit.Framework;

namespace Cardinal.IoC.UnitTests
{
    [TestFixture]
    public class BasicResolutionTests
    {
        /// <summary>
        /// Tests getting the desired container from the factory - useful for parameterless constructors
        /// </summary>
        [Test]
        public void GetContainerFromFactory()
        {
            var containerManager = ContainerManagerFactory.GetContainerManager();
            Assert.AreEqual(String.Empty, containerManager.CurrentAdapter.Name);

            var containerManager2 = ContainerManagerFactory.GetContainerManager(TestConstants.UnityContainerName);
            Assert.AreEqual(TestConstants.UnityContainerName, containerManager2.CurrentAdapter.Name); 
        }

        /// <summary>
        /// Tests starting a container manager manually
        /// </summary>
        [Test]
        [ExpectedException(typeof(ComponentNotFoundException))]
        public void StartContainerManually()
        {
            var containerManager = new ContainerManager(new EmptyWindsorContainerAdapter());
            Assert.AreEqual(TestConstants.EmptyWindsorContainerName, containerManager.CurrentAdapter.Name);

            Assert.IsNull(containerManager.Resolve<IDependantClass>());

            containerManager.Register(new RegistrationDefinition<IDependantClass, DependantClass>());
            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();

            Assert.IsNotNull(dependantClass);
            Assert.AreEqual(TestConstants.DependantClassName, dependantClass.Name);
        }

        /// <summary>
        /// Tests working on TryResolves
        /// </summary>
        [Test]
        public void TryResolvesSuccessful()
        {
            Mock<IContainerAdapter> containerAdapterMock = new Mock<IContainerAdapter>();
            containerAdapterMock.SetupGet(x => x.Name).Returns("Try Resolves");
            DependantClass dependantClass = new DependantClass();
            
            IContainerManager containerManager = new ContainerManager(containerAdapterMock.Object);
            
            containerAdapterMock.Setup(x => x.TryResolve<IDependantClass>()).Returns(dependantClass);
            Assert.AreEqual(dependantClass, containerManager.TryResolve<IDependantClass>());

            DependantClass dependantClass2 = new DependantClass { Name = "Pass 2" };
            containerAdapterMock.Setup(x => x.TryResolve<IDependantClass>(It.IsAny<string>())).Returns(dependantClass2);
            Assert.AreEqual(dependantClass2, containerManager.TryResolve<IDependantClass>("string"));

            DependantClass dependantClass3 = new DependantClass { Name = "Pass 3" };
            containerAdapterMock.Setup(x => x.TryResolve<IDependantClass>(It.IsAny<IDictionary>())).Returns(dependantClass3);
            Assert.AreEqual(dependantClass3, containerManager.TryResolve<IDependantClass>(new Dictionary<string, string>()));

            DependantClass dependantClass4 = new DependantClass { Name = "Pass 4" };
            containerAdapterMock.Setup(x => x.TryResolve<IDependantClass>(It.IsAny<string>(), It.IsAny<IDictionary>())).Returns(dependantClass4);
            Assert.AreEqual(dependantClass4, containerManager.TryResolve<IDependantClass>("name", new Dictionary<string, string>()));
        }

        /// <summary>
        /// Tests the behaviour of when TryResolve fails.
        /// </summary>
        [Test]
        public void TryResolvesFails()
        {
            Mock<IContainerAdapter> containerAdapterMock = new Mock<IContainerAdapter>();

            IContainerManager containerManager = new ContainerManager(containerAdapterMock.Object);
            containerAdapterMock.SetupGet(x => x.Name).Returns("Try Resolves Fails");

            containerAdapterMock.Setup(x => x.Resolve<IDependantClass>()).Throws<Exception>();
            Assert.IsNull(containerManager.TryResolve<IDependantClass>());

            containerAdapterMock.Setup(x => x.Resolve<IDependantClass>(It.IsAny<string>())).Throws<Exception>();
            Assert.IsNull(containerManager.TryResolve<IDependantClass>("string"));

            containerAdapterMock.Setup(x => x.Resolve<IDependantClass>(It.IsAny<IDictionary>())).Throws<Exception>();
            Assert.IsNull(containerManager.TryResolve<IDependantClass>(new Dictionary<string, string>()));

            containerAdapterMock.Setup(x => x.Resolve<IDependantClass>(It.IsAny<string>(), It.IsAny<IDictionary>())).Throws<Exception>();
            Assert.IsNull(containerManager.TryResolve<IDependantClass>("name", new Dictionary<string, string>()));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReuseSameContainerManagerFromExternalContainer()
        {
            IWindsorContainer windsorContainer = new WindsorContainer();
            windsorContainer.Register(Component.For<IDependantClass>().ImplementedBy<DependantClass2>());
            IContainerManager containerManager =
                new ContainerManager(new WindsorContainerAdapter(windsorContainer));
            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependantClass);
            IContainerManager containerManager2 = new ContainerManager("New Container");

            IDependantClass dependantClass2 = containerManager2.Resolve<IDependantClass>();
            Assert.IsNotNull(dependantClass2);

            IContainerManager containerManager3 = new ContainerManager("New Container 1");

            // Show its not random
            IDependantClass dependantClass3 = containerManager3.TryResolve<IDependantClass>();
            Assert.IsNull(dependantClass3);
        }
    }
}
