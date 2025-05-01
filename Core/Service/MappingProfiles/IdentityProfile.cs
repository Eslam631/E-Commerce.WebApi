using AutoMapper;
using Domain.Models.IdentityModel;
using Shared.DataTransferObject.IdentityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
   public class IdentityProfile:Profile
    {
        public IdentityProfile() {
        
            CreateMap<Address,AddressDto>().ReverseMap();
        
        }
    }
}
