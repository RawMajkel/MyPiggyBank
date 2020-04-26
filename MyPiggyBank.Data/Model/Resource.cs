using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Data.Model {
    class Resource
    {
        public Guid ResourceId { get; set; }
        public string UserEmail { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Currency { get; set; }
    }
}
