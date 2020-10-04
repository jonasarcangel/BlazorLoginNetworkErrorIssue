using MyProject.Core.Helper;
using MyProject.Web.Client.Modules.Blogs.Models;
using MyProject.Web.Client.Modules.Common.Models;
using MyProject.Web.Client.Modules.Common.Services;
using System.Collections.Generic;

namespace MyProject.Web.Client.Modules.Blogs.Models
{
    public class CommentsModel : NodesModel
    {
        public CommentsModel(INodeService nodeService) : base(nodeService)
        {

        }

        public IEnumerable<Comment> Items()
        {
            return Data.ConvertTo<Comment>();
        }
    }
}
