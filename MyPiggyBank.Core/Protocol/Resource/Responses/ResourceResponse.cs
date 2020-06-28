using System;

namespace MyPiggyBank.Core.Protocol.Resource.Responses
{
    public class ResourceResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Currency { get; set; }
    }
}