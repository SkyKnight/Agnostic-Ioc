﻿using System;
using System.Collections.Generic;
using Cardinal.IoC.UnitTests.Helpers;
using Cardinal.IoC.UnitTests.TestAdapters;
using Cardinal.IoC.Unity;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Cardinal.IoC.UnitTests
{
    public class UnityContainerTests : IContainerTestSuite
    {
        [Test]
        public void ResolveItemByInterfaceOnly()
        {
            IContainerManager containerManager = new ContainerManager(TestConstants.UnityContainerName);
            IDependantClass dependency = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass2), dependency.GetType());
        }

        [Test]
        public void ResolveItemByName()
        {
            ContainerManager containerManager = new ContainerManager(TestConstants.UnityContainerName);
            IDependantClass dependency = containerManager.Resolve<IDependantClass>("DependentClass2");
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass), dependency.GetType());
        }

        [Test]
        public void ResolveItemWithParameters()
        {
            ContainerManager containerManager = new ContainerManager(TestConstants.UnityContainerName);
            IDependantClass dependency = containerManager.Resolve<IDependantClass>(new Dictionary<string, string>());
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass2), dependency.GetType());
        }

        [Test]
        public void ResolveItemWithNameAndParameters()
        {
            ContainerManager containerManager = new ContainerManager(TestConstants.UnityContainerName);
            IDependantClass dependency = containerManager.Resolve<IDependantClass>("DependentClass2", new Dictionary<string, string>());
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass), dependency.GetType());
        }

        [Test]
        public void UseExternalContainer()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType(typeof(IDependantClass), typeof(DependantClass));
            IContainerManager containerManager = new ContainerManager(new UnityContainerAdapter(container));
            IDependantClass dependency = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass), dependency.GetType());
        }
    }
}
