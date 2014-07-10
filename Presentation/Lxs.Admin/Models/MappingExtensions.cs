using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Lxs.Admin.Models.Catalog;
using Lxs.Core.Domain.Catalog;

namespace Lxs.Admin.Models
{
    public static class MappingExtensions
    {
        #region Category

        public static CategoryModel ToModel(this Category entity)
        {
            return Mapper.Map<Category, CategoryModel>(entity);
        }

        public static Category ToEntity(this CategoryModel model)
        {
            return Mapper.Map<CategoryModel, Category>(model);
        }

        public static Category ToEntity(this CategoryModel model, Category destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion
    }
}