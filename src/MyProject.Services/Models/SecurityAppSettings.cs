using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Services.Configuration.Models
{
    public class SecurityAppSettings
    {
        public RoleUsers[] DefaultRoleUsers { get; set; }
        public string[] DefaultPermissions { get; set; }
    }
}
