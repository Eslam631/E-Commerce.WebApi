using Domain.Models.ProductModel;
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
        public ProductWithBrandAndType(ProductQueryParams queryParams):base(P=>(!queryParams.BrandId.HasValue ||P.BrandId==queryParams.BrandId)
        
        &&(!queryParams.TypeId.HasValue||P.TypeId==queryParams.TypeId)
        &&(string.IsNullOrWhiteSpace(queryParams.Search) ||P.Name.ToLower().Contains(queryParams.Search.ToLower()))
            )
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

            switch (queryParams.Sort)
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

            ApplyPagination(queryParams.PageNumber, queryParams.PageSize);
            
        }
        public ProductWithBrandAndType(int id) : base(P=>P.Id==id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

        }
    }
}
