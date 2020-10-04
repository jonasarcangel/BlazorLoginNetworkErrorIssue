using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyProject.Core.Entities.Content;

namespace MyProject.Web.Client.Modules.Common.Services
{
    public interface IVoteService
    {
        Task<NodeVote> GetAsync(string id);
        Task<int> AddAsync(string id, bool isUpVote);
    }
}
