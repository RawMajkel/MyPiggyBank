using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPiggyBank.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Data.Configuration
{
    class OperationCategoryConfiguration : IEntityTypeConfiguration<OperationCategory>
    {
        public void Configure(EntityTypeBuilder<OperationCategory> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name)
                   .IsRequired();
        }
    }
}
