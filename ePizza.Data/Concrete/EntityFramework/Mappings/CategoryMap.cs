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
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(70);
            builder.Property(c => c.Description).HasMaxLength(500);
            builder.ToTable("Categories");

            builder.HasData(
                new Category
                {
                    Id = 1,
                    Name = "Pizza",
                    Description = "Pizza",


                },
                new Category()
                {
                    Id = 2,
                    Name = "Tatlı",
                    Description = "Tatlı"
                },
                new Category()
                {
                    Id = 3,
                    Name = "İçecek",
                    Description = "İçecek"
                });


        }
    }
}
