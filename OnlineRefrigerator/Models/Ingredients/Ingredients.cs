using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class Ingredients
    {

        public int Id { get; set; }

       // public string Category { get; set; }
        public string Name { get; set; }
        public decimal Fat { get; set; }
        public decimal Carbs { get; set; }
        public decimal Protein { get; set; }
        public decimal Energy { get; set; }

      
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Categories Category{ get; set; }


        [ForeignKey("Image")]
        [DisplayName("")]
        public int? ImageId { get; set; }

       
        public virtual IngredientsImages Image { get; set; }

    
    }
      
    
}
