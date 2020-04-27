﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Data.Model {
    public class Resource
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Currency { get; set; }
        public virtual User User { get; set; }
    }
}