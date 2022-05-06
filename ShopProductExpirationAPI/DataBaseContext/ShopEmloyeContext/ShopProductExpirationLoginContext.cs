using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ShopProductExpirationAPI.Models;

#nullable disable

namespace ShopProductExpirationAPI
{
    public partial class ShopProductExpirationLoginContext : IdentityDbContext<ShopEmploye>
    {
        public ShopProductExpirationLoginContext()
        {
        }

        public ShopProductExpirationLoginContext(DbContextOptions<ShopProductExpirationLoginContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=mssqllocaldb;Database=ShopProductExpiration.Login;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
