using Domain.Exceptions;
using Domain.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityDto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthService(UserManager<ApplicationUser> _userManager,IConfiguration _configuration) : IAuthService
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
                    Token =await CreateTokenAsync(user)
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
                    Token =await CreateTokenAsync(user)
                };
            }
            else {

                var error = CreateUser.Errors.Select(R => R.Description).ToList();

                throw new BadRequestException(error); 
            }

        }

        private  async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new (ClaimTypes.Email,user.Email),
                new (ClaimTypes.Name,user.UserName),
                new (ClaimTypes.NameIdentifier,user.Id),
            };

            var Roles = await _userManager.GetRolesAsync(user);

            foreach (var role in Roles)
                Claims.Add(new(ClaimTypes.Role, role));


            var SecretKey = _configuration.GetSection("JWTOption")["SecretKey"];

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            var Creds = new SigningCredentials(Key,SecurityAlgorithms.HmacSha256) ;

            var Token = new JwtSecurityToken(claims:Claims,
           issuer: _configuration.GetSection("JWTOption")["Issuer"],
            audience: _configuration["JWTOption:Audience"],
            signingCredentials:Creds,
            expires:DateTime.Now.AddHours(1));

            return new JwtSecurityTokenHandler().WriteToken(Token);

        }
    }
}
