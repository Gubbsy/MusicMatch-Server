using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Models
{
    public class ApplicationUserDbo : IdentityUser
    {
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Bio { get; set; }
    }
}
