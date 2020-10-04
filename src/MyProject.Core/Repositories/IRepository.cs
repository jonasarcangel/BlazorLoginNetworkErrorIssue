using System.Threading.Tasks;

namespace MyProject.Core.Repositories
{
    public interface IRepository
    {
        Task SaveChangesAsync();
    }
}
