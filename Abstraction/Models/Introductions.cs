using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models
{
    public class Introductions
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string  Recipient { get; set; }
        public bool Requested { get; set; }
    }
}
