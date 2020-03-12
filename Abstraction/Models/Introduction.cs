using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models
{
    public class Introduction
    {
        public int Id { get; set; }
        public ApplicationUser Sender { get; set; }
        public ApplicationUser Recipient { get; set; }
        public bool DidRequest { get; set; }
    }
}
