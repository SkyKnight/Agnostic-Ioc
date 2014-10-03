using System;
using Cardinal.IoC.Registration;
using Cardinal.IoC.UnitTests.Helpers;
using NUnit.Framework;

namespace Cardinal.IoC.UnitTests.Registration
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
            containerManager.Adapter.Register<IDependantClass, DependantClass>();
            IDependantClass dependantClass1 = containerManager.Resolve<IDependantClass>();
            IDependantClass dependantClass2 = containerManager.Resolve<IDependantClass>();
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
