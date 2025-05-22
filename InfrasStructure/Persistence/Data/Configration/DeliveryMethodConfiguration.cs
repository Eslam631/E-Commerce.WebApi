
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data.Configration
{
    internal class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("DeliveryMethods");

            builder.Property(D => D.ShortName).HasColumnType("nvarchar(50)");
            builder.Property(D => D.Description).HasColumnType("nvarchar(100)");
            builder.Property(D => D.DeliveryTime).HasColumnType("nvarchar(50)");
            builder.Property(D => D.Price).HasColumnType("decimal(8,2)");



            
        }
    }
}
