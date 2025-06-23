using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Application.DTOs.Products;
using ECommerce.Domain.EcommerceDbEntities;

namespace ECommerce.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, PaginationProductsDTO>()
               .ForMember(dest => dest.IdCoin, opt => opt.Ignore());

        }
    }
}
