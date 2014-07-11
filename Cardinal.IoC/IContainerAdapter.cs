namespace Cardinal.IoC
{
    public interface IContainerAdapter<out TContainer> : IContainerAdapter
    {
        TContainer Container { get; }
    }

    public interface IContainerAdapter
    {
        T Resolve<T>();

        T Resolve<T>(string name);

        void Setup();

        string Name { get; }
    }
}
