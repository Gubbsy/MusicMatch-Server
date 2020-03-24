using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server.Responses
{
    public class UserLoggedIn
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
    }
}
