using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class Recipes
    {

        //TODO: add rest of properties

        public int Id { get; set; }

        public string Name { get; set; }


        
        [ForeignKey("Type")]
        public int? TypeId { get; set; }

        [DisplayName("Dish Category")]
        public virtual RecipesCategories Type { get; set; }

        /// <summary>
        /// time in minutes as integer 
        /// </summary>
        /// 

        [DisplayName("Preparation Time")]
        public int PreparationTime { get; set; }

        
        //public int StepId { get; set; }
        //public virtual RecipesSteps Step { get; set; }

               
        public virtual RecipesImages Image { get; set; }



    }
}
