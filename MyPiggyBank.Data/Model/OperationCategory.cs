﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Data.Model {
    class OperationCategory
    {
        public Guid OperationCategoryId { get; set; }
        public string UserEmail { get; set; }
        public string Name { get; set; }
    }
}
