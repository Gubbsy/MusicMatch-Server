using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server.Responses
{
    public class AccountCredentials
    {
        public string Id { get; set; }
        public string AccountRole { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
