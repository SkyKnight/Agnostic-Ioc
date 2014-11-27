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
using StructureMap.Pipeline;

namespace Agnostic.IoC.StructureMap
{
    public static class AdapterExtensions
    {
        public static SmartInstance<T1, T2> SetLifeStyle<T1, T2>(this SmartInstance<T1, T2> registration, LifetimeScope lifeTimeKey) where T1 : T2
        {
            switch (lifeTimeKey)
            {
                case LifetimeScope.Unowned:
                    return registration.Transient();
                case LifetimeScope.Singleton:
                    return registration.Singleton();
                case LifetimeScope.PerHttpRequest:
                    return registration.Transient();
                case LifetimeScope.PerThread:
                    return registration.Transient();
                default:
                    return registration.Transient();
            }
        }

        public static ConfiguredInstance SetLifeStyle(this ConfiguredInstance registration, LifetimeScope lifeTimeKey)
        {
            switch (lifeTimeKey)
            {
                case LifetimeScope.Unowned:
                    return registration.Transient();
                case LifetimeScope.Singleton:
                    return registration.Singleton();
                case LifetimeScope.PerHttpRequest:
                    return registration.Transient();
                case LifetimeScope.PerThread:
                    return registration.Transient();
                default:
                    return registration.Transient();
            }
        }
    }
}
