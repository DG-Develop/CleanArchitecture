using ECommerce.Domain.EcommerceDbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ECommerce.Persistence.ECommerceDbContext;

public partial class EcommerceDbContext : DbContext
{
    public EcommerceDbContext()
    {
    }

    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CatCoin> CatCoins { get; set; }

    public virtual DbSet<CatProductsType> CatProductsTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CatCoin>(entity =>
        {
            entity.HasKey(e => e.IdCoin).HasName("PK__Cat_Coin__37DAEAACF7CB1574");

            entity.ToTable("Cat_Coins");

            entity.Property(e => e.IsoCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Symbol)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<CatProductsType>(entity =>
        {
            entity.HasKey(e => e.IdCatProductType).HasName("PK_Cat_ProductType");

            entity.ToTable("Cat_ProductsType");

            entity.Property(e => e.Descrip).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdCoinNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdCoin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Cat_Coins");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
