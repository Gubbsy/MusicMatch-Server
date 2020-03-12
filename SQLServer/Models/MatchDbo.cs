using Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Models
{
    public class MatchDbo : Match
    {
        public new int Id { get => base.Id; set => base.Id = value; }
        public new ApplicationUserDbo User { get => (ApplicationUserDbo)base.User; set => base.User = value; }
        public new ApplicationUserDbo Matcher { get => (ApplicationUserDbo)base.Matcher; set => base.Matcher = value; }
        public new DateTime MatchDate { get => base.MatchDate; set => base.MatchDate = value; }
    }
}
