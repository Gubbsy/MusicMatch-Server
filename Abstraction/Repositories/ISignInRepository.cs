using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Repositories
{
    public interface ISignInRepository
    {
        public Task<IEnumerable<string>?> SignIn(string credential, string password);
        public Task SignOut();
    }
}
