using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lxs.Core.Infrastructure;
using Lxs.Data;

namespace Lxs.Admin.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //if (true)
            //{
            //    //var dataProviderInstance = EngineContext.Current.Resolve<BaseDataProviderManager>().LoadDataProvider();

            //    var dataProviderInstance = new SqlServerDataProvider();
            //    dataProviderInstance.InitDatabase();
            //}
            return View();
        }

    }
}
