using System;
using System.Collections;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;

namespace Cardinal.IoC.Autofac
{
    public abstract class AutofacContainerAdapter : ContainerAdapter<IContainer>
    {
        protected AutofacContainerAdapter()
        {

        }

        protected AutofacContainerAdapter(IContainer container) : base(container)
        {
        }

        protected abstract void RegisterComponents(ContainerBuilder builder);

        /// <summary>
        /// 
        /// </summary>
        public override void Setup()
        {
            ContainerBuilder builder = new ContainerBuilder();
            RegisterComponents(builder);
            Container = builder.Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override T Resolve<T>()
        {
            if (!Container.IsRegistered<T>())
            {
                throw new ArgumentException("Component not registered");
            }
            return Container.Resolve<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public override T Resolve<T>(string name)
        {
            if (!Container.IsRegisteredWithName<T>(name))
            {
                throw new ArgumentException("Component not registered with provided name", name);
            }

            return Container.ResolveNamed<T>(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public override T Resolve<T>(IDictionary arguments)
        {
            if (!Container.IsRegistered<T>())
            {
                throw new ArgumentException("Component not registered");
            }

            List<NamedParameter> parameters = new List<NamedParameter>();

            foreach (var key in arguments.Keys)
            {
                parameters.Add(new NamedParameter(key, arguments[key]));
            }

            Parameter[] array = parameters.ToArray();

            return Container.Resolve<T>(array);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public override T Resolve<T>(string name, IDictionary arguments)
        {
            if (!Container.IsRegisteredWithName<T>(name))
            {
                throw new ArgumentException("Component not registered with provided name", name);
            }

            List<NamedParameter> parameters = new List<NamedParameter>();

            foreach (var key in arguments.Keys)
            {
                parameters.Add(new NamedParameter(key, arguments[key]));
            }

            Parameter[] array = parameters.ToArray();

            return Container.ResolveNamed<T>(name, array);
        }
    }
}
