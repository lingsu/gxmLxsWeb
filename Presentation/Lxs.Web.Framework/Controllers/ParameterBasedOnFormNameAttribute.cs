﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Lxs.Web.Framework.Controllers
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple = true)]
    public class ParameterBasedOnFormNameAttribute:FilterAttribute,IActionFilter
    {
        private readonly string _name;
        private readonly string _actionParameterName;
        public ParameterBasedOnFormNameAttribute(string name, string actionParameterName)
        {
            this._name = name;
            this._actionParameterName = actionParameterName;
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var fromValue = filterContext.RequestContext.HttpContext.Request.Form[_name];
            filterContext.ActionParameters[_actionParameterName] = !string.IsNullOrEmpty(fromValue);
        }
    }
}
