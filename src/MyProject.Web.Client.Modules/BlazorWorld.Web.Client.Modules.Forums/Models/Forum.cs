using MyProject.Core.Entities.Content;
using MyProject.Core.Helper;
using System;

namespace MyProject.Web.Client.Modules.Forums.Models
{
    public class Forum : Node
    {
        public Forum() : base()
        {
            Module = Constants.ForumsModule;
            Type = Constants.ForumType;
        }

        public static Forum Create(Node node)
        {
            return node.ConvertTo<Forum>();
        }

        public string Name
        {
            get => Slug;
            set => Slug = value;
        }

        public int TopicCount => ChildCount;
    }
}
