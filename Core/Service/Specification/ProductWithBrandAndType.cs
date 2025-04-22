using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification
{
    internal class ProductWithBrandAndType:BaseSpecification<Product,int>
    {
        public ProductWithBrandAndType(int? BrandId,int? TypeId):base(P=>(!BrandId.HasValue ||P.BrandId==BrandId)&&
      (!TypeId.HasValue||P.TypeId==TypeId)
            )
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
            
        }
        public ProductWithBrandAndType(int id) : base(P=>P.Id==id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

        }
    }
}
