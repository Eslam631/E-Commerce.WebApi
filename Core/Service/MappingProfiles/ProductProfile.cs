using AutoMapper;
using Domain.Models;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
   public class ProductProfile:Profile
    {
        public ProductProfile() {

            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.BrandName, Option => Option.MapFrom(Src => Src.ProductBrand.Name))
                .ForMember(dist=>dist.TypeName,Option=>Option.MapFrom(src=>src.ProductType.Name))
                .ForMember(dist=>dist.PictureUrl,Option=>Option.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandProductDto>();
            CreateMap<ProductType, TypeProductDto>();
        
        
        
        } 
    }
}
