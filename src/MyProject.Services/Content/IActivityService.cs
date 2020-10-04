using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyProject.Core.Entities.Content;

namespace MyProject.Services.Content
{
    public interface IActivityService
    {
        Task<string> AddAsync(string nodeId, string message, string userId);
    }
}
