using Cardinal.IoC.Registration;
using StructureMap.Configuration.DSL.Expressions;
using StructureMap.Pipeline;

namespace Cardinal.IoC.StructureMap
{
    public static class AdapterExtensions
    {
        public static SmartInstance<T1, T2> SetLifeStyle<T1, T2>(this SmartInstance<T1, T2> registration, LifetimeScope lifeTimeKey) where T1 : T2
        {
            switch (lifeTimeKey)
            {
                case LifetimeScope.Unowned:
                    return registration.Transient();
                case LifetimeScope.Singleton:
                    return registration.Singleton();
                case LifetimeScope.PerHttpRequest:
                    return registration.Transient();
                case LifetimeScope.PerThread:
                    return registration.Transient();
                default:
                    return registration.Transient();
            }
        }
        
        public static CreatePluginFamilyExpression<TPluginType> SetLifeStyle<TPluginType>(this CreatePluginFamilyExpression<TPluginType> registration, LifetimeScope lifeTimeKey)
        {
            switch (lifeTimeKey)
            {
                case LifetimeScope.Unowned:
                    return registration.Transient();
                case LifetimeScope.Singleton:
                    return registration.Singleton();
                case LifetimeScope.PerHttpRequest:
                    return registration.Transient();
                case LifetimeScope.PerThread:
                    return registration.Transient();
                default:
                    return registration.Transient();
            }   
        }

        public static ConfiguredInstance SetLifeStyle(this ConfiguredInstance registration, LifetimeScope lifeTimeKey)
        {
            switch (lifeTimeKey)
            {
                case LifetimeScope.Unowned:
                    return registration.Transient();
                case LifetimeScope.Singleton:
                    return registration.Singleton();
                case LifetimeScope.PerHttpRequest:
                    return registration.Transient();
                case LifetimeScope.PerThread:
                    return registration.Transient();
                default:
                    return registration.Transient();
            }
        }
    }
}
