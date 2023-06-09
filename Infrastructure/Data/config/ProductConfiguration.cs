using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(200);
            builder.Property(p => p.price).HasColumnType("decimal(18,2)");
            builder.Property(p=>p.PictureUrl).IsRequired();
            builder.HasOne(b => b.productBrand).WithMany()
                .HasForeignKey(p => p.ProductBrandId);
            builder.HasOne(b => b.productType).WithMany()
                .HasForeignKey(p => p.ProductTypeId);

        }
    }
}