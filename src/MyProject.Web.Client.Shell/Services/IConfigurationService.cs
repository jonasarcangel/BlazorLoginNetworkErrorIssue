using MyProject.Web.Shared.Models;
using System.Threading.Tasks;

namespace MyProject.Web.Client.Shell.Services
{
    public interface IConfigurationService
    {
        Task<SidebarMenuSetting[]> SidebarMenuSettingsAsync();
    }
}
