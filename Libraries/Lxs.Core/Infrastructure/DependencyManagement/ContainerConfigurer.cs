using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lxs.Core.Configuration;

namespace Lxs.Core.Infrastructure.DependencyManagement
{
    public class ContainerConfigurer
    {
        public virtual void Configure(IEngine engine, ContainerManager containerManager, NopConfig configuration)
        {
            //other dependencies
            containerManager.AddComponentInstance<NopConfig>(configuration, "lxs.configuration");
            containerManager.AddComponentInstance<IEngine>(engine, "lxs.engine");
            containerManager.AddComponentInstance<ContainerConfigurer>(this, "lxs.containerConfigurer");

            //type finder
            containerManager.AddComponent<ITypeFinder, WebAppTypeFinder>("lxs.typeFinder");

            //register dependencies provided by other assemblies
            var typeFinder = containerManager.Resolve<ITypeFinder>();
            containerManager.UpdateContainer(x =>
            {
                var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
                var drInstances = new List<IDependencyRegistrar>();
                foreach (var drType in drTypes)
                    drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
                //sort
                drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
                foreach (var dependencyRegistrar in drInstances)
                    dependencyRegistrar.Register(x, typeFinder);
            });

        }
    }
}
