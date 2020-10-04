using MyProject.Web.Client.Modules.Articles.Models;
using Microsoft.AspNetCore.Components;

namespace MyProject.Web.Client.Modules.Articles.Components
{
    public partial class ArticleCard : ComponentBase
    {
        [Parameter]
        public Article Article { get; set; }
    }
}
