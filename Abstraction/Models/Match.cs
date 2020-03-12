using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models
{
    public class Match
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public ApplicationUser Matcher { get; set; }
        public DateTime MatchDate { get; set; }
    }
}
