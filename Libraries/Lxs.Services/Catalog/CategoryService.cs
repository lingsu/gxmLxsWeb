using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lxs.Core;
using Lxs.Core.Domain.Catalog;

namespace Lxs.Services.Catalog
{
    public class CategoryService:ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : category ID
        /// </remarks>
        private const string CATEGORIES_BY_ID_KEY = "Nop.category.id-{0}";
        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Gets a category
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <returns>Category</returns>
        public virtual Category GetCategoryById(int categoryId)
        {
            if (categoryId == 0)
                return null;

            string key = string.Format(CATEGORIES_BY_ID_KEY, categoryId);
             return _categoryRepository.GetById(categoryId);
        }

        public IPagedList<Category> GetAllCategories(string categoryName = "", int pageIndex = 0, int pageSize = Int32.MaxValue,
            bool showHidden = false)
        {
            var query = _categoryRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.Published);
            if (!String.IsNullOrWhiteSpace(categoryName))
                query = query.Where(c => c.Name.Contains(categoryName));
            query = query.Where(c => !c.Deleted);
            query = query.OrderBy(c => c.ParentCategoryId).ThenBy(c => c.DisplayOrder);

            //if (!showHidden && (!_catalogSettings.IgnoreAcl || !_catalogSettings.IgnoreStoreLimitations))
            //{
            //    if (!_catalogSettings.IgnoreAcl)
            //    {
            //        //ACL (access control list)
            //        var allowedCustomerRolesIds = _workContext.CurrentCustomer.CustomerRoles
            //            .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
            //        query = from c in query
            //                join acl in _aclRepository.Table
            //                on new { c1 = c.Id, c2 = "Category" } equals new { c1 = acl.EntityId, c2 = acl.EntityName } into c_acl
            //                from acl in c_acl.DefaultIfEmpty()
            //                where !c.SubjectToAcl || allowedCustomerRolesIds.Contains(acl.CustomerRoleId)
            //                select c;
            //    }
            //    if (!_catalogSettings.IgnoreStoreLimitations)
            //    {
            //        //Store mapping
            //        var currentStoreId = _storeContext.CurrentStore.Id;
            //        query = from c in query
            //                join sm in _storeMappingRepository.Table
            //                on new { c1 = c.Id, c2 = "Category" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
            //                from sm in c_sm.DefaultIfEmpty()
            //                where !c.LimitedToStores || currentStoreId == sm.StoreId
            //                select c;
            //    }

            //    //only distinct categories (group by ID)
            //    query = from c in query
            //            group c by c.Id
            //                into cGroup
            //                orderby cGroup.Key
            //                select cGroup.FirstOrDefault();
            //    query = query.OrderBy(c => c.ParentCategoryId).ThenBy(c => c.DisplayOrder);
            //}

            var unsortedCategories = query.ToList();

            //sort categories
            var sortedCategories = unsortedCategories.SortCategoriesForTree();

            //paging
            return new PagedList<Category>(sortedCategories, pageIndex, pageSize);
        }
    }
}
