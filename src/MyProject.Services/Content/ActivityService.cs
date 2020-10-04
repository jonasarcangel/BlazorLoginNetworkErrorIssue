using MyProject.Core.Entities.Content;
using MyProject.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace MyProject.Services.Content
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityService(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task<string> AddAsync(
            string nodeId, 
            string message, 
            string userId)
        {
            var activity = new Activity()
            {
                Id = Guid.NewGuid().ToString(),
                NodeId = nodeId,
                Content = message,
                CreatedDate = DateTimeOffset.UtcNow.ToString("s"),
                CreatedBy = userId
            };
            _activityRepository.Add(activity);
            await _activityRepository.SaveChangesAsync();
            return activity.Id;
        }
    }
}
