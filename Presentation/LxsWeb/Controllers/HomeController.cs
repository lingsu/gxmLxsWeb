using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lxs.Core;
using Lxs.Core.Domain.Catalog;
using Lxs.Data;

namespace LxsWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //var lxsContext = new LxsObjectContext("gxm");
            //IRepository<Category> sb = new EfRepository<Category>(lxsContext);
            //var model = sb.Table.ToList();
            return View();
        }

    }
}
