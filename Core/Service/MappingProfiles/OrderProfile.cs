using AutoMapper;
using Domain.Models.IdentityModel;
using Domain.Models.OrderModules;
using Shared.DataTransferObject.IdentityDto;
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
            CreateMap<AddressDto, OrderAddress>();
        }
    }
}
