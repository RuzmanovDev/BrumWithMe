using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using BrumWithMe.Data.Models.Entities;
using Microsoft.AspNet.Identity;

namespace BrumWithMe.Auth.Identity.Contracts
{
    public interface IAuthService
    {
        void LogOff();

        Task<SignInStatus> LogIn(string email, string password);

        Task<IdentityResult> Register(User user, string password);

        Task<IdentityResult> ChangePasswordAsync(string v, string oldPassword, string newPassword);

        Task LogIn(User user, bool isPersistent, bool rememberBrowser);
    }
}
