using Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Models
{
    public class MessageDbo : Message
    {
        public new string Sender { get => base.Sender; set => base.Sender = value; }
        public new string Recipient { get => base.Recipient; set => base.Recipient = value; }
        public new string Msg { get => base.Msg; set => base.Msg = value; }
        public new float Date { get => base.Date; set => base.Date = value; }
    }
}
