using Agnostic.IoC.Registration;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using System.Collections.Generic;

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

        /// <summary>
        /// Converts the generic IDictionary to an array of named parameters for the container to
        /// use in constructor resolution. See <see cref="NamedParameter"/>.
        /// </summary>
        /// <param name="arguments">The dictionary of arguments</param>
        /// <returns>The named parameter array</returns>
        internal static Parameter[] GetParametersFromDictionary(this IDictionary<string, object> arguments)
        {
            List<Parameter> parameters = new List<Parameter>(arguments.Keys.Count);

            foreach (string key in arguments.Keys)
            {
                NamedParameter p = new NamedParameter(key, arguments[key]);
                parameters.Add(p);
            }

            return parameters.ToArray();
        }
    }
}
