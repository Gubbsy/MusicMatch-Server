using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server.Requests
{
    public class SignIn
    {
        public string Credential { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
