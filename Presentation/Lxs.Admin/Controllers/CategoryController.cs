using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lxs.Admin.Models.Catalog;
using Lxs.Core.Domain.Catalog;
using Lxs.Web.Framework.Kendoui;

namespace Lxs.Admin.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/

        public ActionResult List()
        {
            var model = new CategoryListModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult List(DataSourceRequest command, CategoryListModel model)
        {
            
            //var categories = _categoryService.GetAllCategories(model.SearchCategoryName,
            //    command.Page - 1, command.PageSize, true);
            //var gridModel = new DataSourceResult
            //{
            //    Data = categories.Select(x =>
            //    {
            //        var categoryModel = x.ToModel();
            //        categoryModel.Breadcrumb = x.GetFormattedBreadCrumb(_categoryService);
            //        return categoryModel;
            //    }),
            //    Total = categories.TotalCount
            //};

            var gridModel = new DataSourceResult();
            var categories = new List<Category>();

            for (int i = 0; i < 30; i++)
            {
                categories.Add(new Category()
                {
                    Id = i,
                    Name = "name"+i

                });
            }
            if (model.SearchCategoryName != null)
            {
                categories = categories.Where(x => x.Name.Contains(model.SearchCategoryName)).ToList();
            }
            gridModel.Data = categories.Skip((command.Page-1) * 10).Take(10).Select(x =>
            {
                var categoryModel = new CategoryModel() {Id = x.Id, Name = x.Name,DisplayOrder = x.DisplayOrder,Published = x.Published};
                return categoryModel;
            });
            

            int TotalPages = categories.Count / 10;

            if (categories.Count % 10 > 0)
                TotalPages++;

            gridModel.Total = TotalPages;
            return Json(gridModel);
        }

    }
}
