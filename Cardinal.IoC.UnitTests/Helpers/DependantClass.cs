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

using System;

namespace Cardinal.IoC.UnitTests.Helpers
{
    public class DependantClass3 : IDependantClass
    {
        public DependantClass3()
        {
            Name = TestConstants.DependantClass3Name;
        }

        public string Name { get; set; }
    }

    public class DependantClass2 : IDependantClass
    {
        public DependantClass2()
        {
            Name = TestConstants.DependantClass2Name;
        }

        public string Name { get; set; }
    }

    public class DependantClass : ISuperDependantClass, IOtherDependantClass
    {
        public DependantClass()
        {
            Name = TestConstants.DependantClassName;
        }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Other { get; set; }
    }

    public interface IOtherDependantClass : IDependantClass
    {
        string Other { get; set; }
    }

    public interface ISuperDependantClass : IDependantClass
    {
        DateTime Date { get; set; }
    }

    public interface IDependantClass
    {
        string Name { get; set; }
    }
}
