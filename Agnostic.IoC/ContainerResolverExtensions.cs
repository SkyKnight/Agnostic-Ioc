using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agnostic.IoC
{
    public static class ContainerResolverExtensions
    {
        public static T TryResolve<T>(this IContainerResolver resolver, Type t, string name) where T : class
        {
            return resolver.Resolve(t, name) as T;
        }

        /// <summary>
        /// Attempts to resolve the requested type with arguments
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        /// <typeparam name="T">
        /// The requested type
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T TryResolve<T>(this IContainerResolver resolver, string name, IDictionary<string, object> arguments)
        {
            try
            {
                return resolver.Resolve<T>(name, arguments);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Attempts to resolve the requested type
        /// </summary>
        /// <typeparam name="T">
        /// The requested type
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T TryResolve<T>(this IContainerResolver resolver)
        {
            try
            {
                return resolver.Resolve<T>();
            }
            catch
            {
                return default(T);
            }
        }

        public static T Resolve<T>(this IContainerResolver resolver, Type t) where T : class
        {
            return resolver.Resolve(t) as T;
        }

        public static T TryResolve<T>(this IContainerResolver resolver, Type t) where T : class
        {
            return resolver.TryResolve(t) as T;
        }

        public static T Resolve<T>(this IContainerResolver resolver, Type t, string name) where T : class
        {
            return resolver.Resolve(t, name) as T;
        }

        public static object TryResolve(this IContainerResolver resolver, Type t)
        {
            try
            {
                return resolver.Resolve(t);
            }
            catch
            {
                return null;
            }
        }

        public static object TryResolve(this IContainerResolver resolver, Type t, string name)
        {
            try
            {
                return resolver.Resolve(t, name);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Attempts to resolve the requested type by name
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <typeparam name="T">
        /// The requested type
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T TryResolve<T>(this IContainerResolver resolver, string name)
        {
            try
            {
                return resolver.Resolve<T>(name);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Attempts to resolve the requested type with arguments
        /// </summary>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        /// <typeparam name="T">
        /// The requested type
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T TryResolve<T>(this IContainerResolver resolver, IDictionary<string, object> arguments)
        {
            try
            {
                return resolver.Resolve<T>(arguments);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
