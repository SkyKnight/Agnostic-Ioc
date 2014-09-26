using System;
using Cardinal.IoC.Registration;

namespace Cardinal.IoC
{
    public class ComponentRegistration : IComponentRegistration
    {
        public ComponentRegistration() : this(new ComponentRegistrationDefinition())
        {
            
        }

        public ComponentRegistration(IComponentRegistrationDefinition definition)
        {
            Definition = definition;
        }

        public IComponentRegistration Lifetime(LifetimeScope lifetimeScope)
        {
            Definition.LifetimeScope = lifetimeScope;
            return this;
        }

        public IComponentRegistration Register<T1>()
        {
            Definition.Types = new[] { typeof(T1) };
            return this;
        }

        public IComponentRegistration Register<T1, T2>()
        {
            Definition.Types = new [] { typeof(T1), typeof(T2) };
            return this;
        }

        public IComponentRegistration Register<T1, T2, T3>()
        {
            Definition.Types = new[] { typeof(T1), typeof(T2), typeof(T3) };
            return this;
        }
        
        public IComponentRegistrationDefinition Definition { get; private set; }

        public IComponentRegistration As<T>()
        {
            Definition.ReturnType = typeof(T);
            return this;
        }
    }

    public interface IComponentRegistration
    {
        IComponentRegistration Lifetime(LifetimeScope lifetimeScope);

        IComponentRegistrationDefinition Definition { get; }

        IComponentRegistration As<T>();

        IComponentRegistration Register<T1>();

        IComponentRegistration Register<T1, T2>();

        IComponentRegistration Register<T1, T2, T3>();
    }

    public class ComponentRegistrationDefinition : IComponentRegistrationDefinition
    {
        public Type[] Types { get; set; }

        public Type ReturnType { get; set; }

        public LifetimeScope LifetimeScope { get; set; }

        public string Name { get; set; }
    }

    public interface IComponentRegistrationDefinition
    {
        Type[] Types { get; set; }

        string Name { get; set; }

        Type ReturnType { get; set; }

        LifetimeScope LifetimeScope { get; set; }
    }
}
