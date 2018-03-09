using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agnostic.IoC.Autofac
{
    public class AutofacContainerResolver : IContainerResolver
    {
        private IComponentContext _componentContext;

        public AutofacContainerResolver(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public object Resolve(Type t)
        {
            return _componentContext.Resolve(t);
        }

        public object Resolve(Type t, string name)
        {
            return _componentContext.ResolveNamed(name, t);
        }

        public T Resolve<T>()
        {
            return _componentContext.Resolve<T>();
        }

        public T Resolve<T>(IDictionary<string, object> arguments)
        {
            return _componentContext.Resolve<T>(arguments.GetParametersFromDictionary());
        }

        public T Resolve<T>(string name)
        {
            return _componentContext.ResolveNamed<T>(name);
        }

        public T Resolve<T>(string name, IDictionary<string, object> arguments)
        {
            return _componentContext.ResolveNamed<T>(name, arguments.GetParametersFromDictionary());
        }

        public IEnumerable<TResolvedTo> ResolveAll<TResolvedTo>()
        {
            return _componentContext.Resolve<IEnumerable<TResolvedTo>>();
        }
    }
}
