using Agnostic.IoC.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Agnostic.IoC
{
    public static class ContainerRegistrarExtensions
    {
        public static void Register<TRegisteredAs, TResolvedTo>(this IContainerRegistrar registrar, string name) where TRegisteredAs : class where TResolvedTo : TRegisteredAs
        {
            registrar.Register<TRegisteredAs, TResolvedTo>(LifetimeScope.Transient, name);
        }

        public static void RegisterGroup(this IContainerRegistrar registrar, IContainerManagerGroupRegistration groupRegistration)
        {
            groupRegistration.RegisterComponents(registrar);
        }

        public static void Register<TRegisteredAs>(this IContainerRegistrar registrar, string name, Func<TRegisteredAs> factory) where TRegisteredAs : class
        {
            registrar.Register(LifetimeScope.Transient, name, factory);
        }

        public static void Register<TRegisteredAs>(this IContainerRegistrar registrar, string name, Func<IContainerResolver, TRegisteredAs> factory) where TRegisteredAs : class
        {
            registrar.Register(LifetimeScope.Transient, name, factory);
        }

        public static IAssemblyRegistration CreateAssemblyRegistration(this IContainerRegistrar registrar)
        {
            return registrar.CreateAssemblyRegistration<AssemblyRegistration>();
        }

        public static IAssemblyRegistration CreateFromAssembly(this IContainerRegistrar registrar, Assembly assembly)
        {
            return registrar.CreateAssemblyRegistration().FromAssembly(assembly);
        }
    }
}
