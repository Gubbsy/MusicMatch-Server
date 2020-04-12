using Abstraction.Models;
using Abstraction.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SQLServer.Models;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SQLServer.Repositories
{
    public class SignInRepository : ISignInRepository
    {

        private readonly SignInManager<ApplicationUserDbo> signInManager;
        private readonly UserManager<ApplicationUserDbo> userManager;
        private readonly AppDbContext appDbContext;

        public SignInRepository(SignInManager<ApplicationUserDbo> signInManager, UserManager<ApplicationUserDbo> userManager, AppDbContext appDbContext)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.appDbContext = appDbContext;
        }

        public async Task<ApplicationUser?> SignIn(string credential, string password)
        {
            string? username;
            Regex emailRgx = new Regex(@"^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$");

            if (emailRgx.IsMatch(credential))
            {
                username = (await appDbContext.Users.FirstOrDefaultAsync(u => u.Email == credential.ToLower()).ConfigureAwait(false))?.UserName;
            }
            else
            {
                username = credential;
            }

            SignInResult result = await signInManager.PasswordSignInAsync(username, password, true, false).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return null;
            }

            ApplicationUser user = await userManager.FindByNameAsync(username).ConfigureAwait(false);

            return user;
        }

        public async Task SignOut()
        {
            await signInManager.SignOutAsync().ConfigureAwait(false);
        }
    }
}
