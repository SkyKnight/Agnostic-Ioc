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

namespace Cardinal.IoC.UnitTests
{
    /// <summary>
    /// A basic suite of tests to ensure that the containers have at least the same base functionality
    /// </summary>
    public interface IContainerTestSuite
    {
        /// <summary>
        /// Tests resolving an item just passing in the interface
        /// </summary>
        void ResolveItemByInterfaceOnly();

        /// <summary>
        /// Tests resolving a type using the name 
        /// </summary>
        void ResolveItemByName();

        /// <summary>
        /// Tests resolving a type using the parameters passed in
        /// </summary>
        void ResolveItemWithParameters();

        /// <summary>
        /// Tests resolving a type using the name and parameters passed in
        /// </summary>
        void ResolveItemWithNameAndParameters();

        /// <summary>
        /// Tests initialising a container manager based on an existing container of the default type
        /// </summary>
        void UseExternalContainer();
    }
}
