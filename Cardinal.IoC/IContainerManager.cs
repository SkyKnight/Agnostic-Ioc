using System.Collections;
using Cardinal.IoC.Registration;

namespace Cardinal.IoC
{
    public interface IContainerManager
    {
        /// <summary>
        /// Attempts to resolve the dependency
        /// </summary>
        /// <typeparam name="T">
        /// The type to resolve
        /// </typeparam>
        /// <returns>
        /// The resolved dependency
        /// </returns>
        T Resolve<T>();

        /// <summary>
        /// Attempts to resolve the dependency returning the default if failed
        /// </summary>
        /// <typeparam name="T">
        /// The type to resolve
        /// </typeparam>
        /// <returns>
        /// The resolved dependency or default
        /// </returns>
        T TryResolve<T>();

        /// <summary>
        /// Attempts to resolve the dependency
        /// </summary>
        /// <typeparam name="T">
        /// The type to resolve
        /// </typeparam>
        /// <param name="name">
        /// The component name
        /// </param>
        /// <returns>
        /// The resolved dependency
        /// </returns>
        T Resolve<T>(string name);

        /// <summary>
        /// Attempts to resolve the dependency returning the default if failed
        /// </summary>
        /// <typeparam name="T">
        /// The type to resolve
        /// </typeparam>
        /// <param name="name">
        /// The component name
        /// </param>
        /// <returns>
        /// The resolved dependency or default
        /// </returns>
        T TryResolve<T>(string name);

        /// <summary>
        /// Attempts to resolve the dependency
        /// </summary>
        /// <typeparam name="T">
        /// The type to resolve
        /// </typeparam>
        /// <param name="name">
        /// The component name
        /// </param>
        /// <param name="parameters">
        /// The arguments
        /// </param>
        /// <returns>
        /// The resolved dependency
        /// </returns>
        T Resolve<T>(string name, IDictionary parameters);

        /// <summary>
        /// Attempts to resolve the dependency returning the default if failed
        /// </summary>
        /// <typeparam name="T">
        /// The type to resolve
        /// </typeparam>
        /// <param name="name">
        /// The component name
        /// </param>
        /// <param name="arguments">
        /// The arguments
        /// </param>
        /// <returns>
        /// The resolved dependency or default
        /// </returns>
        T TryResolve<T>(string name, IDictionary arguments);

        /// <summary>
        /// Attempts to resolve the dependency
        /// </summary>
        /// <typeparam name="T">
        /// The type to resolve
        /// </typeparam>
        /// <returns>
        /// The resolved dependency
        /// </returns>
        T Resolve<T>(IDictionary parameters);

        /// <summary>
        /// Attempts to resolve the dependency returning the default if failed
        /// </summary>
        /// <typeparam name="T">
        /// The type to resolve
        /// </typeparam>
        /// <returns>
        /// The resolved dependency or default
        /// </returns>
        T TryResolve<T>(IDictionary parameters);

        /// <summary>
        /// Gets the current adapter
        /// </summary>
        IContainerAdapter CurrentAdapter { get; }

        void Register<TRegisteredAs, TResolvedTo>(IRegistrationDefinition<TRegisteredAs, TResolvedTo> registrationDefinition)  where TRegisteredAs : class
            where TResolvedTo : TRegisteredAs;
    }
}
