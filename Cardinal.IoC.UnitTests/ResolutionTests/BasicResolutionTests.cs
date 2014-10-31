// --------------------------------------------------------------------------------------------------------------------
// Copyright (c) 2014, Simon Proctor and Nathanael Mann
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Cardinal.IoC.UnitTests.Helpers;
using Cardinal.IoC.Windsor;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Moq;
using NUnit.Framework;

namespace Cardinal.IoC.UnitTests.ResolutionTests
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
            var containerManager = ContainerManagerFactory.GetContainerManager(TestConstants.WindsorContainerName);
            Assert.AreEqual(TestConstants.WindsorContainerName, containerManager.Adapter.Name);

            var containerManager2 = ContainerManagerFactory.GetContainerManager(TestConstants.UnityContainerName);
            Assert.AreEqual(TestConstants.UnityContainerName, containerManager2.Adapter.Name); 
        }

        /// <summary>
        /// Tests starting a container manager manually
        /// </summary>
        [Test]
        [ExpectedException(typeof(ComponentNotFoundException))]
        public void StartContainerManually()
        {
            IWindsorContainer container = new WindsorContainer();
            string containerKey = Guid.NewGuid().ToString();
            var containerManager = new ContainerManager(new WindsorContainerAdapter(containerKey, container));

            Assert.IsNull(containerManager.Resolve<IDependantClass>());

            containerManager.Adapter.Register<IDependantClass, DependantClass>();
            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();

            Assert.IsNotNull(dependantClass);
        }

        /// <summary>
        /// Tests working on TryResolves
        /// </summary>
        [Test]
        public void TryResolvesSuccessful()
        {
            string containerKey = Guid.NewGuid().ToString();
            Mock<IContainerAdapter> containerAdapterMock = new Mock<IContainerAdapter>();
            containerAdapterMock.SetupGet(x => x.Name).Returns(containerKey);
            DependantClass dependantClass = new DependantClass();
            
            IContainerManager containerManager = new ContainerManager(containerAdapterMock.Object);
            
            containerAdapterMock.Setup(x => x.TryResolve<IDependantClass>()).Returns(dependantClass);
            Assert.AreEqual(dependantClass, containerManager.TryResolve<IDependantClass>());

            DependantClass dependantClass2 = new DependantClass { Name = "Pass 2" };
            containerAdapterMock.Setup(x => x.TryResolve<IDependantClass>(It.IsAny<string>())).Returns(dependantClass2);
            Assert.AreEqual(dependantClass2, containerManager.TryResolve<IDependantClass>("string"));

            DependantClass dependantClass3 = new DependantClass { Name = "Pass 3" };
            containerAdapterMock.Setup(x => x.TryResolve<IDependantClass>(It.IsAny<IDictionary<string, object>>())).Returns(dependantClass3);
            Assert.AreEqual(dependantClass3, containerManager.TryResolve<IDependantClass>(new Dictionary<string, object>()));

            DependantClass dependantClass4 = new DependantClass { Name = "Pass 4" };
            containerAdapterMock.Setup(x => x.TryResolve<IDependantClass>(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>())).Returns(dependantClass4);
            Assert.AreEqual(dependantClass4, containerManager.TryResolve<IDependantClass>("name", new Dictionary<string, object>()));
        }

        /// <summary>
        /// Tests the behaviour of when TryResolve fails.
        /// </summary>
        [Test]
        public void TryResolvesFails()
        {
            Mock<IContainerAdapter> containerAdapterMock = new Mock<IContainerAdapter>();
            string containerKey = Guid.NewGuid().ToString();
            containerAdapterMock.SetupGet(x => x.Name).Returns(containerKey);

            IContainerManager containerManager = new ContainerManager(containerAdapterMock.Object);

            containerAdapterMock.Setup(x => x.Resolve<IDependantClass>()).Throws<Exception>();
            Assert.IsNull(containerManager.TryResolve<IDependantClass>());

            containerAdapterMock.Setup(x => x.Resolve<IDependantClass>(It.IsAny<string>())).Throws<Exception>();
            Assert.IsNull(containerManager.TryResolve<IDependantClass>("string"));

            containerAdapterMock.Setup(x => x.Resolve<IDependantClass>(It.IsAny<IDictionary<string, object>>())).Throws<Exception>();
            Assert.IsNull(containerManager.TryResolve<IDependantClass>(new Dictionary<string, object>()));

            containerAdapterMock.Setup(x => x.Resolve<IDependantClass>(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>())).Throws<Exception>();
            Assert.IsNull(containerManager.TryResolve<IDependantClass>("name", new Dictionary<string, object>()));

            containerAdapterMock.Setup(x => x.Resolve(typeof(IDependantClass))).Throws<Exception>();
            Assert.IsNull(containerManager.TryResolve(typeof(IDependantClass)));

            containerAdapterMock.Setup(x => x.Resolve(typeof(IDependantClass), It.IsAny<string>())).Throws<Exception>();
            Assert.IsNull(containerManager.TryResolve(typeof(IDependantClass), "fred"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReuseSameContainerManagerFromExternalContainer()
        {
            IWindsorContainer windsorContainer = new WindsorContainer();
            windsorContainer.Register(Component.For<IDependantClass>().ImplementedBy<DependantClass2>());
            string containerKey = Guid.NewGuid().ToString();

            IContainerManager containerManager =
                new ContainerManager(new WindsorContainerAdapter(containerKey, windsorContainer));
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
