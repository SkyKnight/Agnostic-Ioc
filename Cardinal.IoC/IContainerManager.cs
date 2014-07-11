namespace Cardinal.IoC
{
    public interface IContainerManager
    {
        T Resolve<T>();

        T Resolve<T>(string name);
    }
}
