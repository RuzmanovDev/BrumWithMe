using BrumWithMe.Auth.Identity.Contracts;
using BrumWithMe.Auth.Identity.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using BrumWithMe.Data.Models.Entities;
using System;
using System.Security.Principal;
using Bytes2you.Validation;
using BrumWithMe.Data.Contracts;

namespace BrumWithMe.Auth.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationSignInManager signInManager;
        private readonly ApplicationUserManager userManager;
        private readonly IAuthenticationManager authManager;
        private readonly IRepositoryEf<User> userRepo;
        private readonly Func<IUnitOfWorkEF> unitOfWork;

        public AuthService(IOwinContext owinContext, Func<IUnitOfWorkEF> unitOfWork, IRepositoryEf<User> userRepo)
        {
            Guard.WhenArgument(owinContext, nameof(owinContext)).IsNull().Throw();
            Guard.WhenArgument(unitOfWork, nameof(unitOfWork)).IsNull().Throw();
            Guard.WhenArgument(userRepo, nameof(userRepo)).IsNull().Throw();

            this.unitOfWork = unitOfWork;
            this.userRepo = userRepo;

            this.signInManager = owinContext.Get<ApplicationSignInManager>();
            this.userManager = owinContext.Get<ApplicationUserManager>();
            this.authManager = owinContext.Authentication;

            this.userManager.UserLockoutEnabledByDefault = true;
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

        public string GetLoggedUserId(IPrincipal loggedUser)
        {
            return loggedUser.Identity.GetUserId();
        }

        public void LockAccount(string userId, int daysToLockOutAccount)
        {
            Guard.WhenArgument(userId, nameof(userId)).IsNullOrEmpty().Throw();

            var user = this.userRepo.GetById(userId);

            if (user != null)
            {
                using (var uow = this.unitOfWork())
                {
                    user.LockoutEnabled = true;
                    user.LockoutEndDateUtc = DateTime.Now.AddDays(daysToLockOutAccount);

                    uow.Commit();
                }
            }
        }
    }
}
