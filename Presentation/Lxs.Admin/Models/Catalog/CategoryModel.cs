using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lxs.Web.Framework.Mvc;

namespace Lxs.Admin.Models.Catalog
{
    public class CategoryModel : BaseLxsEntityModel
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public bool Published { get; set; }
        public string Breadcrumb { get; set; }

        public IList<SelectListItem> AvailableCategoryTemplates { get; set; }
        public IList<SelectListItem> AvailableCategories { get; set; }
        //discounts
       // public List<DiscountModel> AvailableDiscounts { get; set; }
        public int[] SelectedDiscountIds { get; set; }
    }
}