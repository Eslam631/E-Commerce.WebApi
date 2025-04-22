using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification
{
    internal class ProductWithBrandAndType:BaseSpecification<Product,int>
    {
        public ProductWithBrandAndType(int? BrandId,int? TypeId,ProductOptionSorting optionSorting):base(P=>(!BrandId.HasValue ||P.BrandId==BrandId)&&
      (!TypeId.HasValue||P.TypeId==TypeId)
            )
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

            switch (optionSorting)
            {
                case ProductOptionSorting.NameAsc:
                    AddOrderByAsec(P => P.Name);
                    break;
                case ProductOptionSorting.NameDesc:
                   AddOrderByDesc(P => P.Name);
                    break;
                case ProductOptionSorting.PriceAsc:
                    AddOrderByAsec(P => P.Price);
                    break;
                case ProductOptionSorting.PriceDesc:
                    AddOrderByDesc(P => P.Price);
                    break;
                default:
                    break;

            }
            
        }
        public ProductWithBrandAndType(int id) : base(P=>P.Id==id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

        }
    }
}
