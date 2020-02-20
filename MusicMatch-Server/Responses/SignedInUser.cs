using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server.Responses
{
    public class SignedInUser
    {
        public IEnumerable<string> role { get; set; }
    }
}
