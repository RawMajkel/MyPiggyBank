using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Data.Repositories.Base
{
    public abstract class BaseRepository
    {
        protected readonly MyPiggyBankContext _context;

        public BaseRepository(MyPiggyBankContext context)
        {
            _context = context;
        }
    }
}
