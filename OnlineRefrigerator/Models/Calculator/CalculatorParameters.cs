using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class CalculatorParameters
    {
        public decimal Fat { get; set; }
        public decimal Carbs { get; set; }       
        public decimal Protein { get; set; }
        public decimal Energy { get; set; }
        public string ServingType { get; set; }       
        public int ServingValue { get; set; }
        public int ServingQuantity { get; set; }
    }
}
