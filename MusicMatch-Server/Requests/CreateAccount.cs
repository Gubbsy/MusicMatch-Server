using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server.Requests
{
    public class CreateAccount
    {
        [Required]
        public string AccountRole { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$",
            ErrorMessage = "The Email field is not a valid email-address.")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
