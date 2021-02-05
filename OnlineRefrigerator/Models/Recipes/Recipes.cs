using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class Recipes
    {
             

        public int Id { get; set; }


        [RegularExpression(@"^([\sA-Za-z]+)$", ErrorMessage = "Name can only contain letters"), Required(ErrorMessage = "Name must be provided")]        
        public string Name { get; set; }


        [ForeignKey("Type")]
        public int? TypeId { get; set; }

        [DisplayName("Dish Category")]
        public virtual RecipesCategories Type { get; set; }

        /// <summary>
        /// time in minutes as integers
        /// </summary>
        /// 
        [DisplayName("Preparation Time [min]"), Range(1, Int32.MaxValue, ErrorMessage = "Value cannot be negative!")]
        public int PreparationTime { get; set; }


        //public int StepId { get; set; }
        //public virtual RecipesSteps Step { get; set; }


        [ForeignKey("Image")]
        [DisplayName("")]
        public int? ImageId { get; set; }

        public virtual RecipesImages Image { get; set; }


        public int? VoteValue { get; set; }

        public int? VoteCounts { get; set; }

    }
}
