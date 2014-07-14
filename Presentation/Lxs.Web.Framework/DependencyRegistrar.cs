using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Lxs.Core;
using Lxs.Core.Configuration;
using Lxs.Core.Infrastructure.DependencyManagement;
using Lxs.Data;
using Lxs.Services.Catalog;

namespace Lxs.Web.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.Register(c =>(new HttpContextWrapper(HttpContext.Current) as HttpContextBase))
                .As<HttpContextBase>()
                .InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerHttpRequest();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerHttpRequest();

           // builder.RegisterModule(new AutofacWebTypesModule());


            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerHttpRequest();

            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            //data layer
            builder.Register<IDbContext>(c => new LxsObjectContext("Data Source=.;Initial Catalog=lxsweb;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=123456;MultipleActiveResultSets=True")).InstancePerHttpRequest();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerHttpRequest();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerHttpRequest();
        }

        public int Order
        {
            get { return 0; }
        }
    }

    //public class SettingsSource : IRegistrationSource
    //{
    //    static readonly MethodInfo BuildMethod = typeof(SettingsSource).GetMethod(
    //        "BuildRegistration",
    //        BindingFlags.Static | BindingFlags.NonPublic);

    //    public bool IsAdapterForIndividualComponents
    //    {
    //        get { return false; }
    //    }

    //    public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
    //    {
    //        var ts = service as TypedService;
    //        if (ts != null && typeof(ISettings).IsAssignableFrom(ts.ServiceType))
    //        {
    //            var buildMethod = BuildMethod.MakeGenericMethod(ts.ServiceType);
    //            yield return (IComponentRegistration)buildMethod.Invoke(null, null);
    //        }
    //    }
    //    static IComponentRegistration BuildRegistration<TSettings>() where TSettings : ISettings, new()
    //    {
    //        return RegistrationBuilder
    //            .ForDelegate((c, p) =>
    //            {
    //                var currentStoreId = c.Resolve<IStoreContext>().CurrentStore.Id;
    //                //uncomment the code below if you want load settings per store only when you have two stores installed.
    //                //var currentStoreId = c.Resolve<IStoreService>().GetAllStores().Count > 1
    //                //    c.Resolve<IStoreContext>().CurrentStore.Id : 0;

    //                //although it's better to connect to your database and execute the following SQL:
    //                //DELETE FROM [Setting] WHERE [StoreId] > 0
    //                return c.Resolve<ISettingService>().LoadSetting<TSettings>(currentStoreId);
    //            })
    //            .InstancePerHttpRequest()
    //            .CreateRegistration();
    //    }
    //}
}
