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
using System.Linq;
using Cardinal.IoC.Registration;
using Cardinal.IoC.StructureMap;
using Cardinal.IoC.UnitTests.Helpers;
using Cardinal.IoC.UnitTests.TestClasses;
using Cardinal.IoC.Unity;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using StructureMap;

namespace Cardinal.IoC.UnitTests.Registration
{
    [TestFixture]
    public class StructureMapRegistrationTests : IRegistrationTestSuite
    {
        [Test]
        public void TestSimpleRegistration()
        {
            IContainer container = new Container();
            string containerKey = Guid.NewGuid().ToString();
            IContainerManager containerManager = new ContainerManager(new StructureMapContainerAdapter(containerKey, container));
            Assert.IsNull(containerManager.TryResolve<IDependantClass>());

            containerManager.Adapter.Register<IDependantClass, DependantClass>();
            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependantClass);
        }

        [Test]
        public void TestSimpleNamedRegistration()
        {
            IContainer container = new Container();
            string containerKey = Guid.NewGuid().ToString();
            IContainerManager containerManager = new ContainerManager(new StructureMapContainerAdapter(containerKey, container));

            const string dependencyName = "dependantReg";

            containerManager.Adapter.Register<IDependantClass, DependantClass2>(dependencyName);
            containerManager.Adapter.Register<IDependantClass, DependantClass>();
            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependantClass);

            IDependantClass dependantClass2 = containerManager.Resolve<IDependantClass>(dependencyName);
            Assert.IsNotNull(dependantClass2);
            Assert.AreEqual(typeof(DependantClass2), dependantClass2.GetType());
            Assert.AreEqual(TestConstants.DependantClassName, dependantClass.Name);
            Assert.AreEqual(TestConstants.DependantClass2Name, dependantClass2.Name);
        }

        [Test]
        public void TestSimpleInstanceRegistration()
        {
            IContainer container = new Container();
            string containerKey = Guid.NewGuid().ToString();
            IContainerManager containerManager = new ContainerManager(new StructureMapContainerAdapter(containerKey, container));
            Assert.IsNull(containerManager.TryResolve<IDependantClass>());

            DependantClass instanceDependantClass = new DependantClass();
            containerManager.Adapter.Register<IDependantClass>(instanceDependantClass);
            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.AreEqual(instanceDependantClass, dependantClass);
        }

        [Test]
        public void GroupRegistration()
        {
            IContainer container = new Container();
            string containerKey = Guid.NewGuid().ToString();
            IContainerManager containerManager = new ContainerManager(new StructureMapContainerAdapter(containerKey, container));
            Assert.IsNull(containerManager.TryResolve<IDependantClass>());

            IContainerManagerGroupRegistration groupRegistration = new TestGroupRegistration();
            containerManager.Adapter.RegisterGroup(groupRegistration);

            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependantClass);
            Assert.AreEqual(typeof(DependantClass), dependantClass.GetType());

        }

        [Test]
        public void ResolveAll()
        {
            IContainer container = new Container();
            string containerKey = Guid.NewGuid().ToString();
            IContainerManager containerManager = new ContainerManager(new StructureMapContainerAdapter(containerKey, container));
            containerManager.Adapter.Register<IDependantClass, DependantClass>();
            containerManager.Adapter.Register<IDependantClass, DependantClass2>();
            var resolved = containerManager.ResolveAll<IDependantClass>();
            Assert.Greater(resolved.Count(), 0);
        }
    }
}
