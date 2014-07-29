using Cardinal.IoC.Registration;
using Cardinal.IoC.UnitTests.Helpers;
using Cardinal.IoC.UnitTests.TestAdapters;
using Cardinal.IoC.Windsor;
using Castle.MicroKernel;
using Castle.Windsor;
using NUnit.Framework;

namespace Cardinal.IoC.UnitTests.Registration
{
    [TestFixture]
    public class BasicRegistrationTests
    {
        [Test]
        public void TestSimpleRegistration()
        {
            IContainerManager containerManager = new ContainerManager(new EmptyWindsorContainerAdapter());
            Assert.IsNull(containerManager.TryResolve<IDependantClass>());

            containerManager.Register(new RegistrationDefinition<IDependantClass, DependantClass>());
            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependantClass);
        }

        [Test]
        public void TestSimpleNamedRegistration()
        {
            IContainerManager containerManager = new ContainerManager(new EmptyWindsorContainerAdapter());

            const string dependencyName = "dependantReg";

            containerManager.Register(new RegistrationDefinition<IDependantClass, DependantClass>());
            containerManager.Register(new NamedRegistrationDefinition<IDependantClass, DependantClass2>(dependencyName));
            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependantClass);

            IDependantClass dependantClass2 = containerManager.Resolve<IDependantClass>(dependencyName);
            Assert.IsNotNull(dependantClass2);
            Assert.AreEqual(typeof(DependantClass2), dependantClass2.GetType());
            Assert.AreEqual(TestConstants.DependantClassName, dependantClass.Name);
            Assert.AreEqual(TestConstants.DependantClass2Name, dependantClass2.Name);
        }

        [Test]
        public void TestSimpleInstanceRegistration()
        {
            IContainerManager containerManager = new ContainerManager(new EmptyWindsorContainerAdapter());
            Assert.IsNull(containerManager.TryResolve<IDependantClass>());

            DependantClass instanceDependantClass = new DependantClass();
            containerManager.Register(new InstanceRegistrationDefinition<IDependantClass, DependantClass>(instanceDependantClass));
            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.AreEqual(instanceDependantClass, dependantClass);
        }
    }
}
