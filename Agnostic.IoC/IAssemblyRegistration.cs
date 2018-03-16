﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Agnostic.IoC
{
    public interface IAssemblyRegistration
    {
        IAssemblyRegistrationDefinition Definition { get; }
    }
}
