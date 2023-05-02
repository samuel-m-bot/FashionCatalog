using FashionCatalogue.Areas.Identity.Data;
using FashionCatalogue.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FashionCatalogue.Data;

public class FashionCatalogContext : IdentityDbContext<FashionCatalogUser>
{
    public FashionCatalogContext(DbContextOptions<FashionCatalogContext> options)
        : base(options)
    {
    }
    public DbSet<FashionCatalogue.Models.Product> Products { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Product>()
           .ToTable("products_table")
           .HasKey(p => p.Id); 


        builder.Entity<Product>()
            .Property(p => p.Store)
        .HasMaxLength(50)
            .IsRequired();

        builder.Entity<Product>()
            .Property(p => p.Name)
        .HasMaxLength(80)
            .IsRequired();
        builder.Entity<Product>()
            .Property(p => p.Category)
        .HasMaxLength(50)
            .IsRequired();
        builder.Entity<Product>()
            .Property(p => p.SubCategory)
            .HasMaxLength(50)
            .IsRequired();

        builder.Entity<Product>()
            .Property(p => p.Price)
            .IsRequired();
        builder.Entity<Product>()
            .Property(p => p.SiteURL)
        .HasMaxLength(200)
            .IsRequired();
        builder.Entity<Product>()
            .Property(p => p.ImageURL)
            .HasMaxLength(200)
            .IsRequired();
        builder.Entity<Product>()
            .Property(p => p.Sex)
            .HasMaxLength(10)
            .IsRequired();

        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntitiyConfigurations());
    }
}
public class ApplicationUserEntitiyConfigurations : IEntityTypeConfiguration<FashionCatalogUser>
{
    public void Configure(EntityTypeBuilder<FashionCatalogUser> builder)
    {
  
    }
}
