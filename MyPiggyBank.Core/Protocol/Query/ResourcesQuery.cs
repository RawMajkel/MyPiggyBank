using AutoMapper;
using MyPiggyBank.Data.Model;
using System;
using System.Text;

namespace MyPiggyBank.Core.Protocol.Query
{

    public class ResourcesQuery : QueryStringParams {
        public string Name { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public string Currency { get; set; }
    }
}
