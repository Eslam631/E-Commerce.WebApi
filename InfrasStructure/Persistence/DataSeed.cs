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
        public void DataSeeding()
        {
            try
            {
                if (_dbContext.Database.GetAppliedMigrations().Any())
                {

                    _dbContext.Database.Migrate();
                }

                if (!_dbContext.ProductBrands.Any()) {

                    var ProductsBrandData = File.ReadAllText(@"..\InfrasStructure\Persistence\Data\DataSeed\brands.json");

                    var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductsBrandData);
                    if(ProductBrands is not null&& ProductBrands.Any())
                      _dbContext.ProductBrands.AddRange(ProductBrands);

                }


                if (!_dbContext.ProductTypes.Any())
                {

                    var ProductsTypesData = File.ReadAllText(@"..\InfrasStructure\Persistence\Data\DataSeed\types.json");

                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductsTypesData);
                    if (ProductTypes is not null && ProductTypes.Any())
                        _dbContext.ProductTypes.AddRange(ProductTypes);

                }
                if (!_dbContext.Products.Any())
                {

                    var ProductsData = File.ReadAllText(@"..\InfrasStructure\Persistence\Data\DataSeed\products.json");

                    var Product = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    if (Product is not null && Product.Any())
                        _dbContext.Products.AddRange(Product);

                }
                _dbContext.SaveChanges();
            }
            catch (Exception )
            {

              
            }
        }
    }
}
