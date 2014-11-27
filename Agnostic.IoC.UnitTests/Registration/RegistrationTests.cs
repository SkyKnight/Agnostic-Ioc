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
using Agnostic.Ioc.Autofac;
using Agnostic.IoC.Ninject;
using Agnostic.IoC.StructureMap;
using Agnostic.IoC.Unity;
using Agnostic.IoC.Windsor;
using Agnostic.IoC.Registration;
using Agnostic.IoC.UnitTests.Helpers;
using Agnostic.IoC.UnitTests.TestClasses;
using Castle.Windsor;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using StructureMap;

namespace Agnostic.IoC.UnitTests.Registration
{
    [TestFixture]
    public partial class RegistrationTests
    {
        [Test]
        public void TestWindsorContainerAdapter()
        {
            TestContainerAdapter<WindsorContainerAdapter>(ContainerAdapterFactory.GetWindsorContainerAdapter);
        }

        [Test]
        public void TestAutofacContainerAdapter()
        {
            TestContainerAdapter<AutofacContainerAdapter>(ContainerAdapterFactory.GetAutofacContainerAdapter);
        }

        [Test]
        public void TestStructureMapContainerAdapter()
        {
            TestContainerAdapter<StructureMapContainerAdapter>(ContainerAdapterFactory.GetStructureMapContainerAdapter);
        }

        [Test]
        public void TestUnityContainerAdapter()
        {
            TestContainerAdapter<UnityContainerAdapter>(ContainerAdapterFactory.GetUnityContainerAdapter);
        }

        [Test]
        public void TestNinjectContainerAdapter()
        {
            TestContainerAdapter<NinjectContainerAdapter>(ContainerAdapterFactory.GetNinjectContainerAdapter);
        }

        protected void TestContainerAdapter<T>(Func<IContainerAdapter> adapterFunc) where T : IContainerAdapter
        {
            Assert.AreEqual(typeof(T), adapterFunc().GetType());
            TestSimpleRegistration(adapterFunc);
            CanRegisterRawType(adapterFunc);
            CanRegisterNamedRawType(adapterFunc);
            CanRegisterInstanceByName(adapterFunc);
            CanRegisterByName(adapterFunc);
            EnsureRegistrationOrderCorrect(adapterFunc);
            CanRegisterGroup(adapterFunc);
            CanRegisterBasicType(adapterFunc);

            CanRegisterOneFluentType(adapterFunc);
            CanRegisterTwoFluentTypes(adapterFunc);
            CanRegisterThreeFluentTypes(adapterFunc);

            CanRegisterOneNamedFluentType(adapterFunc);

            // todo: fix this
            // CanRegisterTwoNamedFluentTypes(adapterFunc);
            // CanRegisterThreeNamedFluentTypes(adapterFunc);

            TestMultipleSimpleRegistrationsResolvesFirst(adapterFunc);
            CanRegisterFluentTypeWithLifetime(adapterFunc);
            CanRegisterFluentInstance(adapterFunc);

            CanRegisterDefaultTypeComponent(adapterFunc);
            CanRegisterSingletonTypeComponent(adapterFunc);
            CanRegisterTransientTypeComponent(adapterFunc);

            CanRegisterNamedDefaultTypeComponent(adapterFunc);
            CanRegisterNamedSingletonTypeComponent(adapterFunc);
            CanRegisterNamedTransientTypeComponent(adapterFunc);

            CanRegisterFluentDefaultTypeComponent(adapterFunc);
            CanRegisterFluentSingletonTypeComponent(adapterFunc);
            CanRegisterFluentTransientTypeComponent(adapterFunc);
        }
    }
}
