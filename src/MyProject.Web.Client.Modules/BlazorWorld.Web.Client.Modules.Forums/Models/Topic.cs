using MyProject.Core.Entities.Content;
using MyProject.Core.Helper;
using System;

namespace MyProject.Web.Client.Modules.Forums.Models
{
    public class Topic : Node
    {        
        public Topic() : base()
        {
            Module = Constants.ForumsModule;
            Type = Constants.TopicType;
        }

        public static Topic Create(Node node)
        {
            return node.ConvertTo<Topic>();
        }

        public string ForumId
        {
            get => ParentId;
            set => ParentId = value;
        }

        public int PostCount => ChildCount;
    }
}
