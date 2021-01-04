using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class Ingredients
    {

        public int Id { get; set; }

        // public string Category { get; set; }

        [RegularExpression(@"([a-zA-Z](\s)?)+", ErrorMessage = "Name can only contain letters"), Required(ErrorMessage = "Name must be provided")]
        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayName("Fat"), Range(0.0, Double.MaxValue, ErrorMessage = "Value cannot be negative!")]
        public decimal Fat { get; set; }

        [DisplayName("Carbs"), Range(0.0, Double.MaxValue, ErrorMessage = "Value cannot be negative!")]
        public decimal Carbs { get; set; }

        [DisplayName("Protein"), Range(0.0, Double.MaxValue, ErrorMessage = "Value cannot be negative!")]
        public decimal Protein { get; set; }

        [DisplayName("Energy"), Range(0.0, Double.MaxValue, ErrorMessage = "Value cannot be negative!")]
        public decimal Energy { get; set; }

      
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Categories Category{ get; set; }

        [ForeignKey("Serving")]
        public int? ServingId { get; set; }
        [DisplayName("Serving Type")]
        public virtual Servings Serving { get; set; }

        [DisplayName("Serving Value")]
        public int? ServingValue { get; set; }

        [ForeignKey("Image")]
        [DisplayName("")]
        public int? ImageId { get; set; }

       
        public virtual IngredientsImages Image { get; set; }

    
    }
      
    
}
