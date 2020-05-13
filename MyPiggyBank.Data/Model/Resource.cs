﻿using System;
using System.Collections.Generic;
using MyPiggyBank.Data.Model.Base;

namespace MyPiggyBank.Data.Model {
    public class Resource : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Currency { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Operation> Operations { get; set; }
        public virtual ICollection<CyclicOperation> CyclicOperations { get; set; }
    }
}
