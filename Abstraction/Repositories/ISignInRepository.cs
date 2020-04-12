using Abstraction.Models;
using System.Threading.Tasks;

namespace Abstraction.Repositories
{
    public interface ISignInRepository
    {
        public Task<ApplicationUser?> SignIn(string credential, string password);
        public Task SignOut();
    }
}
