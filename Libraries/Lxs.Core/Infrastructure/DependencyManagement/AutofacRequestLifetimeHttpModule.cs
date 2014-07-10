using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Autofac;

namespace Lxs.Core.Infrastructure.DependencyManagement
{
    public class AutofacRequestLifetimeHttpModule : IHttpModule
    {
        /// <summary>
        /// Tag used to identify registrations that are scoped to the HTTP request level.
        /// </summary>
        //in the previous versions of Autofac (for MVC3) it was set to "httpRequest"
        public static readonly object HttpRequestTag = "AutofacWebRequest";

        public void Init(HttpApplication context)
        {
            context.EndRequest += ContextEndRequest;
        }
        public static void ContextEndRequest(object sender, EventArgs e)
        {
            ILifetimeScope lifetimeScope = LifetimeScope;
            if (lifetimeScope != null)
                lifetimeScope.Dispose();
        }
        public void Dispose()
        {
        }
        public static ILifetimeScope GetLifetimeScope(ILifetimeScope container, Action<ContainerBuilder> configurationAction)
        {
            //little hack here to get dependencies when HttpContext is not available
            if (HttpContext.Current != null)
            {
                return LifetimeScope ?? (LifetimeScope = InitializeLifetimeScope(configurationAction, container));
            }
            else
            {
                //throw new InvalidOperationException("HttpContextNotAvailable");
                return InitializeLifetimeScope(configurationAction, container);
            }
        }
        static ILifetimeScope LifetimeScope
        {
            get
            {
                return (ILifetimeScope)HttpContext.Current.Items[typeof(ILifetimeScope)];
            }
            set
            {
                HttpContext.Current.Items[typeof(ILifetimeScope)] = value;
            }
        }
        static ILifetimeScope InitializeLifetimeScope(Action<ContainerBuilder> configurationAction, ILifetimeScope container)
        {
            return (configurationAction == null)
                ? container.BeginLifetimeScope(HttpRequestTag)
                : container.BeginLifetimeScope(HttpRequestTag, configurationAction);
        }
    }
}
