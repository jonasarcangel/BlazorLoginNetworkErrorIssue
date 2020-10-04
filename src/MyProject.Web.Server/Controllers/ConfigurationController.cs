using MyProject.Services.Configuration;
using MyProject.Web.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyProject.Web.Server.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private ISiteConfigurationService _siteConfigurationService;

        public ConfigurationController(ISiteConfigurationService siteConfigurationService)
        {
            _siteConfigurationService = siteConfigurationService;
        }

        public IActionResult SidebarMenuSettings()
        {
            return Ok(_siteConfigurationService.SidebarMenuSettings());
        }
    }
}
