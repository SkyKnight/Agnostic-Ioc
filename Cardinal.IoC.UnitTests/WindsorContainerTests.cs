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
using Cardinal.IoC.UnitTests.Helpers;
using Cardinal.IoC.Windsor;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace Cardinal.IoC.UnitTests
{
    public class WindsorContainerTests : IContainerTestSuite
    {
        [Test]
        public void ResolveItemByInterfaceOnly()
        {
            IContainerManager containerManager = new ContainerManager();
            IDependantClass dependency = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass), dependency.GetType());
        }

        [Test]
        public void ResolveItemByName()
        {
            ContainerManager containerManager = new ContainerManager();
            IDependantClass dependency = containerManager.Resolve<IDependantClass>("DependentClass2");
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass2), dependency.GetType());
        }

        [Test]
        public void ResolveItemWithParameters()
        {
            ContainerManager containerManager = new ContainerManager();
            IDependantClass dependency = containerManager.Resolve<IDependantClass>(new Dictionary<string, string>());
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass), dependency.GetType());
        }

        [Test]
        public void ResolveItemWithNameAndParameters()
        {
            ContainerManager containerManager = new ContainerManager();
            IDependantClass dependency = containerManager.Resolve<IDependantClass>("DependentClass2", new Dictionary<string, string>());
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass2), dependency.GetType());
        }

        [Test]
        public void UseExternalContainer()
        {
            IWindsorContainer container = new WindsorContainer();
            container.Register(Component.For<IDependantClass>().ImplementedBy<DependantClass>());
            ContainerManager containerManager = new ContainerManager(new WindsorContainerAdapter(container));
            IDependantClass dependency = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass), dependency.GetType());
        }
    }
}
