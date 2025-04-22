using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController(IServiceManager _serviceManager) : ControllerBase
    {
        //Get All Product
        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProductDto>>> GetaAllProduct(int? brandId,int? TypeId)
        {
            var Products = await _serviceManager.ProductService.GetAllProductAsync( brandId,  TypeId);
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
