using Agnostic.IoC.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Agnostic.IoC
{
    public static class AssemblyRegistrationExtensions
    {
        public static IAssemblyRegistration FromAssembly(this IAssemblyRegistration registration, Assembly assembly)
        {
            registration.Definition.Assembly = assembly;
            return registration;
        }

        public static IAssemblyRegistration AssignableTo(this IAssemblyRegistration registration, Type type)
        {
            registration.Definition.AssignableTo = type;
            return registration;
        }

        public static IAssemblyRegistration AssignableTo<T>(this IAssemblyRegistration registration)
        {
            return registration.AssignableTo(typeof(T));
        }

        public static IAssemblyRegistration AsSelf(this IAssemblyRegistration registration)
        {
            registration.Definition.RegistrationMode = AssemblyRegistrationMode.AsSelf;
            return registration;
        }

        public static IAssemblyRegistration AsImplementedInterfaces(this IAssemblyRegistration registration)
        {
            registration.Definition.RegistrationMode = AssemblyRegistrationMode.AsImplementedInterfaces;
            return registration;
        }

        public static IAssemblyRegistration SetLifetimeScope(this IAssemblyRegistration registration, LifetimeScope lifetimeScope)
        {
            registration.Definition.LifetimeScope = lifetimeScope;
            return registration;
        }

        public static void Register(this IAssemblyRegistration assemblyRegistration, IContainerRegistrar registration)
        {
            registration.Register(assemblyRegistration);
        }
    }
}
