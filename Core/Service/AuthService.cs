using AutoMapper;
using Domain.Exceptions;
using Domain.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class AuthService(UserManager<ApplicationUser> _userManager,IConfiguration _configuration,IMapper _mapper) : IAuthService
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
         var user=   await _userManager.FindByEmailAsync(email);

          return  user!=null?true: false;
        }


        public async Task<UserDto> GetCurrentUserAsync(string Email)
        {
         var User=  await _userManager.FindByEmailAsync(Email)??throw new NotFoundEmailException(Email);

            return new UserDto() { Email =User.Email, DisplayName=User.DisplayName,Token=await CreateTokenAsync(User)  };
            
        }

        public async Task<AddressDto> GetCurrentUserAddressAsync(string Email)
        {

            var User =await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(E => E.Email == Email)
                ??throw new NotFoundEmailException(Email);

            if (User.Address != null)
              return  _mapper.Map<Address, AddressDto>(User.Address);
            else
                throw new NotFoundAddressException(User.UserName);

           
        }
        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string Email, AddressDto addressDto)
        {
            var User = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(E => E.Email == Email)
              ?? throw new NotFoundEmailException(Email);

            if (User.Address != null) {
            
                User.Address.FirstName=addressDto.FirstName;
                 User.Address.LastName=addressDto.LastName;
                User.Address.City=addressDto.City;
                User.Address.Street=addressDto.Street;
                User.Address.Country=addressDto.Country;
            
            }
            else
            {
              User.Address= _mapper.Map<AddressDto,Address>(addressDto);
            }

           await _userManager.UpdateAsync(User);

            return _mapper.Map< AddressDto>(User.Address);

        }
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
