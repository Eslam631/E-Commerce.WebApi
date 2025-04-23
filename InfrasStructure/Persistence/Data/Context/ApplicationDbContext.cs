using Domain.Models.ProductModel;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Configration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Context
{
   public class ApplicationDbContext:DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
         public DbSet<ProductType> ProductTypes { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)  
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        }
    }
}
