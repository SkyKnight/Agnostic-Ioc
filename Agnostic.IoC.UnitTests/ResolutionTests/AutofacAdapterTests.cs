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
using System.Linq;
using Agnostic.Ioc.Autofac;
using Agnostic.IoC.UnitTests.Helpers;
using Autofac;
using NUnit.Framework;

namespace Agnostic.IoC.UnitTests.ResolutionTests
{
    /// <summary>
    /// Test fixture for the Autofac container adapter.
    /// </summary>
    [TestFixture]
    public class AutofacAdapterTests : SharedContainerTests, IResolutionTestSuite
    {
        [Test]
        public void PerformSharedTests()
        {
            PerformSharedTests(() => new ContainerManager(TestConstants.AutofacContainerName));
        }

        [Test]
        public void UseExternalContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<DependantClass2>().As<IDependantClass>();
            ContainerManager containerManager = new ContainerManager(new AutofacContainerAdapter(builder.Build()));
            IDependantClass dependency = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass2), dependency.GetType());
        }

        [Test]
        public void ResolveAll()
        {
            string containerKey = Guid.NewGuid().ToString();
            IContainerManager containerManager = new ContainerManager(new AutofacContainerAdapter(containerKey));
            containerManager.Adapter.Register<IDependantClass, DependantClass>();
            containerManager.Adapter.Register<IDependantClass, DependantClass2>();
            var resolved = containerManager.ResolveAll<IDependantClass>();
            Assert.Greater(resolved.Count(), 0);
        }
    }
}
