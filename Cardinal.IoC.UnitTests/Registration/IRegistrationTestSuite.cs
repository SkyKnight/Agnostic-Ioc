using NUnit.Framework;

namespace Cardinal.IoC.UnitTests.Registration
{
    public interface IRegistrationTestSuite
    {
        [Test]
        void TestSimpleRegistration();

        [Test]
        void TestSimpleNamedRegistration();

        [Test]
        void TestSimpleInstanceRegistration();

        [Test]
        void GroupRegistration();
    }
}