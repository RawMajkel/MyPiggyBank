using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPiggyBank.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Data.Configuration
{
    class ResourceConfiguration
    {
        public class UserConfiguration : IEntityTypeConfiguration<Resource>
        {
            public void Configure(EntityTypeBuilder<Resource> builder)
            {
                builder.HasKey(u => u.Id);
                builder.Property(u => u.Value)
                       .IsRequired();
                builder.Property(u => u.Currency)
                       .IsRequired();
                builder.Property(u => u.Name)
                       .IsRequired();
            }
        }
    }
}
