using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class RecipesSteps
    {
             
        public int Id { get; set; }        
        public int? RecipeId { get; set; }
        [DisplayName("Step number ")]        
        public int? StepNumber { get; set; }
        [Column(TypeName = "text")]
        public string Text { get; set; }
        public byte[] StepImage { get; set; }        
    }
}
