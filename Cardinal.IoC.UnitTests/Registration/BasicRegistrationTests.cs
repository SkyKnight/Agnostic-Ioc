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
using NUnit.Framework;

namespace Cardinal.IoC.UnitTests.Registration
{
    public partial class RegistrationTests
    {
        protected void TestSimpleRegistration(Func<IContainerAdapter> adapterFunc)
        {
            IContainerManager containerManager = new ContainerManager(adapterFunc());
            Assert.IsNull(containerManager.TryResolve<IDependantClass>());

            containerManager.Adapter.Register<IDependantClass, DependantClass>();
            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependantClass);
        }

        protected void TestMultipleSimpleRegistrationsResolvesFirst(Func<IContainerAdapter> adapterFunc)
        {
            IContainerManager containerManager = new ContainerManager(adapterFunc());
            Assert.IsNull(containerManager.TryResolve<IDependantClass>());

            containerManager.Adapter.Register<IDependantClass, DependantClass2>();
            containerManager.Adapter.Register<IDependantClass, DependantClass>();
            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependantClass);
            Assert.AreEqual(typeof(DependantClass2), dependantClass.GetType());
        }

        protected void TestSimpleNamedRegistration(Func<IContainerAdapter> adapterFunc)
        {
            IContainerManager containerManager = new ContainerManager(adapterFunc());

            const string dependencyName = "dependantReg";

            containerManager.Adapter.Register<IDependantClass, DependantClass>();
            containerManager.Adapter.Register<IDependantClass, DependantClass2>(dependencyName);
            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependantClass);

            IDependantClass dependantClass2 = containerManager.Resolve<IDependantClass>(dependencyName);
            Assert.IsNotNull(dependantClass2);
            Assert.AreEqual(typeof(DependantClass2), dependantClass2.GetType());
            Assert.AreEqual(TestConstants.DependantClassName, dependantClass.Name);
            Assert.AreEqual(TestConstants.DependantClass2Name, dependantClass2.Name);
        }

        protected void CanRegisterBasicType(Func<IContainerAdapter> adapterFunc)
        {
            IContainerManager containerManager = new ContainerManager(adapterFunc());
            Assert.IsNull(containerManager.TryResolve<IDependantClass>());

            DependantClass instanceDependantClass = new DependantClass();
            containerManager.Adapter.Register<IDependantClass>(instanceDependantClass);
            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.AreEqual(instanceDependantClass, dependantClass);
        }

        protected void CanRegisterGroup(Func<IContainerAdapter> adapterFunc)
        {
            IContainerManager containerManager = new ContainerManager(adapterFunc());
            Assert.IsNull(containerManager.TryResolve<IDependantClass>());

            IContainerManagerGroupRegistration groupRegistration = new TestGroupRegistration();
            containerManager.Adapter.RegisterGroup(groupRegistration);

            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependantClass);
            Assert.AreEqual(typeof(DependantClass), dependantClass.GetType());
        }

        protected void EnsureRegistrationOrderCorrect(Func<IContainerAdapter> adapterFunc)
        {
            IContainerManager containerManager = new ContainerManager(adapterFunc());

            containerManager.Adapter.Register<IDependantClass, DependantClass>();
            containerManager.Adapter.Register<IDependantClass, DependantClass2>();
            containerManager.Adapter.Register<IDependantClass, DependantClass3>();
            containerManager.Adapter.Register<IDependantClass, DependantClass2>();

            IDependantClass[] resolved = containerManager.ResolveAll<IDependantClass>().ToArray();
            Assert.AreEqual(typeof(DependantClass), resolved[0].GetType());
            Assert.AreEqual(typeof(DependantClass2), resolved[1].GetType());
            Assert.AreEqual(typeof(DependantClass3), resolved[2].GetType());
            Assert.AreEqual(typeof(DependantClass2), resolved[3].GetType());
        }
    }
}
