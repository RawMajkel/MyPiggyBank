using System;

namespace MyPiggyBank.Core.Protocol.Resource.Requests
{
    public class ResourceSaveRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Currency { get; set; }
    }
}