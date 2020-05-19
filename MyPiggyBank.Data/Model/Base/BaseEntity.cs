using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Data.Model.Base
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

    }
}
