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
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o=> o.Id);
            builder.Property(o=> o.Id).ValueGeneratedOnAdd();
            builder.Property(o => o.UserId);
            builder.Property(o => o.PaymentId);
            builder.Property(o => o.Street).IsRequired();
            builder.Property(o => o.Street).HasMaxLength(250);
            builder.Property(o => o.ZipCode).IsRequired();
            builder.Property(o => o.ZipCode).HasMaxLength(10);
            builder.Property(o => o.City).HasMaxLength(40);
            builder.Property(o => o.City).IsRequired();
            builder.Property(o => o.CreatedDate).IsRequired();
            builder.Property(o => o.Locality).IsRequired();
            builder.Property(o => o.Locality).HasMaxLength(100);
            builder.Property(o => o.PhoneNumber).IsRequired();
            builder.Property(o => o.PhoneNumber).HasMaxLength(13);
            builder.ToTable("Orders");
        }
    }
}
