using System.Collections.Generic;

namespace Cardinal.IoC
{
    public interface IContainerManager
    {
        T Resolve<T>();

        T Resolve<T>(string name);

        IContainerAdapter CurrentAdapter { get; }
    }
}
