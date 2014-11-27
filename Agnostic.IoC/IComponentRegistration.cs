using System;
using Agnostic.IoC.Registration;

namespace Agnostic.IoC
{
    public interface IComponentRegistration
    {
        IComponentRegistration Lifetime(LifetimeScope lifetimeScope);

        IComponentRegistrationDefinition Definition { get; }

        IComponentRegistration As<T>();

        IComponentRegistration Instance<T>(T instance);

        IComponentRegistration Register<T1>();

        IComponentRegistration Register<T1, T2>();

        IComponentRegistration Register<T1, T2, T3>();

        IComponentRegistration Named(string name);
    }
}
