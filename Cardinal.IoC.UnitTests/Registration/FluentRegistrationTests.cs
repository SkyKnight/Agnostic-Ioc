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

        protected void CanRegisterOneFluentTypes(Func<IContainerAdapter> adapterFunc)
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
