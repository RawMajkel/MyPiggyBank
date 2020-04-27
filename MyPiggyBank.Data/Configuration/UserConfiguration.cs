using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPiggyBank.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Data.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Email)
                   .IsRequired();
            builder.Property(u => u.Username)
                   .IsRequired();
            builder.Property(u => u.PasswordHash)
                   .IsRequired();
        }
    }
}
