using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Core.Protocol.Query
{
    public class OperationCategoriesQuery : QueryStringParams
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}
