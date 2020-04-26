using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MyPiggyBank.Data
{
    public class MyPiggyBankContext : DbContext
    {
        public MyPiggyBankContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
