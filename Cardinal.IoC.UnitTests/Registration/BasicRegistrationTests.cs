using System;
using Cardinal.IoC.Registration;
using Cardinal.IoC.UnitTests.Helpers;
using Castle.MicroKernel;
using NUnit.Framework;

namespace Cardinal.IoC.UnitTests.Registration
{
    [TestFixture]
    public class BasicRegistrationTests
    {
        [Test]
        [ExpectedException(typeof(ComponentNotFoundException))]
        public void TestSimpleRegistration()
        {
            IContainerManager containerManager = new ContainerManager(TestConstants.EmptyWindsorContainerName);
            Assert.IsNull(containerManager.Resolve<IDependantClass>());

            containerManager.Register(new RegistrationDefinition<IDependantClass, DependantClass>());
            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependantClass);
        }

        [Ignore("This needs implementing")]
        [Test]
        [ExpectedException(typeof(ComponentNotFoundException))]
        public void TestSimpleNamedRegistration()
        {
            IContainerManager containerManager = new ContainerManager(TestConstants.EmptyWindsorContainerName);
            Assert.IsNull(containerManager.Resolve<IDependantClass>());

            const string dependencyName = "dependantReg";

            containerManager.Register(new NamedRegistrationDefinition<IDependantClass, DependantClass>(dependencyName));
            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.IsNull(dependantClass);

            dependantClass = containerManager.Resolve<IDependantClass>(dependencyName);
            Assert.IsNotNull(dependantClass);
            Assert.AreEqual(TestConstants.DependantClassName, dependantClass.Name);
        }

        [Ignore("This needs implementing")]
        [Test]
        public void TestSimpleInstanceRegistration()
        {
        }
    }
}
