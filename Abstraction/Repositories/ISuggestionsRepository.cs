using Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Repositories
{
    public interface ISuggestionsRepository
    {
        public Task<IEnumerable<ApplicationUser>> GetUsersInMatchRadius(double maxLat, double maxLon);
    }
}
