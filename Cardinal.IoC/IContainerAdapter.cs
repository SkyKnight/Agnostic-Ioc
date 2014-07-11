using System.Collections;

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

        T Resolve<T>(IDictionary arguments);

        T Resolve<T>(string name, IDictionary arguments);

        void Setup();

        string Name { get; }
    }
}
