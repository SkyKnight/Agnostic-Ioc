using Agnostic.IoC.Registration;
using Autofac.Builder;

namespace Agnostic.IoC.Autofac
{
    public static class AdapterExtensions
    {
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> SetLifeStyle<TLimit, TActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registration, LifetimeScope lifeTimeKey)
        {
            switch (lifeTimeKey)
            {
                case LifetimeScope.Unowned:
                    return registration.InstancePerDependency().ExternallyOwned();
                case LifetimeScope.Singleton:
                    return registration.SingleInstance();
                case LifetimeScope.PerHttpRequest:
                    // todo: fix this
                    return registration.InstancePerDependency();
                case LifetimeScope.PerThread:
                    return registration.InstancePerLifetimeScope();
                default:
                    return registration.InstancePerDependency();
            }
        }
    }
}
