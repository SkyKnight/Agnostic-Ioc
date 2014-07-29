namespace Cardinal.IoC.UnitTests
{
    public interface IContainerTestSuite
    {
        void ResolveItemByInterfaceOnly();

        void ResolveItemByName();

        void ResolveItemWithParameters();

        void ResolveItemWithNameAndParameters();

        void UseExternalContainer();
    }
}
