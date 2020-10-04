﻿using MyProject.Core.Entities.Content;
using MyProject.Core.Repositories;
using MyProject.Web.Shared.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyProject.Web.Client.Modules.Common.Services
{
    public interface INodeService
    {
        Task<Node> GetAsync(string id);
        Task<Node> GetBySlugAsync(string module, string type, string slug);
        Task<Node[]> GetAsync(
            NodeSearch nodeSearch,
            int currentPage);
        Task<int> GetCountAsync(NodeSearch nodeSearch);
        Task<int> GetPageSizeAsync(NodeSearch nodeSearch);
        Task<Node> SecureGetAsync(string id);
        Task<Node> SecureGetAsync(string module, string type, string slug);
        Task<Node[]> SecureGetAsync(
            NodeSearch nodeSearch,
            int currentPage);
        Task<int> SecureGetCountAsync(NodeSearch nodeSearch);
        Task<HttpResponseMessage> AddAsync(ContentActivity contentActivity);
        Task<HttpResponseMessage> UpdateAsync(ContentActivity contentActivity);
        Task<HttpResponseMessage> DeleteAsync(string id); 
    }
}
