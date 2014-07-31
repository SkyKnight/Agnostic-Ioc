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

using System.Collections.Generic;
using Autofac;
using Cardinal.Ioc.Autofac;
using Cardinal.IoC.Registration;
using Cardinal.IoC.UnitTests.Helpers;
using NUnit.Framework;

namespace Cardinal.IoC.UnitTests
{
    /// <summary>
    /// Test fixture for the Autofac container adapter.
    /// </summary>
    [TestFixture]
    public class AutofacAdapterTests : IContainerTestSuite
    {
        /// <summary>
        /// Test that the Autofac container adapter can be loaded from the container factory correctly.
        /// </summary>
        [Test]
        public void GetContainerFromFactory()
        {
            IContainerManager containerManager2 = ContainerManagerFactory.GetContainerManager(TestConstants.AutofacContainerName);
            Assert.AreEqual(TestConstants.AutofacContainerName, containerManager2.CurrentAdapter.Name);
        }

        /// <summary>
        /// Test that trying to resolve a component that has been registered returns that component
        /// </summary>
        [Test]
        public void TestTryResolvesSuccessful()
        {
            ContainerManager manager = new ContainerManager(TestConstants.AutofacContainerName);
            Assert.IsNotNull(manager);

            IDependantClass instance = manager.TryResolve<IDependantClass>();
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// Test that trying to resolve a component that hasn't been registered returns null
        /// rather than throwing an exception
        /// </summary>
        [Test]
        public void TestTryResolvesFails()
        {
            ContainerManager manager = new ContainerManager(TestConstants.AutofacContainerName);
            Assert.IsNotNull(manager);

            LateDependantClass instance = manager.TryResolve<LateDependantClass>();
            Assert.IsNull(instance);
        }

        /// <summary>
        /// Test the late registration of components against an existing container
        /// </summary>
        [Test]
        public void TestLateBoundRegistration()
        {
            IContainerManager containerManager = new ContainerManager(TestConstants.AutofacContainerName);

            containerManager.CurrentAdapter.Register<ILateDependantClass, LateDependantClass>();
            ILateDependantClass dependantClass = containerManager.Resolve<ILateDependantClass>();
            Assert.IsNotNull(dependantClass);
        }

        public void ResolveComponentByInterfaceOnly()
        {
            ContainerManager manager = new ContainerManager(TestConstants.AutofacContainerName);
            Assert.IsNotNull(manager);

            IDependantClass component = manager.Resolve<IDependantClass>();
            Assert.IsNotNull(component);

            Assert.AreEqual(typeof(DependantClass), component.GetType());
        }

        public void ResolveComponentByName()
        {
            ContainerManager manager = new ContainerManager(TestConstants.AutofacContainerName);
            Assert.IsNotNull(manager);

            IDependantClass component = manager.Resolve<IDependantClass>("DependantClass2");
            Assert.IsNotNull(component);

            Assert.AreEqual(typeof(DependantClass2), component.GetType());
        }

        /// <summary>
        /// Test that trying to resolve a component with parameters that has been registered returns that component
        /// </summary>
        [Test]
        public void ResolveComponentWithParameters()
        {
            ContainerManager containerManager = new ContainerManager(TestConstants.AutofacContainerName);
            IDependantClass dependency = containerManager.Resolve<IDependantClass>(new Dictionary<string, object>());
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass), dependency.GetType());
        }

        /// <summary>
        /// Test that trying to resolve a named component with parameters that has been registered returns that component
        /// </summary>
        [Test]
        public void ResolveComponentWithNameAndParameters()
        {
            ContainerManager containerManager = new ContainerManager(TestConstants.AutofacContainerName);
            IDependantClass dependency = containerManager.Resolve<IDependantClass>("DependantClass2", new Dictionary<string, object>());
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass2), dependency.GetType());
        }

        public void UseExternalContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<DependantClass2>().As<IDependantClass>();
            ContainerManager containerManager = new ContainerManager(new AutofacContainerAdapter(builder.Build()));
            IDependantClass dependency = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass2), dependency.GetType());
        }
    }
}
