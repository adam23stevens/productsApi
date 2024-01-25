using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.DAL.Entity;

namespace Products.DAL.Configuration
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
    { 
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasOne(x => x.Colour)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ColourId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

