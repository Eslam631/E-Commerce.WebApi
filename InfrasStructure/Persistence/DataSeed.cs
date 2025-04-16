using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeed(ApplicationDbContext _dbContext) : IDataSeed
    {
        public async Task DataSeedAsync()
        {
            try
            {
                
                if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
                {

                    _dbContext.Database.Migrate();
                }

                if (!_dbContext.ProductBrands.Any()) {

                    var ProductsBrandData = File.OpenRead(@"..\InfrasStructure\Persistence\Data\DataSeed\brands.json");

                    var ProductBrands =await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductsBrandData);
                    if(ProductBrands is not null&& ProductBrands.Any())
                   await  _dbContext.ProductBrands.AddRangeAsync(ProductBrands);

                }


                if (!_dbContext.ProductTypes.Any())
                {

                    var ProductsTypesData = File.OpenRead(@"..\InfrasStructure\Persistence\Data\DataSeed\types.json");

                    var ProductTypes =await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductsTypesData);
                    if (ProductTypes is not null && ProductTypes.Any())
                      await _dbContext.ProductTypes.AddRangeAsync(ProductTypes);

                }
                if (!_dbContext.Products.Any())
                {

                    var ProductsData = File.OpenRead(@"..\InfrasStructure\Persistence\Data\DataSeed\products.json");

                    var Product =await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);
                    if (Product is not null && Product.Any())
                      await  _dbContext.Products.AddRangeAsync(Product);

                }
               await _dbContext.SaveChangesAsync();
            }
            catch (Exception )
            {

              
            }
        }
    }
}
