using BrumWithMe.Auth.Identity.Contracts;
using BrumWithMe.Auth.Identity.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using BrumWithMe.Data.Models.Entities;
using System;

namespace BrumWithMe.Auth.Identity.Services
{
    public class AuthService : IAuthService
    {
        private ApplicationSignInManager signInManager;
        private ApplicationUserManager userManager;
        private IAuthenticationManager authManager;

        public AuthService(IOwinContext owinContext)
        {
            this.signInManager = owinContext.Get<ApplicationSignInManager>();
            this.userManager = owinContext.Get<ApplicationUserManager>();
            this.authManager = owinContext.Authentication;
        }

        public void LogOff()
        {
            this.authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public async Task<SignInStatus> LogIn(string email, string password)
        {
            SignInStatus result = await this.signInManager.PasswordSignInAsync(email, password, isPersistent: false, shouldLockout: false);

            return result;
        }

        public async Task<IdentityResult> Register(User user, string password)
        {
            var result = await this.userManager.CreateAsync(user, password);

            return result;
        }

        public Task<IdentityResult> ChangePasswordAsync(string v, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task LogIn(User user, bool isPersistent, bool rememberBrowser)
        {
            throw new NotImplementedException();
        }
    }
}
