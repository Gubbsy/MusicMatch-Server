﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models
{
    public class Matches
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Matchie { get; set; }
        public DateTime MatchDate { get; set; }
    }
}
