using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Core.Protocol.Query
{
    public class OperationCategoriesQuery : QueryStringParams
    {
        public string Name { get; set; }
    }
}
