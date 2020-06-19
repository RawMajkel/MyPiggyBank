﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Core.Protocol.CyclicOperation
{
    public class CyclicOperationInfo 
    {
        public Guid ResourceId { get; set; }
        public Guid OperationCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsIncome { get; set; }
        public decimal? EstimatedValue { get; set; }
        public int Period { get; set; }
        public DateTime NextOccurence { get; set; }
    }
}