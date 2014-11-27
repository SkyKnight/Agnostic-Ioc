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
using Agnostic.IoC.UnitTests.Helpers;
using NUnit.Framework;

namespace Agnostic.IoC.UnitTests.ResolutionTests
{
    public class SharedContainerTests
    {
        protected void PerformSharedTests(Func<IContainerManager> containerManagerFunc)
        {
            ResolveComponentByInterfaceOnly(containerManagerFunc);
            ResolveComponentByName(containerManagerFunc);
            ResolveComponentWithParameters(containerManagerFunc);
            ResolveComponentWithNameAndParameters(containerManagerFunc);
            TestLateBoundRegistration(containerManagerFunc);
            //TestTryResolvesFails(containerManagerFunc);
            //TestTryResolvesSuccessful(containerManagerFunc);

        }

        /// <summary>
        /// Test that trying to resolve a component that has been registered returns that component
        /// </summary>
        public void TestTryResolvesSuccessful(Func<IContainerManager> containerManagerFunc)
        {
            IContainerManager manager = containerManagerFunc();
            Assert.IsNotNull(manager);

            IDependantClass instance = manager.TryResolve<IDependantClass>();
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// Test that trying to resolve a component that hasn't been registered returns null
        /// rather than throwing an exception
        /// </summary>
        public void TestTryResolvesFails(Func<IContainerManager> containerManagerFunc)
        {
            IContainerManager manager = containerManagerFunc();
            Assert.IsNotNull(manager);

            LateDependantClass instance = manager.TryResolve<LateDependantClass>();
            Assert.IsNull(instance);
        }

        /// <summary>
        /// Test the late registration of components against an existing container
        /// </summary>
        public void TestLateBoundRegistration(Func<IContainerManager> containerManagerFunc)
        {
            IContainerManager containerManager = containerManagerFunc();

            containerManager.Adapter.Register<ILateDependantClass, LateDependantClass>();
            ILateDependantClass dependantClass = containerManager.Resolve<ILateDependantClass>();
            Assert.IsNotNull(dependantClass);
        }

        protected void ResolveComponentByInterfaceOnly(Func<IContainerManager> containerManagerFunc)
        {
            IContainerManager containerManager = containerManagerFunc();
            IDependantClass dependency = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass), dependency.GetType());
        }

        protected void ResolveComponentByName(Func<IContainerManager> containerManagerFunc)
        {
            IContainerManager containerManager = containerManagerFunc();
            IDependantClass dependency = containerManager.Resolve<IDependantClass>("DependentClass2");
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass2), dependency.GetType());
        }

        protected void ResolveComponentWithParameters(Func<IContainerManager> containerManagerFunc)
        {
            IContainerManager containerManager = containerManagerFunc();
            IDependantClass dependency = containerManager.Resolve<IDependantClass>(new Dictionary<string, object>());
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof (DependantClass), dependency.GetType());
        }

        protected void ResolveComponentWithNameAndParameters(Func<IContainerManager> containerManagerFunc)
        {
            IContainerManager containerManager = containerManagerFunc();
            IDependantClass dependency = containerManager.Resolve<IDependantClass>("DependentClass2", new Dictionary<string, object>());
            Assert.IsNotNull(dependency);
            Assert.AreEqual(typeof(DependantClass2), dependency.GetType());
        }
    }
}
