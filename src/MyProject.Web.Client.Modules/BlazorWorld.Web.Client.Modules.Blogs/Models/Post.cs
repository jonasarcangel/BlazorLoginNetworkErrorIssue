using MyProject.Core.Entities.Content;
using MyProject.Core.Helper;

namespace MyProject.Web.Client.Modules.Blogs.Models
{
    public class Post : Node
    {
        public Post() : base()
        {
            Module = Constants.BlogsModule;
            Type = Constants.PostType;
        }

        public static Post Create(Node node)
        {
            return node.ConvertTo<Post>();
        }

        public string BlogId
        {
            get => ParentId;
            set => ParentId = value;
        }
    }
}
