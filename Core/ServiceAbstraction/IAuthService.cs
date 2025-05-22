using Shared.DataTransferObject.IdentityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
   public interface IAuthService
    {
        public Task<UserDto> LoginAsync(LoginDto loginDto);
        public Task<UserDto> RegisterAsync(RegisterDto registerDto);


        public Task<bool> CheckEmailAsync(string email);
        public Task<UserDto> GetCurrentUserAsync(string Email);
        public Task<AddressDto> GetCurrentUserAddressAsync(string Email);
        public Task<AddressDto> UpdateCurrentUserAddressAsync(string Email,AddressDto addressDto);
    }
}
