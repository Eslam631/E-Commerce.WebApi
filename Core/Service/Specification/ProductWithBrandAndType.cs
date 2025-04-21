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
        public ProductWithBrandAndType():base(null)
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
