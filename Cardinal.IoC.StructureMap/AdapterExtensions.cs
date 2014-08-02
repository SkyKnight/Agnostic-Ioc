using Cardinal.IoC.Registration;
using StructureMap.Configuration.DSL.Expressions;

namespace Cardinal.IoC.StructureMap
{
    public static class AdapterExtensions
    {
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
    }
}
