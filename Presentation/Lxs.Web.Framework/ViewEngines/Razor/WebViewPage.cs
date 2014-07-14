using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Lxs.Web.Framework.ViewEngines.Razor
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
       
    }



    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }
}
