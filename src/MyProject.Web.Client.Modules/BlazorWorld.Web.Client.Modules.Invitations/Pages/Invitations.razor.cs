using MyProject.Core.Entities.Organization;
using MyProject.Web.Client.Shell.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MyProject.Web.Client.Modules.Invitations.Pages
{
    public partial class Invitations : ComponentBase
    {
        [Inject]
        protected IInvitationService InvitationService { get; set; }
        private Invitation Invitation { get; set; }
        private Invitation[] MyInvitations { get; set; }

        protected async Task SubmitAsync()
        {
            await InvitationService.Invite(Invitation);
        }
    }
}
