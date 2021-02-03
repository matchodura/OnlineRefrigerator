using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class UserVotes
    {

        public int Id { get; set; }

        public string UserId { get; set; }
        public int RecipeId { get; set; }

        public int VoteValue { get; set; }

    }
}
