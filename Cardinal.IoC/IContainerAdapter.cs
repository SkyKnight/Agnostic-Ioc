using System.Collections;
using Cardinal.IoC.Registration;

namespace Cardinal.IoC
{
    public interface IContainerAdapter<out TContainer> : IContainerAdapter
    {
        TContainer Container { get; }
    }

    public interface IContainerAdapter
    {
        T Resolve<T>();
        
        T TryResolve<T>();

        T Resolve<T>(string name);

        T TryResolve<T>(string name);

        T Resolve<T>(IDictionary arguments);

        T TryResolve<T>(IDictionary arguments);

        T Resolve<T>(string name, IDictionary arguments);

        T TryResolve<T>(string name, IDictionary arguments);

        void Setup();

        string Name { get; }

        void Register<TRegisteredAs, TResolvedTo>(
            IRegistrationDefinition<TRegisteredAs, TResolvedTo> registrationDefinition) where TRegisteredAs : class
            where TResolvedTo : TRegisteredAs;

        void RegisterNamed<TRegisteredAs, TResolvedTo>(
            IRegistrationDefinition<TRegisteredAs, TResolvedTo> registrationDefinition, string name)
            where TRegisteredAs : class
            where TResolvedTo : TRegisteredAs;
    }
}
