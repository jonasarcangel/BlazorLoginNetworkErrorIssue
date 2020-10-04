using MyProject.Core.Repositories;
using System.Threading.Tasks;

namespace MyProject.Data.Identity.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task<ApplicationUser> GetUserAsync(string appUserId);
        Task<ApplicationUser> GetUserByUsernameAsync(string username);
    }
}
