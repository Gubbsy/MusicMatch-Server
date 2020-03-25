using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server.Controllers
{
    public static class Endpoints
    {
        private const string Base = "api/v1/";
        
        //Account

        public const string Account = Base + "account/";

        // Genres

        public const string Genres = Base + "genres/";

        // Venues

        public const string Venues = Base + "venues/";

        // Suggestions

        public const string Suggestions = Base + "suggestions/";

        //Matches

        public const string Matches = Base + "matches/";

        //Messages

        public const string Messages = Base + "messages/";
    }
}
