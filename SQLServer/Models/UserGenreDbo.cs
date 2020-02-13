using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Models
{
    public class UserGenreDbo
    {
        public int GenreId { get; set; }
        public GenreDbo Genre { get; set; }

        public string UserId { get; set; }
        public ApplicationUserDbo AssociatedUser { get; set; }
    }
}
