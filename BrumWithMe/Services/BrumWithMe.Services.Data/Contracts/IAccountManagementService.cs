using BrumWithMe.Data.Models.CompositeModels;
using System.Collections.Generic;

namespace BrumWithMe.Services.Data.Contracts
{
    public interface IAccountManagementService
    {
        string GetUserAvatarUrl(string loggedUser);

        void SetUserAvatar(string logedUserId, string imageUrl);

        IEnumerable<UserBasicInfo> GetAllUsersBasicInfo();
    }
}
