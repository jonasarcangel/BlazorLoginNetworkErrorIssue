using MyProject.Core.Helper;
using MyProject.Web.Client.Modules.Common.Models;
using MyProject.Web.Client.Modules.Common.Services;
using System.Collections.Generic;

namespace MyProject.Web.Client.Modules.Forums.Models
{
    public class ForumsModel : NodesModel
    {
        public ForumsModel(INodeService nodeService) : base(nodeService)
        {

        }

        public IEnumerable<Forum> Items()
        {
            return Data.ConvertTo<Models.Forum>();
        }
    }
}
