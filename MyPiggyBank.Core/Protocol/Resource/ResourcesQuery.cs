using MyPiggyBank.Core.Protocol.Base;

namespace MyPiggyBank.Core.Protocol.Resource
{

    public class ResourcesQuery : QueryStringParams
    {
        public string Name { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public string Currency { get; set; }
    }
}
