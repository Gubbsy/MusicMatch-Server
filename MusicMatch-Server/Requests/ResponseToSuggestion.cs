using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server.Requests
{
    public class ResponseToSuggestion
    {
        public string SuggestedUserId { get; set; }
        public bool requestMatch { get; set; }
    }
}
