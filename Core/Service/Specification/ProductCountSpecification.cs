﻿using Domain.Models.ProductModel;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification
{
    internal class ProductCountSpecification:BaseSpecification<Product,int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams):base(P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId) &&
      (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId) && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || P.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {
            
        }
    }
}
