using Domain.Exceptions;
using Domain.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthService(UserManager<ApplicationUser> _userManager) : IAuthService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email) ?? throw new NotFoundEmailException(loginDto.Email); 



            var CheckPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (CheckPassword)
                return new UserDto
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token = CreateToken(user)
                };
            else
             throw new UnAuthorizedException();





        }

      

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {

            var user = new ApplicationUser()
            {
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                DisplayName = registerDto.DisplayName,
                UserName = registerDto.DisplayName,
            };
            var CreateUser=await _userManager.CreateAsync(user,registerDto.Password);

            if (CreateUser.Succeeded)
            {
                return new UserDto
                {
                    Email = registerDto.Email,
                    DisplayName = registerDto.DisplayName,
                    Token = CreateToken(user)
                };
            }
            else {

                var error = CreateUser.Errors.Select(R => R.Description).ToList();

                throw new BadRequestException(error); 
            }

        }

        private static string CreateToken(ApplicationUser user)
        {
            return "To-do";
        }
    }
}
