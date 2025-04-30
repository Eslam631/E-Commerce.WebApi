using Domain.Contracts;
using Domain.Models.IdentityModel;
using Domain.Models.ProductModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Context;
using Persistence.IdentityDbcontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeed(ApplicationDbContext _dbContext,  UserManager<ApplicationUser> _userManager,
        RoleManager<IdentityRole> _roleManager,ECommerceIdentityContext _identityContext) : IDataSeed
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

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));

                }

                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        Email = "Eslam@gmail.com",
                        PhoneNumber = "0109785871",
                        DisplayName = "Eslam Tarek",
                        UserName = "EslamTarek"

                    };
                    var User02 = new ApplicationUser()
                    {
                        Email = "Ail@gmail.com",
                        PhoneNumber = "0109785871",
                        DisplayName = "Ail Ahmed",
                        UserName = "AilAhmed"

                    };

                    await _userManager.CreateAsync(User01, "P@ssw0rd");
                await _userManager.CreateAsync(User02, "P@ssw0rd");

                   await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User02, "SuperAdmin");

                
                
                }
              await  _identityContext.SaveChangesAsync();
            
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
