using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Msg { get; set; }
        public float Date { get; set; }
    }
}
