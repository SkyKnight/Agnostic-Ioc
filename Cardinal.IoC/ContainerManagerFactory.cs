using System;
using System.Collections.Generic;

namespace Cardinal.IoC
{
    public static class ContainerManagerFactory
    {
        private static readonly Dictionary<string, IContainerManager> containerManagers = new Dictionary<string, IContainerManager>();

        public static IContainerManager GetContainerManager()
        {
            return GetContainerManager(String.Empty);
        }

        public static IContainerManager GetContainerManager(string name)
        {
            if (containerManagers.ContainsKey(name))
            {
                return containerManagers[name];
            }

            IContainerManager containerManager = new ContainerManager(name);
            containerManagers.Add(name, containerManager);
            return containerManager;
        }
    }
}
