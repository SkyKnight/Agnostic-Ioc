// --------------------------------------------------------------------------------------------------------------------
// Copyright (c) 2014, Simon Proctor and Nathanael Mann
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Cardinal.IoC.Registration;
using StructureMap;
using StructureMap.Pipeline;

namespace Cardinal.IoC.StructureMap
{
    public class StructureMapContainerAdapter : ContainerAdapter<IContainer>
    {
        protected StructureMapContainerAdapter()
            : this(new Container())
        {
        }

        public StructureMapContainerAdapter(IContainer container)
            : base(container)
        {
        }

        public override T Resolve<T>()
        {
            return Container.GetInstance<T>();
        }

        public override T Resolve<T>(string name)
        {
            return Container.GetInstance<T>(name);
        }

        public override T Resolve<T>(IDictionary<string, object> arguments)
        {
            ExplicitArguments explicitArguments = new ExplicitArguments(arguments);
            return Container.GetInstance<T>(explicitArguments);
        }

        public override T Resolve<T>(string name, IDictionary<string, object> arguments)
        {
            ExplicitArguments explicitArguments = new ExplicitArguments(arguments);
            return Container.GetInstance<T>(explicitArguments, name);
        }

        public override void Register<TRegisteredAs, TResolvedTo>(IRegistrationDefinition<TRegisteredAs, TResolvedTo> registrationDefinition)
        {
            Container.Configure(x => x.For<TRegisteredAs>().Use<TResolvedTo>());
        }

        public override void Register<TRegisteredAs, TResolvedTo>(IRegistrationDefinition<TRegisteredAs, TResolvedTo> registrationDefinition, string name)
        {
            Container.Configure(x => x.For<TRegisteredAs>().Use<TResolvedTo>().Named(name));
        }

        public override void Register<TRegisteredAs, TResolvedTo>(IRegistrationDefinition<TRegisteredAs, TResolvedTo> registrationDefinition, TResolvedTo instance)
        {
            Container.Configure(x => x.For<TRegisteredAs>().UseInstance(new ObjectInstance(instance)));
        }

        public override void Register<TRegisteredAs, TResolvedTo>(IRegistrationDefinition<TRegisteredAs, TResolvedTo> registrationDefinition, string name, TResolvedTo instance)
        {
            Container.Configure(x => x.For<TRegisteredAs>().UseInstance(new ObjectInstance(instance).Named(name)));
        }
    }
}
