using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Services.Configuration.Models
{
    public class ContentAppSettings
    {
        public RoleWeightSetting[] RoleWeightSettings { get; set; }
        public PageSizeSetting[] PageSizeSettings { get; set; }
    }
}
