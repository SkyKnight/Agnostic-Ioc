using Cardinal.IoC.Registration;
using Ninject.Syntax;

namespace Cardinal.IoC.Ninject
{
    public static class AdapterExtensions
    {
        public static IBindingNamedWithOrOnSyntax<T> SetLifeStyle<T>(this IBindingInSyntax<T> registration, LifetimeScope lifeTimeKey)
        {
            switch (lifeTimeKey)
            {
                case LifetimeScope.Unowned:
                    return registration.InTransientScope();
                case LifetimeScope.Singleton:
                    return registration.InSingletonScope();
                case LifetimeScope.PerHttpRequest:
                    return registration.InTransientScope();
                case LifetimeScope.PerThread:
                    return registration.InThreadScope();
                default:
                    return registration.InTransientScope();
            }
        }
    }
}
