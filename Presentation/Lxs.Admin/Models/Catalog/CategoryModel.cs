using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lxs.Web.Framework.Mvc;

namespace Lxs.Admin.Models.Catalog
{
    public class CategoryModel : BaseLxsEntityModel
    {
        public CategoryModel()
        {
            AvailableCategories = new List<SelectListItem>();
        }
        [Required(ErrorMessage = "请输入分类名称")]
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public bool Published { get; set; }
        public string Breadcrumb { get; set; }

        public IList<SelectListItem> AvailableCategoryTemplates { get; set; }
        public IList<SelectListItem> AvailableCategories { get; set; }
        //discounts
       // public List<DiscountModel> AvailableDiscounts { get; set; }
        public int[] SelectedDiscountIds { get; set; }
        public int PageSize { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Picture { get; set; }
        public int ParentCategoryId { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
    }
}