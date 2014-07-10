using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Autofac;
using Lxs.Core.Configuration;
using Lxs.Core.Infrastructure.DependencyManagement;

namespace Lxs.Core.Infrastructure
{
    public class LxsEngine:IEngine
    {
        #region Fields

        private ContainerManager _containerManager;

        #endregion
        

        public LxsEngine() 
            : this(new ContainerConfigurer())
		{
		}
        public LxsEngine(ContainerConfigurer configurer)
		{
            var config = ConfigurationManager.GetSection("NopConfig") as NopConfig;
            InitializeContainer(configurer, config);
		}
      
        public void Initialize(NopConfig config)
        {
            //startup tasks
            if (!config.IgnoreStartupTasks)
            {
               RunStartupTasks();
            }
        }

        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }

        #region Properties

        public ContainerManager ContainerManager
        {
            get { return _containerManager; }
        }

        #endregion

        #region Utilities

        private void RunStartupTasks()
        {
            var typeFinder = _containerManager.Resolve<ITypeFinder>();
            var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            var startUpTasks = new List<IStartupTask>();
            foreach (var startUpTaskType in startUpTaskTypes)
                startUpTasks.Add((IStartupTask)Activator.CreateInstance(startUpTaskType));
            //sort
            startUpTasks = startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();
            foreach (var startUpTask in startUpTasks)
                startUpTask.Execute();
        }

        private void InitializeContainer(ContainerConfigurer configurer, NopConfig config)
        {
            var builder = new ContainerBuilder();

            _containerManager = new ContainerManager(builder.Build());
            configurer.Configure(this, _containerManager, config);
        }
        #endregion
    }
}
