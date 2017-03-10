using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using BrumWithMe.Data.Models.Entities;
using Microsoft.AspNet.Identity;
using System.Security.Principal;

namespace BrumWithMe.Auth.Identity.Contracts
{
    public interface IAuthService
    {
        void LogOff();

        Task<SignInStatus> LogIn(string email, string password);

        Task<IdentityResult> Register(User user, string password);

        Task<IdentityResult> ChangePasswordAsync(string v, string oldPassword, string newPassword);

        string GetLoggedUserId(IPrincipal loggedUser);
    }
}
