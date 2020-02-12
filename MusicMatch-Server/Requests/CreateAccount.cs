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
        public string Username { get; set; }
        [Required]
        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$",
            ErrorMessage = "The Email field is not a valid email-address.")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Bio { get; set; }
        [Required]
        public double Lat { get; set; }
        [Required]
        public double Lon { get; set; }
        [Required]
        public string AccountRole { get; set; }

    }
}
