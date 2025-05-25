using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attribute;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class ProductsController(IServiceManager _serviceManager) : ApiBaseController
    {
        //Get All Product

        
        [HttpGet]
        [Cache]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetaAllProduct([FromQuery]ProductQueryParams productQuery)
        {
            var Products = await _serviceManager.ProductService.GetAllProductAsync( productQuery);
            return Ok(Products);
        }


        //Get Product By Id
        [HttpGet("{id:int}")]


       public async Task<ActionResult<ProductDto?>> GetProductId(int id)
        {
            var Product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(Product);
        }

        //Get All Types
        [HttpGet("types")]
        public async Task<ActionResult<TypeProductDto>> GetAllTypes()
        {
            var Types=await _serviceManager.ProductService.GetAllTypeAsync();

            return Ok(Types);
        }

        [HttpGet("brands")]

        public async Task<ActionResult<BrandProductDto>> GetAllBrand()
        {
            var Brand = await _serviceManager.ProductService.GetAllBrandAsync();
            return Ok(Brand);
        }


    }
}
