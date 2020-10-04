using MyProject.Core.Helper;
using MyProject.Web.Client.Modules.Common.Models;
using MyProject.Web.Client.Modules.Common.Services;
using System.Collections.Generic;

namespace MyProject.Web.Client.Modules.Blogs.Models
{
    public class PostsModel : NodesModel
    {
        public PostsModel(INodeService nodeService) : base(nodeService)
        {

        }

        public IEnumerable<Post> Items()
        {
            return Data.ConvertTo<Post>();
        }
    }
}
