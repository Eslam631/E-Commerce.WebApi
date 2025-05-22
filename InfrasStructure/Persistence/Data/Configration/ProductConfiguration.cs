using Domain.Models.ProductModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(P => P.ProductBrand).WithMany().HasForeignKey(E => E.BrandId);
            builder.HasOne(P => P.ProductType).WithMany().HasForeignKey(E => E.TypeId);
            builder.Property(P => P.Price).HasColumnType("decimal(10,2)");
        }
    }
}
