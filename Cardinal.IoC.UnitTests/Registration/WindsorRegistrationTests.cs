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
using Cardinal.IoC.UnitTests.Helpers;
using Cardinal.IoC.UnitTests.TestClasses;
using Cardinal.IoC.Windsor;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace Cardinal.IoC.UnitTests.Registration
{
    [TestFixture]
    public class WindsorRegistrationTests : IRegistrationTestSuite
    {
        [Test]
        public void TestSimpleRegistration()
        {
            IWindsorContainer container = new WindsorContainer();
            string containerKey = Guid.NewGuid().ToString();
            IContainerManager containerManager = new ContainerManager(new WindsorContainerAdapter(containerKey, container));
            Assert.IsNull(containerManager.TryResolve<IDependantClass>());

            containerManager.CurrentAdapter.Register<IDependantClass, DependantClass>();
            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependantClass);
        }

        [Test]
        public void TestSimpleNamedRegistration()
        {
            IWindsorContainer container = new WindsorContainer();
            string containerKey = Guid.NewGuid().ToString();
            IContainerManager containerManager = new ContainerManager(new WindsorContainerAdapter(containerKey, container));

            const string dependencyName = "dependantReg";

            containerManager.CurrentAdapter.Register<IDependantClass, DependantClass>();
            containerManager.CurrentAdapter.Register<IDependantClass, DependantClass2>(dependencyName);
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
            IWindsorContainer container = new WindsorContainer();
            string containerKey = Guid.NewGuid().ToString();
            IContainerManager containerManager = new ContainerManager(new WindsorContainerAdapter(containerKey, container));
            Assert.IsNull(containerManager.TryResolve<IDependantClass>());

            DependantClass instanceDependantClass = new DependantClass();
            containerManager.CurrentAdapter.Register<IDependantClass>(instanceDependantClass);
            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.AreEqual(instanceDependantClass, dependantClass);
        }

        [Test]
        public void GroupRegistration()
        {
            IWindsorContainer container = new WindsorContainer();
            string containerKey = Guid.NewGuid().ToString();
            IContainerManager containerManager = new ContainerManager(new WindsorContainerAdapter(containerKey, container));
            Assert.IsNull(containerManager.TryResolve<IDependantClass>());

            IContainerManagerGroupRegistration groupRegistration = new TestGroupRegistration();
            containerManager.CurrentAdapter.Register(groupRegistration);

            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependantClass);
            Assert.AreEqual(typeof(DependantClass), dependantClass.GetType());

        }

        [Test]
        public void ResolveAll()
        {
            IWindsorContainer container = new WindsorContainer();
            container.Register(Classes.FromAssemblyInThisApplication().BasedOn<IDependantClass>().WithServiceAllInterfaces());
            string containerKey = Guid.NewGuid().ToString();
            IContainerManager containerManager = new ContainerManager(new WindsorContainerAdapter(containerKey, container));
            var resolved = containerManager.ResolveAll<IDependantClass>();
            Assert.Greater(resolved.Count(), 0);
        }
    }
}
