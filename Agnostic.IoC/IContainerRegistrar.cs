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

using Agnostic.IoC.Registration;
using System;

namespace Agnostic.IoC
{
    public interface IContainerRegistrar
    {
        void Register<TRegisteredAs, TResolvedTo>()
            where TRegisteredAs : class
            where TResolvedTo : TRegisteredAs;

        void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope)
            where TRegisteredAs : class
            where TResolvedTo : TRegisteredAs;

        //void Register<TRegisteredAs, TResolvedTo>(string name)
        //    where TRegisteredAs : class
        //    where TResolvedTo : TRegisteredAs;

        void Register<TRegisteredAs, TResolvedTo>(LifetimeScope lifetimeScope, string name)
            where TRegisteredAs : class
            where TResolvedTo : TRegisteredAs;

        void Register<TRegisteredAs>(TRegisteredAs instance)
            where TRegisteredAs : class;

        void Register<TRegisteredAs>(string name, TRegisteredAs instance)
            where TRegisteredAs : class;

        void Register<TRegisteredAs>(Func<TRegisteredAs> factory)
            where TRegisteredAs : class;

        //void Register<TRegisteredAs>(string name, Func<TRegisteredAs> factory)
        //    where TRegisteredAs : class;

        void Register<TRegisteredAs>(LifetimeScope lifetimeScope, Func<TRegisteredAs> factory)
            where TRegisteredAs : class;

        void Register<TRegisteredAs>(LifetimeScope lifetimeScope, string name, Func<TRegisteredAs> factory)
            where TRegisteredAs : class;

        void Register<TRegisteredAs>(Func<IContainerResolver, TRegisteredAs> factory)
            where TRegisteredAs : class;

        //void Register<TRegisteredAs>(string name, Func<IContainerResolver, TRegisteredAs> factory)
        //    where TRegisteredAs : class;

        void Register<TRegisteredAs>(LifetimeScope lifetimeScope, Func<IContainerResolver, TRegisteredAs> factory)
            where TRegisteredAs : class;

        void Register<TRegisteredAs>(LifetimeScope lifetimeScope, string name, Func<IContainerResolver, TRegisteredAs> factory)
            where TRegisteredAs : class;

        //void RegisterGroup(IContainerManagerGroupRegistration groupRegistration);

        void Register(IComponentRegistration registration);

        TComponentRegistrationType CreateComponentRegistration<TComponentRegistrationType>() where TComponentRegistrationType : IComponentRegistration, new();
    }
}
