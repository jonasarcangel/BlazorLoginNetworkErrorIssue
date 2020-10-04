using MyProject.Core.Helper;
using MyProject.Web.Client.Modules.Common.Models;
using MyProject.Web.Client.Modules.Common.Services;
using System.Collections.Generic;

namespace MyProject.Web.Client.Modules.Articles.Models
{
    public class ArticlesModel : NodesModel
    {
        public ArticlesModel(INodeService nodeService) : base(nodeService)
        {

        }

        public IEnumerable<Article> Items()
        {
            return Data.ConvertTo<Models.Article>();
        }
    }
}
