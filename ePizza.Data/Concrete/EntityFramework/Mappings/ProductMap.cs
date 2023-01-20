using ePizza.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ePizza.Data.Concrete.EntityFramework.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
       
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();//eklendikçe bir bir arttır 
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(70);
            builder.Property(c => c.Description).HasMaxLength(500);
            builder.Property(p => p.UnitPrice).IsRequired();
            builder.Property(p => p.ImageUrl).HasMaxLength(500);
            builder.ToTable("Products");
        }
    }
}
