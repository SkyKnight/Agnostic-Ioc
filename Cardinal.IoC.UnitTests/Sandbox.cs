using System;
using Cardinal.IoC.UnitTests.Helpers;
using Cardinal.IoC.Windsor;
using Castle.Windsor;
using NUnit.Framework;

namespace Cardinal.IoC.UnitTests
{
    [TestFixture]
    [Ignore("Sandbox Tests")]
    public class Sandbox
    {
        [Test]
        public void ExtendedRegistration()
        {
            IWindsorContainer container = new WindsorContainer();
            IContainerAdapter adapter = new WindsorContainerAdapter(Guid.NewGuid().ToString(), container);
            /*var registration = new ComponentRegistration();
            registration.Register<IDependantClass, ISuperDependantClass>().As<DependantClass>();*/

            var registration = adapter.CreateComponentRegistration<ComponentRegistration>()
                .Register<IDependantClass, ISuperDependantClass>()
                .As<DependantClass>();
            adapter.Register(registration);
            Assert.AreEqual(adapter.Resolve<IDependantClass>().GetType(), adapter.Resolve<ISuperDependantClass>().GetType());
            Assert.AreEqual(typeof(DependantClass), adapter.Resolve<ISuperDependantClass>().GetType());
            Assert.AreEqual(typeof(DependantClass), adapter.Resolve<IDependantClass>().GetType());
        }
    }
}
