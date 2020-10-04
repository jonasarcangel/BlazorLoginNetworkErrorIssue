using MyProject.Data.Identity;
using MyProject.Data.Identity.Repositories;
using System.Threading.Tasks;

namespace MyProject.Services.Content
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> GetUserNameAsync(string appUserId)
        {
            var username = string.Empty;
            if (!string.IsNullOrEmpty(appUserId))
                username = (await GetUserAsync(appUserId))?.UserName;
            return username;
        }

        public async Task<string> GetUserIdAsync(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user != null)
                return user.Id;
            return string.Empty;
        }

        private async Task<ApplicationUser> GetUserAsync(string appUserId)
        {
            return await _userRepository.GetUserAsync(appUserId);
        }

        public async Task<string> GetAvatarHashAsync(string appUserId)
        {
            var avatarHash = string.Empty;
            if (!string.IsNullOrEmpty(appUserId))
                avatarHash = (await GetUserAsync(appUserId)).AvatarHash;
            return avatarHash;
        }
    }
}
