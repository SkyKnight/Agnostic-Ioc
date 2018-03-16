using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agnostic.IoC
{
    public class AssemblyRegistration : IAssemblyRegistration
    {
        private AssemblyRegistrationDefinition definition = new AssemblyRegistrationDefinition();

        public IAssemblyRegistrationDefinition Definition
        {
            get
            {
                return definition;
            }
        }
    }
}
