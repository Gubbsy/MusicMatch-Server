using Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server.Responses
{
    public class SuggestedUsers
    {
        public IEnumerable<SuggestedUser> suggestedUsers { get; set; }
    }
}
