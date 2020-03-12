using Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Models
{
    public class IntroductionsDbo : Introduction
    {
        public new int Id { get => base.Id; set => base.Id = value; }
        public new ApplicationUser Sender { get => base.Sender; set => base.Sender = value; }
        public new ApplicationUser Recipient { get => base.Recipient; set => base.Recipient = value; }
        public new bool DidRequest { get => base.DidRequest; set => base.DidRequest = value; }
    }
}
