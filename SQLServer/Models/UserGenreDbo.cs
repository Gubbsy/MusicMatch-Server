using Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Models
{
    public class UserGenreDbo : UserGenre
    {
        public new int GenreId { get => base.GenreId; set => base.GenreId = value; }
        public new GenreDbo Genre { get => (GenreDbo)base.Genre; set => base.Genre = value; }

        public new string UserId { get => base.UserId; set => base.UserId = value; }
        public new ApplicationUserDbo AssociatedUser { get => (ApplicationUserDbo)base.AssociatedUser; set => base.AssociatedUser = value; }
    }
}
