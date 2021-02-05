using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class Servings
    {
        public int Id { get; set; }
        public string ServingType { get; set; }

        public int? ServingValue { get; set; }
        public virtual ICollection<Ingredients> Ingredient { get; set; }
    }

    public class ServingDropdownListItem
    {
        public string name { get; set; }

        public int id { get; set; }
    
    }

}
