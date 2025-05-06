using AutoMapper;
using Domain.Models.IdentityModel;
using Domain.Models.OrderModules;
using Shared.DataTransferObject.IdentityDto;
using Shared.DataTransferObject.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
   public class OrderProfile:Profile
    {

        public OrderProfile()
        {
            CreateMap<AddressDto, OrderAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dist => dist.DeliveryMethod, Option => Option.MapFrom(Sourxe => Sourxe.DeliveryMethod.ShortName));

            CreateMap<OrderItem, OrderItemDto>().
                ForMember(dist => dist.ProductName, Option => Option.MapFrom(Source => Source.ProductItemOrdered.ProductName))
                .ForMember(dist=>dist.Picture,Option=>Option.MapFrom<OrderPictureResolver>());
              
        }
    }
}
