using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models
{
    public class Message
    {
        public string UserID { get; set; }
        public string Type { get; set; }
        public string Msg { get; set; }
        public DateTime Date { get; set; }

    }
}
