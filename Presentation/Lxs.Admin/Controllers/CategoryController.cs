using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lxs.Admin.Models;
using Lxs.Admin.Models.Catalog;
using Lxs.Core.Domain.Catalog;
using Lxs.Services.Catalog;
using Lxs.Web.Framework.Kendoui;

namespace Lxs.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult List()
        {
            var model = new CategoryListModel();
            return View(model);
        }
        public ActionResult Create()
        {
            var model = new CategoryModel();
            model.PageSize = 4;
            model.Published = true;
            PrepareAllCategoriesModel(model);

            return View(model);
        }
        [HttpPost]
        public ActionResult List(DataSourceRequest command, CategoryListModel model)
        {
            var categories = _categoryService.GetAllCategories(model.SearchCategoryName,
                 command.Page - 1, command.PageSize, true);
            var gridModel = new DataSourceResult
            {
                Data = categories.Select(x =>
                {
                    var categoryModel = x.ToModel();
                    categoryModel.Breadcrumb = x.GetFormattedBreadCrumb(_categoryService);
                    return categoryModel;
                }),
                Total = categories.TotalCount
            };
            return Json(gridModel);
        }


        [NonAction]
        protected void PrepareAllCategoriesModel(CategoryModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.AvailableCategories.Add(new SelectListItem()
            {
                Text = "顶级类",
                Value = "0"
            });
            var categories = _categoryService.GetAllCategories(showHidden: true);
            foreach (var c in categories)
            {
                model.AvailableCategories.Add(new SelectListItem()
                {
                    Text = c.GetFormattedBreadCrumb(categories),
                    Value = c.Id.ToString()
                });
            }
        }
    }
}
