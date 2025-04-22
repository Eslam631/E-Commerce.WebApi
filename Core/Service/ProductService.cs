using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Service.Specification;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandProductDto>> GetAllBrandAsync()
        {
           var Brands=await _unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
        return  _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandProductDto>>(Brands);

             
        }
        public async Task<IEnumerable<TypeProductDto>> GetAllTypeAsync( )
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeProductDto>>(Types);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductAsync(ProductQueryParams queryParams)
        {
            var specification = new ProductWithBrandAndType( queryParams );
            var Products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(specification);
          return  _mapper.Map<IEnumerable<Product>,IEnumerable< ProductDto>>(Products);
        }


        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var specification=new ProductWithBrandAndType(id);
            var Product=await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(specification);
            return _mapper.Map<Product, ProductDto>(Product);
        }
    }
}
