using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ShopProductExpirationAPI
{
    public partial class ShopProductExpirationContext : DbContext
    {
        public ShopProductExpirationContext()
        {
        }

        public ShopProductExpirationContext(DbContextOptions<ShopProductExpirationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=mssqllocaldb;Database=ShopProductExpiration.Login;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.CountUnits).HasMaxLength(100);

                entity.Property(e => e.DepartmentHeadFio)
                    .HasMaxLength(150)
                    .HasColumnName("DepartmentHeadFIO");

                entity.Property(e => e.ProductManufacturingDate).HasColumnType("datetime");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ProductPackagingDate).HasColumnType("datetime");

                entity.Property(e => e.SellBy).HasColumnType("datetime");

                entity.Property(e => e.ShopDepartment).HasMaxLength(100);

                entity.Property(e => e.TimeUnits).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
