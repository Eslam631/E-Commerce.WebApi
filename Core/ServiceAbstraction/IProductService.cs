using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductService
    {

        public Task<IEnumerable<ProductDto>> GetAllProductAsync(int? BrandId,int? TypeId);

        public Task<ProductDto> GetProductByIdAsync(int id);
         
        public Task<IEnumerable<TypeProductDto>> GetAllTypeAsync();
        public Task<IEnumerable<BrandProductDto>> GetAllBrandAsync();

    }
}
