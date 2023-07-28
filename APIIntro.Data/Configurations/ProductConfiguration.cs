using APIIntro.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIIntro.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired()
                .IsUnicode()
                 .HasMaxLength(30);
            builder.Property(x => x.CreatedAt)
                .HasDefaultValue(DateTime.UtcNow.AddHours(4));
            builder.Property(x=>x.IsDeleted).HasDefaultValue(false);
        }
    }
}
