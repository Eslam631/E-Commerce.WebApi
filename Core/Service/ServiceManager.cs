using AutoMapper;
using Domain.Contracts;
using Domain.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager(IUnitOfWork _unitOfWork,IMapper _mapper,IBasketRepository basketRepository,UserManager<ApplicationUser> _userManager ,IConfiguration _configuration,IMapper mapper) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyServiceProvider=new Lazy<IProductService>(()=>new ProductService(_unitOfWork,_mapper));
       
        private readonly Lazy<IBasketService>  _LazyBasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, _mapper));

        private readonly Lazy<IAuthService> _LazyAuthService = new Lazy<IAuthService>(() => new AuthService(_userManager,_configuration,mapper));
        public IProductService ProductService => _LazyServiceProvider.Value;

        public IBasketService BasketService => _LazyBasketService.Value;

        public IAuthService AuthService => _LazyAuthService.Value;
    }
}
