using MyProject.Core.Entities.Content;
using System.Linq;

namespace MyProject.Core.Repositories
{
    public interface IMessageRepository : IRepository
    {
        IQueryable<Message> Get(string groupId);
        void Add(Message message);
    }
}
