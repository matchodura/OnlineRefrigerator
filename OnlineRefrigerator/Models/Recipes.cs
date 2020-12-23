using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Types of recipe 0-normal 1-wegetarian 2-vegan
        /// </summary>
        /// 
        public int Type { get; set; }

        /// <summary>
        /// time in minutes as integer 
        /// </summary>
        public int PreparationTime { get; set; }

        
        //public int StepId { get; set; }
        //public virtual RecipesSteps Step { get; set; }

               
        public virtual RecipesImages Image { get; set; }



    }
}
