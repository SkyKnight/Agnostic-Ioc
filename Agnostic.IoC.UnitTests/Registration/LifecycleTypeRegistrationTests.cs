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
using Agnostic.IoC.Registration;
using Agnostic.IoC.UnitTests.Helpers;
using NUnit.Framework;

namespace Agnostic.IoC.UnitTests.Registration
{
    public partial class RegistrationTests
    {
        protected void CanRegisterDefaultTypeComponent(Func<IContainerAdapter> adapterFunc)
        {
            IContainerManager containerManager = new ContainerManager(adapterFunc());
            containerManager.Adapter.Register<IDependantClass, DependantClass>();
            IDependantClass dependantClass1 = containerManager.Resolve<IDependantClass>();
            IDependantClass dependantClass2 = containerManager.Resolve<IDependantClass>();
            Assert.AreNotEqual(dependantClass1, dependantClass2);
        }

        protected void CanRegisterSingletonTypeComponent(Func<IContainerAdapter> adapterFunc)
        {
            IContainerManager containerManager = new ContainerManager(adapterFunc());
            containerManager.Adapter.Register<IDependantClass, DependantClass>(LifetimeScope.Singleton);
            IDependantClass dependantClass1 = containerManager.Resolve<IDependantClass>();
            IDependantClass dependantClass2 = containerManager.Resolve<IDependantClass>();
            Assert.AreEqual(dependantClass1, dependantClass2);
        }

        protected void CanRegisterTransientTypeComponent(Func<IContainerAdapter> adapterFunc)
        {
            IContainerManager containerManager = new ContainerManager(adapterFunc());
            containerManager.Adapter.Register<IDependantClass, DependantClass>(LifetimeScope.Transient);
            IDependantClass dependantClass1 = containerManager.Resolve<IDependantClass>();
            IDependantClass dependantClass2 = containerManager.Resolve<IDependantClass>();
            Assert.AreNotEqual(dependantClass1, dependantClass2);
        }

        protected void CanRegisterNamedDefaultTypeComponent(Func<IContainerAdapter> adapterFunc)
        {
            IContainerManager containerManager = new ContainerManager(adapterFunc());
            containerManager.Adapter.Register<IDependantClass, DependantClass>();
            containerManager.Adapter.Register<IDependantClass, DependantClass2>("fred");
            IDependantClass dependantClass1 = containerManager.Resolve<IDependantClass>("fred");
            IDependantClass dependantClass2 = containerManager.Resolve<IDependantClass>("fred");
            Assert.AreNotEqual(dependantClass1, dependantClass2);
        }

        protected void CanRegisterNamedSingletonTypeComponent(Func<IContainerAdapter> adapterFunc)
        {
            IContainerManager containerManager = new ContainerManager(adapterFunc());
            containerManager.Adapter.Register<IDependantClass, DependantClass>(LifetimeScope.Singleton, "fred");
            IDependantClass dependantClass1 = containerManager.Resolve<IDependantClass>("fred");
            IDependantClass dependantClass2 = containerManager.Resolve<IDependantClass>("fred");
            Assert.AreEqual(dependantClass1, dependantClass2);
        }

        protected void CanRegisterNamedTransientTypeComponent(Func<IContainerAdapter> adapterFunc)
        {
            IContainerManager containerManager = new ContainerManager(adapterFunc());
            containerManager.Adapter.Register<IDependantClass, DependantClass>(LifetimeScope.Transient, "fred");
            IDependantClass dependantClass1 = containerManager.Resolve<IDependantClass>("fred");
            IDependantClass dependantClass2 = containerManager.Resolve<IDependantClass>("fred");
            Assert.AreNotEqual(dependantClass1, dependantClass2);
        }

        protected void CanRegisterFluentDefaultTypeComponent(Func<IContainerAdapter> adapterFunc)
        {
            IContainerManager containerManager = new ContainerManager(adapterFunc());
            IComponentRegistration registration = containerManager.Adapter.CreateComponentRegistration<ComponentRegistration>();
            registration = registration.Register<IDependantClass>().As<DependantClass>();
            containerManager.Adapter.Register(registration);
            IDependantClass dependantClass1 = containerManager.Resolve<IDependantClass>();
            IDependantClass dependantClass2 = containerManager.Resolve<IDependantClass>();
            Assert.AreNotEqual(dependantClass1, dependantClass2);
        }

        protected void CanRegisterFluentTransientTypeComponent(Func<IContainerAdapter> adapterFunc)
        {
            IContainerManager containerManager = new ContainerManager(adapterFunc());
            IComponentRegistration registration = containerManager.Adapter.CreateComponentRegistration<ComponentRegistration>();
            registration = registration.Register<IDependantClass>().As<DependantClass>().Lifetime(LifetimeScope.Transient);
            containerManager.Adapter.Register(registration);
            IDependantClass dependantClass1 = containerManager.Resolve<IDependantClass>();
            IDependantClass dependantClass2 = containerManager.Resolve<IDependantClass>();
            Assert.AreNotEqual(dependantClass1, dependantClass2);
        }

        protected void CanRegisterFluentSingletonTypeComponent(Func<IContainerAdapter> adapterFunc)
        {
            IContainerManager containerManager = new ContainerManager(adapterFunc());
            IComponentRegistration registration = containerManager.Adapter.CreateComponentRegistration<ComponentRegistration>();
            registration = registration.Register<IDependantClass>().As<DependantClass>().Lifetime(LifetimeScope.Singleton);
            containerManager.Adapter.Register(registration);
            IDependantClass dependantClass1 = containerManager.Resolve<IDependantClass>();
            IDependantClass dependantClass2 = containerManager.Resolve<IDependantClass>();
            Assert.AreEqual(dependantClass1, dependantClass2);
        }
    }
}
