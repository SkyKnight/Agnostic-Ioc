using Agnostic.IoC.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Agnostic.IoC
{
    public enum AssemblyRegistrationMode
    {
        AsSelf,
        AsImplementedInterfaces
    }

    public interface IAssemblyRegistrationDefinition
    {
        Assembly Assembly { get; set; }

        Type AssignableTo { get; set; }

        Type RegisterTo { get; set; }

        Predicate<Type> Filter { get; set; }

        AssemblyRegistrationMode RegistrationMode { get; set; }

        LifetimeScope LifetimeScope { get; set; }
    }
}
