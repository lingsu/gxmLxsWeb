using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Lxs.Admin.Models.Catalog;
using Lxs.Core.Domain.Catalog;
using Lxs.Core.Infrastructure;

namespace Lxs.Admin.Models.Infrastructure
{
    public class AutoMapperStartupTask:IStartupTask
    {
        public void Execute()
        {
            //category
            Mapper.CreateMap<Category, CategoryModel>()
                .ForMember(dest => dest.AvailableCategoryTemplates, mo => mo.Ignore())
                .ForMember(dest => dest.Breadcrumb, mo => mo.Ignore())
                .ForMember(dest => dest.AvailableCategories, mo => mo.Ignore())
                //.ForMember(dest => dest.AvailableDiscounts, mo => mo.Ignore())
                .ForMember(dest => dest.SelectedDiscountIds, mo => mo.Ignore())
                //.ForMember(dest => dest.SeName, mo => mo.MapFrom(src => src.GetSeName(0, true, false)))
               // .ForMember(dest => dest.AvailableCustomerRoles, mo => mo.Ignore())
               // .ForMember(dest => dest.SelectedCustomerRoleIds, mo => mo.Ignore())
               // .ForMember(dest => dest.AvailableStores, mo => mo.Ignore())
               // .ForMember(dest => dest.SelectedStoreIds, mo => mo.Ignore())
                .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());

            Mapper.CreateMap<CategoryModel, Category>()
                .ForMember(dest => dest.HasDiscountsApplied, mo => mo.Ignore())
                .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore())
                .ForMember(dest => dest.UpdatedOnUtc, mo => mo.Ignore())
                .ForMember(dest => dest.Deleted, mo => mo.Ignore());
            // .ForMember(dest => dest.AppliedDiscounts, mo => mo.Ignore());
        }

        public int Order
        {
            get { return 0; }
        }
    }
}