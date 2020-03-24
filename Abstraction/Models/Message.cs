using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models
{
    public class Message
    {
        public string UserId { get; set; }
        public string Type { get; set; }
        public string Msg { get; set; }
        public float Date { get; set; }
    }
}
