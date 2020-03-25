using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server.Requests
{
    public class GetPreviouseMessages
    {
        public string RecipientId { get; set; }
    }
}
