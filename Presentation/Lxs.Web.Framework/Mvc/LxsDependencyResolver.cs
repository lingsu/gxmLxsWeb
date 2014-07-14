using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Lxs.Core.Infrastructure;

namespace Lxs.Web.Framework.Mvc
{
    public class LxsDependencyResolver : IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            var model=EngineContext.Current.ContainerManager.ResolveOptional(serviceType);
            return model;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var type = typeof(IEnumerable<>).MakeGenericType(serviceType);
            return (IEnumerable<object>)EngineContext.Current.Resolve(type);
        }
    }
}
