namespace BrumWithMe.Services.Data.Contracts
{
    public interface IAccountManagementService
    {
        string GetUserAvatarUrl(string loggedUser);
        void SetUserAvatar(string logedUserId, string imageUrl);
    }
}
