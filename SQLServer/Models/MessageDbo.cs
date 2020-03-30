using Abstraction.Models;

namespace SQLServer.Models
{
    public class MessageDbo : Message
    {
        public new int Id { get => base.Id; set => base.Id = value; }
        public new string Sender { get => base.Sender; set => base.Sender = value; }
        public new string Recipient { get => base.Recipient; set => base.Recipient = value; }
        public new string Msg { get => base.Msg; set => base.Msg = value; }
        public new float Date { get => base.Date; set => base.Date = value; }
    }
}
