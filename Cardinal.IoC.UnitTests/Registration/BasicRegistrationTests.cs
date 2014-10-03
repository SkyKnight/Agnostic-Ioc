using System;
using System.Linq;
using Cardinal.IoC.Registration;
using Cardinal.IoC.UnitTests.Helpers;
using Cardinal.IoC.UnitTests.TestClasses;
using NUnit.Framework;

namespace Cardinal.IoC.UnitTests.Registration
{
    public partial class RegistrationTests
    {
        protected void TestSimpleRegistration(Func<IContainerAdapter> adapterFunc)
        {
            IContainerManager containerManager = new ContainerManager(adapterFunc());
            Assert.IsNull(containerManager.TryResolve<IDependantClass>());

            containerManager.Adapter.Register<IDependantClass, DependantClass>();
            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependantClass);
        }

        protected void TestMultipleSimpleRegistrationsResolvesFirst(Func<IContainerAdapter> adapterFunc)
        {
            IContainerManager containerManager = new ContainerManager(adapterFunc());
            Assert.IsNull(containerManager.TryResolve<IDependantClass>());

            containerManager.Adapter.Register<IDependantClass, DependantClass2>();
            containerManager.Adapter.Register<IDependantClass, DependantClass>();
            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependantClass);
            Assert.AreEqual(typeof(DependantClass2), dependantClass.GetType());
        }

        protected void TestSimpleNamedRegistration(Func<IContainerAdapter> adapterFunc)
        {
            IContainerManager containerManager = new ContainerManager(adapterFunc());

            const string dependencyName = "dependantReg";

            containerManager.Adapter.Register<IDependantClass, DependantClass>();
            containerManager.Adapter.Register<IDependantClass, DependantClass2>(dependencyName);
            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependantClass);

            IDependantClass dependantClass2 = containerManager.Resolve<IDependantClass>(dependencyName);
            Assert.IsNotNull(dependantClass2);
            Assert.AreEqual(typeof(DependantClass2), dependantClass2.GetType());
            Assert.AreEqual(TestConstants.DependantClassName, dependantClass.Name);
            Assert.AreEqual(TestConstants.DependantClass2Name, dependantClass2.Name);
        }

        protected void CanRegisterBasicType(Func<IContainerAdapter> adapterFunc)
        {
            IContainerManager containerManager = new ContainerManager(adapterFunc());
            Assert.IsNull(containerManager.TryResolve<IDependantClass>());

            DependantClass instanceDependantClass = new DependantClass();
            containerManager.Adapter.Register<IDependantClass>(instanceDependantClass);
            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.AreEqual(instanceDependantClass, dependantClass);
        }

        protected void CanRegisterGroup(Func<IContainerAdapter> adapterFunc)
        {
            IContainerManager containerManager = new ContainerManager(adapterFunc());
            Assert.IsNull(containerManager.TryResolve<IDependantClass>());

            IContainerManagerGroupRegistration groupRegistration = new TestGroupRegistration();
            containerManager.Adapter.RegisterGroup(groupRegistration);

            IDependantClass dependantClass = containerManager.Resolve<IDependantClass>();
            Assert.IsNotNull(dependantClass);
            Assert.AreEqual(typeof(DependantClass), dependantClass.GetType());
        }

        protected void EnsureRegistrationOrderCorrect(Func<IContainerAdapter> adapterFunc)
        {
            IContainerManager containerManager = new ContainerManager(adapterFunc());

            containerManager.Adapter.Register<IDependantClass, DependantClass>();
            containerManager.Adapter.Register<IDependantClass, DependantClass2>();
            containerManager.Adapter.Register<IDependantClass, DependantClass3>();
            containerManager.Adapter.Register<IDependantClass, DependantClass2>();

            IDependantClass[] resolved = containerManager.ResolveAll<IDependantClass>().ToArray();
            Assert.AreEqual(typeof(DependantClass), resolved[0].GetType());
            Assert.AreEqual(typeof(DependantClass2), resolved[1].GetType());
            Assert.AreEqual(typeof(DependantClass3), resolved[2].GetType());
            Assert.AreEqual(typeof(DependantClass2), resolved[3].GetType());
        }
    }
}
