using MyProject.Core.Helper;
using MyProject.Web.Client.Common.Services;
using MyProject.Web.Client.Modules.Messages.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MyProject.Web.Client.Modules.Messages.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        protected IGroupService GroupService { get; set; }
        private MessagesGroup[] Groups { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var groups = await GroupService.SecureGetAllAsync(Constants.MessagesModule);
            Groups = groups.ConvertTo<MessagesGroup>();
        }
    }
}
