using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agnostic.IoC.Registration;

namespace Agnostic.IoC
{
    public abstract class ContainerRegistrar : IContainerRegistrar
    {
        public TAssemblyRegistration CreateAssemblyRegistration<TAssemblyRegistration>() where TAssemblyRegistration : IAssemblyRegistration, new()
        {
            throw new NotImplementedException();
        }

        public virtual TComponentRegistrationType CreateComponentRegistration<TComponentRegistrationType>() where TComponentRegistrationType : IComponentRegistration, new()
        {
            return new TComponentRegistrationType();
        }

        public abstract void Register(IAssemblyRegistration registration);

        public virtual void Register(IComponentRegistration registration)
        {
            throw new NotImplementedException();
        }

        public abstract void Register<TRegisteredAs>(Func<TRegisteredAs> factory) where TRegisteredAs : class;

        public abstract void Register<TRegisteredAs>(Func<IContainerResolver, TRegisteredAs> factory) where TRegisteredAs : class;

        public abstract void Register<TRegisteredAs>(TRegisteredAs instance) where TRegisteredAs : class;

        public abstract void Register<TRegisteredAs>(LifetimeScope lifetimeScope, Func<TRegisteredAs> factory) where TRegisteredAs : class;

        public abstract void Register<TRegisteredAs>(LifetimeScope lifetimeScope, Func<IContainerResolver, TRegisteredAs> factory) where TRegisteredAs : class;

        public abstract void Register<TRegisteredAs>(string name, TRegisteredAs instance) where TRegisteredAs : class;

        public abstract void Register<TRegisteredAs>(LifetimeScope lifetimeScope, string name, Func<IContainerResolver, TRegisteredAs> factory) where TRegisteredAs : class;

        public abstract void Register<TRegisteredAs>(LifetimeScope lifetimeScope, string name, Func<TRegisteredAs> factory) where TRegisteredAs : class;

        public abstract void Register<TRegisteredAs, TResolvedTo>()
            where TRegisteredAs : class
            where TResolvedTo : TRegisteredAs;

        public abstract void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope)
            where TRegisteredAs : class
            where TResolvedTo : TRegisteredAs;

        public abstract void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope, string name)
            where TRegisteredAs : class
            where TResolvedTo : TRegisteredAs;
    }
}
