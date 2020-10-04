using System.Threading.Tasks;
using MyProject.Core.Entities;
using MyProject.Core.Entities.Organization;

namespace MyProject.Core.Repositories
{
    public interface IInvitationRepository
    {
        Task<int> Add(Invitation invitation);
        Task<string> GetInvitationAsync(string email, string code);
        Task<Invitation[]> GetInvitationsAsync(string createdBy);
    }
}