using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePizza.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ePizza.Data.Concrete.EntityFramework.Mappings
{
    public class AddressMap :IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.Street).IsRequired();
            builder.Property(a => a.Street).HasMaxLength(250);
            builder.Property(a=>a.Locality).IsRequired();
            builder.Property(a=>a.Locality).HasMaxLength(100);
            builder.Property(a => a.ZipCode).IsRequired();
            builder.Property(a => a.ZipCode).HasMaxLength(10);
            builder.Property(a => a.PhoneNumber).IsRequired();
            builder.Property(a => a.PhoneNumber).HasMaxLength(13);
            builder.Property(c => c.UserId);
            builder.ToTable("Address");
        }
    }
}
