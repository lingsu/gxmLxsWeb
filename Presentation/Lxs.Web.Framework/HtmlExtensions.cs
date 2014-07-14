using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace Lxs.Web.Framework
{
    public static class HtmlExtensions
    {
        public static string FieldIdFor<T, TResult>(this HtmlHelper<T> html, Expression<Func<T, TResult>> expression)
        {
            var id = html.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
            // because "[" and "]" aren't replaced with "_" in GetFullHtmlFieldId
            return id.Replace('[', '_').Replace(']', '_');
        }

        public static MvcHtmlString LxsCheckBoxFor<T, TResult>(this HtmlHelper<T> helper, Expression<Func<T, TResult>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string name = ExpressionHelper.GetExpressionText(expression);
            //string fullHtmlFieldName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            var ischecked = metadata.Model is bool ? (bool) metadata.Model : false;

            TagBuilder label = new TagBuilder("label");
            TagBuilder input = new TagBuilder("input");
            TagBuilder span = new TagBuilder("span");
            TagBuilder hiddeninput = new TagBuilder("input");
            input.GenerateId(metadata.PropertyName);
            input.MergeAttribute("name", name);
            input.MergeAttribute("type", "checkbox");
            input.MergeAttribute("value", "true");
            input.MergeAttribute("class", "ace");
            if (ischecked)
            {
                input.MergeAttribute("checked", "checked");
            }

            hiddeninput.MergeAttribute("name", name);
            hiddeninput.MergeAttribute("type", "hidden");
            hiddeninput.MergeAttribute("value", "false");
            label.InnerHtml += input;

            span.MergeAttribute("class", "lbl");
            label.InnerHtml += span;
            label.InnerHtml += hiddeninput;

            return MvcHtmlString.Create(label.ToString());
        }
    }
}
