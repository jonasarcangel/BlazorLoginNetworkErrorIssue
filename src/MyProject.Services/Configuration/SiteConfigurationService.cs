using MyProject.Services.Configuration.Models;
using MyProject.Web.Shared.Models;
using Microsoft.Extensions.Configuration;

namespace MyProject.Services.Configuration
{
    public class SiteConfigurationService : ISiteConfigurationService
    {
        private readonly SiteAppSettings _siteAppSettings;

        public SiteConfigurationService(
            IConfiguration configuration)
        {
            _siteAppSettings = new SiteAppSettings();
            configuration.Bind(nameof(SiteAppSettings), _siteAppSettings);
        }

        public SidebarMenuSetting[] SidebarMenuSettings()
        {
            return _siteAppSettings.SidebarMenuSettings;
        }
    }
}
