using System;

namespace MyPiggyBank.Core.Protocol.Resource {
    public class ResourceInfo
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Currency { get; set; }
    }
}
