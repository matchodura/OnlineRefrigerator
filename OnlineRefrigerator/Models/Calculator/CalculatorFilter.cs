﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class CalculatorFilter
    {

        public string ItemName { get; set; }
        public int TypeId { get; set; }
        public int CategoryId { get; set; }

        //public bool SortOrder { get; set; } // true - asc,false - desc

        //public string ColumnName { get; set; } //gets column name based on id of element in view


    }
}
