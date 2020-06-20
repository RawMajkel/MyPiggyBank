using System;

namespace MyPiggyBank.Core.Protocol.Base
{
    public abstract class QueryStringParams
    {
        public int Limit { get; set; } = 50;
        public int Page { get; set; } = 1;
        public Guid User { get; set; }
    }
}
