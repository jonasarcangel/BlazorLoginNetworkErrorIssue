﻿using MyProject.Core.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Core.Entities.Content
{
    public class Reaction : Entity
    {
        [Required]
        // Applies to Activities, Messages, and Nodes
        public string ContentId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ReactionType { get; set; }
    }
}
