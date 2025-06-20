using ECommerce.Domain.Features.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
          builder.ToTable("Products");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(p => p.Description)
                   .HasMaxLength(500);
            builder.Property(p => p.Price)
                   .HasColumnType("decimal(18,2)");
        }
    }
}
