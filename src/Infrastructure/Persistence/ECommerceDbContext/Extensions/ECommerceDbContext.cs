using ECommerce.Domain.ValueObject.Products;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.ECommerceDbContext;

public partial class EcommerceDbContext
{
    public DbSet<ProductDetailValueJoin> ProductDTO { get; set; }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<ProductDetailValueJoin>(entity =>
        {
            entity.HasNoKey().ToView(null);


            entity.Property(p => p.Price)
                    .HasPrecision(18, 4);
        });


    }
}
