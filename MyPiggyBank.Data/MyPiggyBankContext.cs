using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyPiggyBank.Data.Model;

namespace MyPiggyBank.Data
{
    public class MyPiggyBankContext : DbContext
    {
        public MyPiggyBankContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelbuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<CyclicOperation> CyclicOperations { get; set; }
        public DbSet<OperationCategory> OperationCategories { get; set; }
    }
}
