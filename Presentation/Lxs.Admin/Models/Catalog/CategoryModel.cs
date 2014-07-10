using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lxs.Admin.Models.Catalog
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public bool Published { get; set; }
        public string Breadcrumb { get; set; }
    }
}