using MyProject.Core.Entities.Content;

namespace MyProject.Core.Repositories
{
    public interface IActivityRepository : IRepository
    {
        void Add(Activity activity);
    }
}
