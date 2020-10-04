using MyProject.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyProject.Core.Entities.Organization
{
    public class Invitation : Item
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string InvitationCode { get; set; }
    }
}
