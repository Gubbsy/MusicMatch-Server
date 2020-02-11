using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server.Responses
{
    public class ErrorResponse
    {
        public string Error { get; set; }
        public string[] ErrorMessages { get; set; }
    }
}
