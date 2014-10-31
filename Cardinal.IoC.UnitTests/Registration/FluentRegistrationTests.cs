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
using Cardinal.IoC.Registration;
using Cardinal.IoC.UnitTests.Helpers;
using NUnit.Framework;

namespace Cardinal.IoC.UnitTests.Registration
{
    public partial class RegistrationTests
    {
        protected void CanRegisterFluentInstance(Func<IContainerAdapter> adapterFunc)
        {
            IContainerAdapter adapter = adapterFunc();

            var returnClass = new DependantClass();

            var registration = adapter.CreateComponentRegistration<ComponentRegistration>()
                .Register<ISuperDependantClass>()
                .Instance(returnClass);

            adapter.Register(registration);

            Assert.AreEqual(returnClass, adapter.Resolve<ISuperDependantClass>());
            Assert.IsNull(adapter.TryResolve<IDependantClass>());
        }

        protected void CanRegisterOneFluentType(Func<IContainerAdapter> adapterFunc)
        {
            IContainerAdapter adapter = adapterFunc();

            var registration = adapter.CreateComponentRegistration<ComponentRegistration>()
                .Register<ISuperDependantClass>()
                .As<DependantClass>();

            adapter.Register(registration);

            Assert.AreEqual(typeof(DependantClass), adapter.Resolve<ISuperDependantClass>().GetType());
            Assert.IsNull(adapter.TryResolve<IDependantClass>());
        }

        protected void CanRegisterTwoFluentTypes(Func<IContainerAdapter> adapterFunc)
        {
            IContainerAdapter adapter = adapterFunc();

            var registration = adapter.CreateComponentRegistration<ComponentRegistration>()
                .Register<IDependantClass, ISuperDependantClass>()
                .As<DependantClass>();

            adapter.Register(registration);

            Assert.AreEqual(adapter.Resolve<IDependantClass>().GetType(), adapter.Resolve<ISuperDependantClass>().GetType());
            Assert.AreEqual(typeof(DependantClass), adapter.Resolve<ISuperDependantClass>().GetType());
            Assert.AreEqual(typeof(DependantClass), adapter.Resolve<IDependantClass>().GetType());
        }

        protected void CanRegisterThreeFluentTypes(Func<IContainerAdapter> adapterFunc)
        {
            IContainerAdapter adapter = adapterFunc();

            var registration = adapter.CreateComponentRegistration<ComponentRegistration>()
                .Register<IDependantClass, ISuperDependantClass, IOtherDependantClass>()
                .As<DependantClass>();

            adapter.Register(registration);

            Assert.AreEqual(adapter.Resolve<IDependantClass>().GetType(), adapter.Resolve<ISuperDependantClass>().GetType());
            Assert.AreEqual(typeof(DependantClass), adapter.Resolve<ISuperDependantClass>().GetType());
            Assert.AreEqual(typeof(DependantClass), adapter.Resolve<IDependantClass>().GetType());
            Assert.AreEqual(typeof(DependantClass), adapter.Resolve<IOtherDependantClass>().GetType());
        }


        protected void CanRegisterOneNamedFluentType(Func<IContainerAdapter> adapterFunc)
        {
            IContainerAdapter adapter = adapterFunc();

            var registration = adapter.CreateComponentRegistration<ComponentRegistration>()
                .Register<ISuperDependantClass>()
                .As<DependantClass>()
                .Named("fred");

            adapter.Register(registration);

            Assert.AreEqual(typeof(DependantClass), adapter.Resolve<ISuperDependantClass>("fred").GetType());
            Assert.IsNull(adapter.TryResolve<IDependantClass>());
        }

        protected void CanRegisterTwoNamedFluentTypes(Func<IContainerAdapter> adapterFunc)
        {
            IContainerAdapter adapter = adapterFunc();

            var registration = adapter.CreateComponentRegistration<ComponentRegistration>()
                .Register<IDependantClass, ISuperDependantClass>()
                .As<DependantClass>()
                .Named("fred");

            adapter.Register(registration);

            Assert.AreEqual(adapter.Resolve<IDependantClass>().GetType(), adapter.Resolve<ISuperDependantClass>().GetType());
            Assert.AreEqual(typeof(DependantClass), adapter.Resolve<ISuperDependantClass>().GetType());
            Assert.AreEqual(typeof(DependantClass), adapter.Resolve<IDependantClass>().GetType());
        }

        protected void CanRegisterThreeNamedFluentTypes(Func<IContainerAdapter> adapterFunc)
        {
            IContainerAdapter adapter = adapterFunc();

            var registration = adapter.CreateComponentRegistration<ComponentRegistration>()
                .Register<IDependantClass, ISuperDependantClass, IOtherDependantClass>()
                .As<DependantClass>()
                .Named("fred");

            adapter.Register(registration);

            Assert.AreEqual(adapter.Resolve<IDependantClass>().GetType(), adapter.Resolve<ISuperDependantClass>().GetType());
            Assert.AreEqual(typeof(DependantClass), adapter.Resolve<ISuperDependantClass>().GetType());
            Assert.AreEqual(typeof(DependantClass), adapter.Resolve<IDependantClass>().GetType());
            Assert.AreEqual(typeof(DependantClass), adapter.Resolve<IOtherDependantClass>().GetType());
        }

        protected void CanRegisterFluentTypeWithLifetime(Func<IContainerAdapter> adapterFunc)
        {
            IContainerAdapter adapter = adapterFunc();

            var registration = adapter.CreateComponentRegistration<ComponentRegistration>()
                .Register<IDependantClass, ISuperDependantClass>()
                .As<DependantClass>()
                .Lifetime(LifetimeScope.Singleton);

            adapter.Register(registration);

            Assert.AreEqual(adapter.Resolve<IDependantClass>().GetType(), adapter.Resolve<ISuperDependantClass>().GetType());
            Assert.AreEqual(typeof(DependantClass), adapter.Resolve<ISuperDependantClass>().GetType());
            Assert.AreEqual(typeof(DependantClass), adapter.Resolve<IDependantClass>().GetType());
        }
    }
}
