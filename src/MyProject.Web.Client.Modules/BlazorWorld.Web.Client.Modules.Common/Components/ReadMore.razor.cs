using Microsoft.AspNetCore.Components;

namespace MyProject.Web.Client.Modules.Common.Components
{
    public partial class ReadMore : ComponentBase
    {
        [Parameter]
        public string Link { get; set; }
    }
}
