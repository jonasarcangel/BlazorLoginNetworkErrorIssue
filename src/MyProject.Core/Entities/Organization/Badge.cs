using System;
using System.Collections.Generic;
using System.Text;
using MyProject.Core.Entities.Common;

namespace MyProject.Core.Entities.Organization
{
    public class Badge : Item
    {
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
