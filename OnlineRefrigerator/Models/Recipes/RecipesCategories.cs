using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class RecipesCategories
    {

        public int Id { get; set; }

        public string Name { get; set; }
              
        public virtual ICollection<Recipes> Recipes { get; set; }


    }
}
