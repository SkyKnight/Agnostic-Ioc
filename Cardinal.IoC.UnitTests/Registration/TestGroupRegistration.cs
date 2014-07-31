using Cardinal.IoC.Registration;
using Cardinal.IoC.UnitTests.Helpers;

namespace Cardinal.IoC.UnitTests.Registration
{
    public class TestGroupRegistration : IContainerManagerGroupRegistration
    {
        public void RegisterComponents(IContainerManager containerManager)
        {
            containerManager.Register(new RegistrationDefinition<IDependantClass, DependantClass>());
            containerManager.Register(new NamedRegistrationDefinition<IDependantClass, DependantClass2>("new name"));
        }
    }
}
