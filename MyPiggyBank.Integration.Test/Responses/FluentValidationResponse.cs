using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Integration.Test.Responses
{
    public class FluentValidationResponse
    {
        public Uri Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }
        public Dictionary<string, IEnumerable<string>> Errors { get; set; }
    }
}
