using Agnostic.IoC.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Agnostic.IoC
{
    public class AssemblyRegistrationDefinition : IAssemblyRegistrationDefinition
    {
        public Assembly Assembly { get; set; }

        public Type AssignableTo { get; set; }

        public Type RegisterTo { get; set; }

        public Predicate<Type> Filter { get; set; }

        public AssemblyRegistrationMode RegistrationMode { get; set; }

        public LifetimeScope LifetimeScope { get; set; }
    }
}
