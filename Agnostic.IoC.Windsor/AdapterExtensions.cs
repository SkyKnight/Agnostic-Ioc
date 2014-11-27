using Agnostic.IoC.Registration;
using Castle.MicroKernel.Registration;

namespace Agnostic.IoC.Windsor
{
    public static class AdapterExtensions
    {
        public static ComponentRegistration<T> SetLifeStyle<T>(this ComponentRegistration<T> registration, LifetimeScope lifeTimeKey) where T : class
        {
            switch (lifeTimeKey)
            {
                case LifetimeScope.Unowned:
                    return registration.LifestyleCustom<NoTrackLifestyleManager>();
                case LifetimeScope.Singleton:
                    return registration;
                case LifetimeScope.PerHttpRequest:
                    return registration.LifestylePerWebRequest();
                case LifetimeScope.PerThread:
                    return registration.LifestylePerThread();
                default:
                    return registration.LifestyleTransient();
            }
        }
    }
}
