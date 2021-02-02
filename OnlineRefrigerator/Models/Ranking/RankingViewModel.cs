using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRefrigerator.Models
{
    public class RankingViewModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public double Score { get; set; }

        public int? Votes { get; set; }

    }
}
