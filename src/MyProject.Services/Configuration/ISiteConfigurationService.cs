using MyProject.Web.Shared.Models;

namespace MyProject.Services.Configuration
{
    public interface ISiteConfigurationService
    {
        SidebarMenuSetting[] SidebarMenuSettings();
    }
}
