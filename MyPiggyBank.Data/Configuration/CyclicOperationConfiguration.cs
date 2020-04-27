using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPiggyBank.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Data.Configuration
{
    class CyclicOperationConfiguration : IEntityTypeConfiguration<CyclicOperation>
    {
        public void Configure(EntityTypeBuilder<CyclicOperation> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name)
                   .IsRequired();
        }
    }
}
