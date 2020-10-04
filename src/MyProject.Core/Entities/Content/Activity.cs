using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyProject.Core.Entities.Common;

namespace MyProject.Core.Entities.Content
{
    public class Activity : Item
    {
        public string NodeId { get; set; }
        [Required]
        public string Content { get; set; }
        public string CategoryId { get; set; }
        public string GroupId { get; set; }
        public bool IsSystem { get; set; }
    }
}
