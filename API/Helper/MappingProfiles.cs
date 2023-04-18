using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(p => p.productType, obj => obj.MapFrom(source => source.productType.Name))
                .ForMember(p => p.productBrand, obj => obj.MapFrom(source => source.productBrand.Name))
                .ForMember(p => p.PictureUrl, obj => obj.MapFrom<ProductUrlResolver>());
        }
    }
}