using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cardinal.Ioc.Autofac;
using Cardinal.IoC.StructureMap;
using Cardinal.IoC.Unity;
using Cardinal.IoC.Windsor;
using Castle.Windsor;
using Microsoft.Practices.Unity;
using StructureMap;

namespace Cardinal.IoC.UnitTests.Registration
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
    }
}
