using AutoMapper;
using ECommerce.Application.DTOs.Products;
using ECommerce.Application.Features.Products.Queries;
using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.ValueObject.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, PaginationProductsDTO>()
               .ForMember(dest => dest.IdCoin, opt => opt.Ignore());

            CreateMap<GetProductFilterQuery, ProductFilterValue>();
        }
    }
}
