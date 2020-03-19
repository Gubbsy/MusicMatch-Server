using Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Models
{
    public class IntroductionsDbo : Introductions
    {
        public new int Id { get => base.Id; set => base.Id = value; }
        public new string Sender { get => base.Sender; set => base.Sender = value; }
        public new string Recipient { get => base.Recipient; set => base.Recipient = value; }
        public new bool Requested { get => base.Requested; set => base.Requested = value; }
    }
}
