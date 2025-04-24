using AutoMapper;
using Domain.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager(IUnitOfWork _unitOfWork,IMapper _mapper,IBasketRepository basketRepository ) : IServiceManager
    {
        private readonly Lazy<IProductService> _serviceProvider=new Lazy<IProductService>(()=>new ProductService(_unitOfWork,_mapper));
        public IProductService ProductService => _serviceProvider.Value;

        private readonly Lazy<IBasketService>  _basketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, _mapper));
        public IBasketService BasketService => _basketService.Value;
    }
}
