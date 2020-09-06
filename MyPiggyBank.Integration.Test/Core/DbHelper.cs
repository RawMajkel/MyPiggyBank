using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MyPiggyBank.Data;

namespace MyPiggyBank.Integration.Test {
    public static class DbHelper
    {
        public static MyPiggyBankContext CreateDbInRuntimeMemory() 
        {
            var dbOptions = new DbContextOptionsBuilder<MyPiggyBankContext>()
                .UseInMemoryDatabase(databaseName: "TestIntegrationDB-" + Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            return new MyPiggyBankContext(dbOptions);
        }
    }
}
