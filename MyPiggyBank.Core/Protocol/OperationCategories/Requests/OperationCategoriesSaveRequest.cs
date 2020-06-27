using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Core.Protocol.OperationCategories.Requests
{
    public class OperationCategoriesSaveRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}
