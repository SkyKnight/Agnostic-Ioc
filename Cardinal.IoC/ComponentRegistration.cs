﻿// --------------------------------------------------------------------------------------------------------------------
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

using Cardinal.IoC.Registration;

namespace Cardinal.IoC
{
    public class ComponentRegistration : IComponentRegistration
    {
        public ComponentRegistration()
            : this(new ComponentRegistrationDefinition())
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

        public IComponentRegistration Instance<T>(T instance)
        {
            Definition.ReturnType = typeof(T);
            Definition.Instance = instance;
            return this;
        }

        public IComponentRegistration Register<T1>()
        {
            Definition.Types = new[] { typeof(T1) };
            return this;
        }

        public IComponentRegistration Register<T1, T2>()
        {
            Definition.Types = new[] { typeof(T1), typeof(T2) };
            return this;
        }

        public IComponentRegistration Register<T1, T2, T3>()
        {
            Definition.Types = new[] { typeof(T1), typeof(T2), typeof(T3) };
            return this;
        }

        public IComponentRegistration Named(string name)
        {
            Definition.Name = name;
            return this;
        }

        public IComponentRegistrationDefinition Definition { get; private set; }

        public IComponentRegistration As<T>()
        {
            Definition.Instance = null;
            Definition.ReturnType = typeof(T);
            return this;
        }
    }
}
