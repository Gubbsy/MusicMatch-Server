﻿using Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Models
{
    public class MatchesDbo : Matches
    {
        public new int Id { get => base.Id; set => base.Id = value; }
        public new string User { get => base.UId1; set => base.UId1 = value; }
        public new string Matchie { get => base.UId2; set => base.UId2 = value; }
        public new DateTime MatchDate { get => base.MatchDate; set => base.MatchDate = value; }
    }
}
