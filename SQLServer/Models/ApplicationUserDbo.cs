using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Models
{
    public class ApplicationUserDbo : IdentityUser
    {
        public string Gender { get; set; }
    }
}
