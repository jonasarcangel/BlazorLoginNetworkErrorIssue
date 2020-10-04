using System.Threading.Tasks;

namespace MyProject.Web.Client.Common.Services
{
    public interface IUserApiService
    {
        Task<string> GetUserNameAsync(string appUserId);
        Task<string> GetUserIdAsync(string username);
        Task<string> GetAvatarHashAsync(string appUserId);
    }
}
