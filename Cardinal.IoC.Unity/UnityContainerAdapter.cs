using System.Collections;
using Cardinal.IoC.Registration;
using Microsoft.Practices.Unity;

namespace Cardinal.IoC.Unity
{
    public abstract class UnityContainerAdapter : ContainerAdapter<IUnityContainer>
    {
        protected UnityContainerAdapter() : this(new UnityContainer())
        {
        }

        protected UnityContainerAdapter(IUnityContainer container) : base(container)
        {
        }

        public override T Resolve<T>(string name)
        {
            return Container.Resolve<T>(name);
        }

        public override T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public override T Resolve<T>(string name, IDictionary arguments)
        {
            ParameterOverrides resolverOverride = GetParametersOverrideFromDictionary<T>(arguments);
            return Container.Resolve<T>(name, resolverOverride);
        }

        public override T Resolve<T>(IDictionary arguments)
        {
            ParameterOverrides resolverOverride = GetParametersOverrideFromDictionary<T>(arguments);
            return Container.Resolve<T>(resolverOverride);
        }

        public override void Register<TRegisteredAs, TResolvedTo>(IRegistrationDefinition<TRegisteredAs, TResolvedTo> registrationDefinition)
        {
            Container.RegisterType(typeof(TRegisteredAs), typeof(TResolvedTo));
        }

        public override void Register<TRegisteredAs, TResolvedTo>(IRegistrationDefinition<TRegisteredAs, TResolvedTo> registrationDefinition, string name)
        {
            Container.RegisterType(typeof(TRegisteredAs), typeof(TResolvedTo), name);
        }

        public override void Register<TRegisteredAs, TResolvedTo>(IRegistrationDefinition<TRegisteredAs, TResolvedTo> registrationDefinition, TResolvedTo instance)
        {
            Container.RegisterInstance(typeof(TRegisteredAs), instance);
        }

        public override void Register<TRegisteredAs, TResolvedTo>(IRegistrationDefinition<TRegisteredAs, TResolvedTo> registrationDefinition, string name, TResolvedTo instance)
        {
            Container.RegisterInstance(typeof(TRegisteredAs), name, instance);
        }

        private static ParameterOverrides GetParametersOverrideFromDictionary<T>(IDictionary arguments)
        {
            ParameterOverrides resolverOverride = new ParameterOverrides();
            foreach (string key in arguments.Keys)
            {
                resolverOverride.Add(key, arguments[key]);
            }
            return resolverOverride;
        }
    }
}
