using MyProject.Core.Helper;
using MyProject.Web.Client.Modules.Common.Models;
using MyProject.Web.Client.Modules.Common.Services;
using System.Collections.Generic;

namespace MyProject.Web.Client.Modules.Forums.Models
{
    public class TopicsModel : NodesModel
    {
        public TopicsModel(INodeService nodeService) : base(nodeService)
        {

        }

        public IEnumerable<Topic> Items()
        {
            return Data.ConvertTo<Models.Topic>();
        }
    }
}