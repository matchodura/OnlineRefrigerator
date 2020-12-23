using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class RecipesSteps
    {


        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }


        
        public int? RecipeId { get; set; }
                
        public int? StepNumber { get; set; }

        [Column(TypeName = "text")]
        public string Text { get; set; }

        public byte[] StepImage { get; set; }

        //public virtual ICollection<Recipes> Recipe { get; set; }

    }
}
