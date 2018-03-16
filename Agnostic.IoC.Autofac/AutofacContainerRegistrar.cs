using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agnostic.IoC.Registration;
using Autofac;

namespace Agnostic.IoC.Autofac
{
    public class AutofacContainerRegistrar : ContainerRegistrar
    {
        private ContainerBuilder _containerBuilder;

        public AutofacContainerRegistrar(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder;
        }

        public override void Register(IAssemblyRegistration registration)
        {
            var definition = registration.Definition;

            if (definition.Assembly == null)
                return;

            var regBuilder = _containerBuilder.RegisterAssemblyTypes(definition.Assembly);
            if (definition.AssignableTo != null)
                regBuilder = regBuilder.AssignableTo(definition.AssignableTo);
            if (definition.Filter != null)
                regBuilder = regBuilder.Where(type => definition.Filter(type));
            if (definition.RegistrationMode == AssemblyRegistrationMode.AsImplementedInterfaces)
                regBuilder = regBuilder.AsImplementedInterfaces();
            else
                regBuilder = regBuilder.AsSelf();
            regBuilder.SetLifeStyle(definition.LifetimeScope);
        }

        public override void Register<TRegisteredAs>(Func<IContainerResolver, TRegisteredAs> factory)
        {
            _containerBuilder.Register(context => factory(new AutofacContainerResolver(context)));
        }

        public override void Register<TRegisteredAs>(Func<TRegisteredAs> factory)
        {
            Register(resolver => factory());
        }

        public override void Register<TRegisteredAs>(TRegisteredAs instance)
        {
            _containerBuilder.RegisterInstance(instance).As<TRegisteredAs>();
        }

        public override void Register<TRegisteredAs>(LifetimeScope lifetimeScope, Func<IContainerResolver, TRegisteredAs> factory)
        {
            _containerBuilder.Register(context => factory(new AutofacContainerResolver(context))).SetLifeStyle(lifetimeScope);
        }

        public override void Register<TRegisteredAs>(LifetimeScope lifetimeScope, Func<TRegisteredAs> factory)
        {
            Register(lifetimeScope, resolver => factory());
        }

        public override void Register<TRegisteredAs>(string name, TRegisteredAs instance)
        {
            _containerBuilder.RegisterInstance(instance).Named<TRegisteredAs>(name);
        }

        public override void Register<TRegisteredAs>(LifetimeScope lifetimeScope, string name, Func<TRegisteredAs> factory)
        {
            Register(lifetimeScope, name, resolve => factory());
        }

        public override void Register<TRegisteredAs>(LifetimeScope lifetimeScope, string name, Func<IContainerResolver, TRegisteredAs> factory)
        {
            _containerBuilder.Register(context => factory(new AutofacContainerResolver(context))).Named<TRegisteredAs>(name).SetLifeStyle(lifetimeScope);
        }

        public override void Register<TRegisteredAs, TResolvedTo>()
        {
            _containerBuilder.RegisterType<TResolvedTo>().As<TRegisteredAs>();
        }

        public override void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope)
        {
            _containerBuilder.RegisterType<TResolvedTo>().As<TRegisteredAs>().SetLifeStyle(lifetimeScope);
        }

        public override void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope, string name)
        {
            _containerBuilder.RegisterType<TResolvedTo>().Named<TRegisteredAs>(name).SetLifeStyle(lifetimeScope);
        }
    }
}
