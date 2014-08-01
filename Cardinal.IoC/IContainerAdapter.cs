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

namespace Cardinal.IoC
{
    public interface IContainerAdapter<out TContainer> : IContainerAdapter
    {
        TContainer Container { get; }
    }

    public interface IContainerAdapter
    {
        T Resolve<T>();
        
        T TryResolve<T>();

        T Resolve<T>(string name);

        T TryResolve<T>(string name);

        T Resolve<T>(IDictionary<string, object> arguments);

        T TryResolve<T>(IDictionary<string, object> arguments);

        T Resolve<T>(string name, IDictionary<string, object> arguments);

        T TryResolve<T>(string name, IDictionary<string, object> arguments);

        void RegisterComponents();

        string Name { get; }

        void Register<TRegisteredAs, TResolvedTo>()
            where TRegisteredAs : class
            where TResolvedTo : TRegisteredAs;

        void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope) where TRegisteredAs : class
            where TResolvedTo : TRegisteredAs;

        void Register<TRegisteredAs, TResolvedTo>(string name)
            where TRegisteredAs : class
            where TResolvedTo : TRegisteredAs;

        void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope, string name)
            where TRegisteredAs : class
            where TResolvedTo : TRegisteredAs;

        void Register<TRegisteredAs, TResolvedTo>(TResolvedTo instance)
            where TRegisteredAs : class
            where TResolvedTo : TRegisteredAs;

        void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope, TResolvedTo instance)
            where TRegisteredAs : class
            where TResolvedTo : TRegisteredAs;

        void Register<TRegisteredAs, TResolvedTo>(string name, TResolvedTo instance)
            where TRegisteredAs : class
            where TResolvedTo : TRegisteredAs;

        void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope, string name, TResolvedTo instance)
            where TRegisteredAs : class
            where TResolvedTo : TRegisteredAs;

        void Register(IContainerManagerGroupRegistration groupRegistration);

        void RegisterAll<TRegisteredAs>();

        void RegisterAll<TRegisteredAs>(string assemblyName);

        IEnumerable<TResolvedTo> ResolveAll<TResolvedTo>();
    }
}
