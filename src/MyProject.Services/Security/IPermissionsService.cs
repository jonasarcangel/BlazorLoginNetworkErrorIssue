using MyProject.Core.Entities.Configuration;
using MyProject.Services.Configuration.Models;
using System.Threading.Tasks;

namespace MyProject.Services.Security
{
    public interface IPermissionsService
    {
        Task<bool> AllowedAsync(
            string module,
            string type,
            string action,
            string role,
            bool useCache);
        Task AddAsync(Permission permission);
    }
}
