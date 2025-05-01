using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
  public class AuthenticationController(IServiceManager _serviceManager):ApiBaseController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> LoginAsync(LoginDto loginDto)
        {
            var userDto = await _serviceManager.AuthService.LoginAsync(loginDto);

            return Ok(userDto);

        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> RegisterAsync(RegisterDto registerDto) 
        {

            var userDto = await _serviceManager.AuthService.RegisterAsync(registerDto);

            return Ok(userDto);


        }

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string Email)
        {
            var Result=await _serviceManager.AuthService.CheckEmailAsync(Email);
            return Ok(Result);
        }

        [HttpGet("CurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUserAsync()
        {
            var email=User.FindFirstValue(ClaimTypes.Email);
            var userDto = await _serviceManager.AuthService.GetCurrentUserAsync(email!);

            return Ok(email);

        }

        [HttpGet("Address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var AddressDto = await _serviceManager.AuthService.GetCurrentUserAddressAsync(email!);
            return Ok(AddressDto);
        }

        [Authorize]
        [HttpPut("Address")]

        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var AddressDto = await _serviceManager.AuthService.UpdateCurrentUserAddressAsync(email!,addressDto);

            return Ok(AddressDto);   
        }


    }
}
