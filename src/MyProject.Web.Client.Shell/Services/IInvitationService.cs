using MyProject.Core.Entities.Organization;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyProject.Web.Client.Shell.Services
{
    public interface IInvitationService
    {
        Task<Invitation[]> GetAllAsync();
        Task<HttpResponseMessage> Invite(Invitation invitation);
    }
}
