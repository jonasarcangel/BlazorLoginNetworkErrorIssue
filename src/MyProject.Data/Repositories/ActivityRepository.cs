using MyProject.Core.Entities.Content;
using MyProject.Core.Repositories;

namespace MyProject.Data.Repositories
{
    public class ActivityRepository : Repository, IActivityRepository
    {
        public ActivityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public void Add(Activity activity)
        {
            _dbContext.Activities.Add(activity);
        }
    }
}
