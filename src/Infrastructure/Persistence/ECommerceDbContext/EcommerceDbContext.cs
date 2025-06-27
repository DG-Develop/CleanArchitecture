using System;
using System.Collections.Generic;
using ECommerce.Domain.EcommerceDbEntities;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<CatPaymentType> CatPaymentTypes { get; set; }

    public virtual DbSet<CatProductsType> CatProductsTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleDetail> SaleDetails { get; set; }

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

        modelBuilder.Entity<CatPaymentType>(entity =>
        {
            entity.HasKey(e => e.IdPaymentType);

            entity.ToTable("Cat_PaymentTypes");

            entity.Property(e => e.Description).HasMaxLength(50);
        });

        modelBuilder.Entity<CatProductsType>(entity =>
        {
            entity.HasKey(e => e.IdCatProductType).HasName("PK_Cat_ProductType");

            entity.ToTable("Cat_ProductsType");

            entity.Property(e => e.Descrip).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct);

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdCoinNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdCoin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Cat_Coins");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.IdSale);

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdPaymentTypeNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.IdPaymentType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Cat_PaymentTypes");
        });

        modelBuilder.Entity<SaleDetail>(entity =>
        {
            entity.HasKey(e => e.IdSaleDetails);

            entity.Property(e => e.PriceOrigin).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.SaleDetails)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SaleDetails_Products");

            entity.HasOne(d => d.IdSaleNavigation).WithMany(p => p.SaleDetails)
                .HasForeignKey(d => d.IdSale)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SaleDetails_Sales");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
