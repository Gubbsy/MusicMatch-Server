using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server
{
    public class APIResponse<T> where T : class
    {
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public T? Payload { get; set; }
    }
}
