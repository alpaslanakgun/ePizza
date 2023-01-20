using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePizza.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ePizza.Data.Concrete.EntityFramework.Mappings
{
    internal class ProductTypeMap : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Name).HasMaxLength(250);
            builder.ToTable("ProductTypes");

            builder.HasData(

                new ProductType
                {
                    Id = 1,
                    Name = "Sebze"
                },

                new ProductType
                {
                    Id = 2,
                    Name = "Sebzesiz"

                });


        }
    }
}
