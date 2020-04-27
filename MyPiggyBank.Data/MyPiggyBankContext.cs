using System;
using System.Collections.Generic;
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
        public DbSet<User> Users { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<CyclicOperation> CyclicOperations { get; set; }
        public DbSet<OperationCategory> OperationCategories { get; set; }
    }
}
