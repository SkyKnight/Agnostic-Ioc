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
using Agnostic.Ioc.Autofac;
using Agnostic.IoC.Ninject;
using Agnostic.IoC.StructureMap;
using Agnostic.IoC.Unity;
using Agnostic.IoC.Windsor;
using Castle.Windsor;
using Microsoft.Practices.Unity;
using Ninject;
using StructureMap;

namespace Agnostic.IoC.UnitTests.Registration
{
    internal static class ContainerAdapterFactory
    {
        internal static IContainerAdapter GetAutofacContainerAdapter()
        {
            string containerKey = Guid.NewGuid().ToString();
            return new AutofacContainerAdapter(containerKey);
        }

        internal static IContainerAdapter GetStructureMapContainerAdapter()
        {
            IContainer container = new Container();
            return new StructureMapContainerAdapter(Guid.NewGuid().ToString(), container);
        }

        internal static IContainerAdapter GetUnityContainerAdapter()
        {
            IUnityContainer container = new UnityContainer();
            return new UnityContainerAdapter(Guid.NewGuid().ToString(), container);
        }

        internal static IContainerAdapter GetWindsorContainerAdapter()
        {
            IWindsorContainer container = new WindsorContainer();
            return new WindsorContainerAdapter(Guid.NewGuid().ToString(), container);
        }

        internal static IContainerAdapter GetNinjectContainerAdapter()
        {
            IKernel kernel = new StandardKernel();
            return new NinjectContainerAdapter(Guid.NewGuid().ToString(), kernel);
        }
    }
}
