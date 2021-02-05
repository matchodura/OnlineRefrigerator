using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class RecipesImages
    {   
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public virtual ICollection<Recipes> Recipe { get; set; }
    }
}
