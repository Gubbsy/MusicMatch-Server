using Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Models
{
    public class MatchesDbo : Matches
    {
        public new int Id { get => base.Id; set => base.Id = value; }
        public new string User { get => base.User; set => base.User = value; }
        public new string Matchie { get => base.Matchie; set => base.Matchie = value; }
        public new DateTime MatchDate { get => base.MatchDate; set => base.MatchDate = value; }
    }
}
