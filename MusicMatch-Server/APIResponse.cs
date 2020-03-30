using System.Collections.Generic;

namespace MusicMatch_Server
{
    public class APIResponse<T> where T : class
    {
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public T? Payload { get; set; }
    }
}
