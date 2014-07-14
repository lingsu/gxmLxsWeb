using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lxs.Core;
using Lxs.Core.Domain.Catalog;

namespace Lxs.Services.Catalog
{
    public interface ICategoryService
    {
        Category GetCategoryById(int categoryId);
        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <param name="categoryName">Category name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Categories</returns>
        IPagedList<Category> GetAllCategories(string categoryName = "",
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);



        void InsertCategory(Category category);

        void UpdateCategory(Category category);
    }
}
