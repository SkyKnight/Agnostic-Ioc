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

using System;
using System.IO;
using Agnostic.IoC.UnitTests.Helpers;
using Agnostic.IoC.Windsor;
using Castle.Windsor;
using NUnit.Framework;

namespace Agnostic.IoC.UnitTests
{
    [TestFixture]
    public class ContainerAdapterTests
    {
        [Test]
        public void AddAdapterToFactory()
        {
            IContainerAdapterAccessor adapterAccessor = new ContainerAdapterAccessor();
            string newAdapterKey = Guid.NewGuid().ToString();

            IContainerAdapter adapter = adapterAccessor.GetAdapter(newAdapterKey);
            Assert.IsNull(adapter);
            IWindsorContainer container = new WindsorContainer();
            IContainerAdapter addedAdapter = new WindsorContainerAdapter(container);
            adapterAccessor.AddAdapter(newAdapterKey, addedAdapter);

            IContainerAdapterAccessor newAdapterAccessor = new ContainerAdapterAccessor();
            IContainerAdapter newAdapter = newAdapterAccessor.GetAdapter(newAdapterKey);
            Assert.IsNotNull(newAdapter);
            Assert.AreEqual(addedAdapter, newAdapter);
        }

        [Test]
        public void GetAssemblyScannedAdapter()
        {
            IContainerAdapterAccessor adapterAccessor = new ContainerAdapterAccessor(true);
            IContainerAdapter adapter = adapterAccessor.GetAdapter(TestConstants.AutofacContainerName);
            Assert.IsNotNull(adapter);
        }

        [Test]
        //[ExpectedException(typeof(InvalidDataException))]
        public void AddAdapterTwice()
        {
            IContainerAdapterAccessor adapterAccessor = new ContainerAdapterAccessor();
            string newAdapterKey = Guid.NewGuid().ToString();

            IContainerAdapter adapter = adapterAccessor.GetAdapter(newAdapterKey);
            Assert.IsNull(adapter);

            IWindsorContainer container = new WindsorContainer();
            IContainerAdapter addedAdapter = new WindsorContainerAdapter(container);
            adapterAccessor.AddAdapter(newAdapterKey, addedAdapter);

            IWindsorContainer newContainer = new WindsorContainer();
            IContainerAdapter newAdapter = new WindsorContainerAdapter(newContainer);
            adapterAccessor.AddAdapter(newAdapterKey, addedAdapter);
            IContainerAdapter retrievedAdapter = adapterAccessor.GetAdapter(newAdapterKey);
            Assert.IsNotNull(newAdapter);
            Assert.AreEqual(retrievedAdapter, newAdapter);
        }
    }
}
