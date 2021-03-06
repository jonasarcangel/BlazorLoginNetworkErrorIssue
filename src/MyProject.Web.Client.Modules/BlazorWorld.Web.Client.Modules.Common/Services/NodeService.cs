﻿using MyProject.Core.Entities.Content;
using MyProject.Core.Repositories;
using MyProject.Web.Client.Common;
using MyProject.Web.Shared.Models;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyProject.Web.Client.Modules.Common.Services
{
    public class NodeService : ApiService, INodeService
    {
        private const string API_URL = "api/Node";
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public NodeService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {

        }

        public async Task<Node> GetAsync(string id)
        {
            var request = $"{API_URL}?id={id}";
            return await PublicHttpClient.GetFromJsonAsync<Node>(request);
        }

        public async Task<Node> GetBySlugAsync(
            string module,
            string type,
            string slug)
        {
            var nodeSearch = new NodeSearch()
            {
                Module = module,
                Type = type,
                Slug = slug
            };
            var request = $"{API_URL}/GetPaginatedResult";
            var response = await PublicHttpClient.PostAsJsonAsync<NodeSearch>(request, nodeSearch);
            var jsonString = await response.Content.ReadAsStringAsync();
            var items = System.Text.Json.JsonSerializer.Deserialize<Node[]>(jsonString, _jsonSerializerOptions);
            return items.FirstOrDefault();
        }

        public async Task<Node[]> GetAsync(
            NodeSearch nodeSearch,
            int currentPage)
        {
            var request = $"{API_URL}/GetPaginatedResult?currentPage={currentPage}";
            var response = await PublicHttpClient.PostAsJsonAsync<NodeSearch>(request, nodeSearch);
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = System.Text.Json.JsonSerializer.Deserialize<Node[]>(jsonString, _jsonSerializerOptions);
            return result;
        }

        public async Task<int> GetCountAsync(NodeSearch nodeSearch)
        {
            var request = $"{API_URL}/GetCount";
            var response = await PublicHttpClient.PostAsJsonAsync<NodeSearch>(request, nodeSearch);
            var count = await response.Content.ReadAsStringAsync();
            return int.Parse(count);
        }

        public async Task<int> GetPageSizeAsync(NodeSearch nodeSearch)
        {
            var request = $"{API_URL}/GetPageSize";
            var response = await PublicHttpClient.PostAsJsonAsync<NodeSearch>(request, nodeSearch);
            var pageSize = await response.Content.ReadAsStringAsync();
            int result;
            int.TryParse(pageSize, out result);
            return result;
        }

        public async Task<Node> SecureGetAsync(string id)
        {
            var request = $"{API_URL}?id={id}";
            var items = await AuthorizedHttpClient.GetFromJsonAsync<Node[]>(request);
            return items.FirstOrDefault();
        }

        public async Task<Node> SecureGetAsync(
            string module,
            string type,
            string slug)
        {
            var nodeSearch = new NodeSearch()
            {
                Module = module,
                Type = type,
                Slug = slug
            };
            var request = $"{API_URL}/GetPaginatedResult";
            var response = await AuthorizedHttpClient.PostAsJsonAsync<NodeSearch>(request, nodeSearch);
            var jsonString = await response.Content.ReadAsStringAsync();
            var items = System.Text.Json.JsonSerializer.Deserialize<Node[]>(jsonString, _jsonSerializerOptions);
            return items.FirstOrDefault();
        }

        public async Task<Node[]> SecureGetAsync(
            NodeSearch nodeSearch,
            int currentPage)
        {
            var request = $"{API_URL}/GetPaginatedResult?currentPage={currentPage}";
            var response = await AuthorizedHttpClient.PostAsJsonAsync<NodeSearch>(request, nodeSearch);
            var jsonString = await response.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<Node[]>(jsonString, _jsonSerializerOptions);
        }

        public async Task<int> SecureGetCountAsync(NodeSearch nodeSearch)
        {
            var request = $"{API_URL}/GetCount";
            var response = await AuthorizedHttpClient.PostAsJsonAsync<NodeSearch>(request, nodeSearch);
            var count = await response.Content.ReadAsStringAsync();
            return int.Parse(count);
        }

        public async Task<HttpResponseMessage> AddAsync(ContentActivity contentActivity)
        {
            var response = await AuthorizedHttpClient.PostAsJsonAsync<ContentActivity>(API_URL, contentActivity);
            var id = await response.Content.ReadAsStringAsync();
            contentActivity.Node.Id = id;

            return response;
        }

        public async Task<HttpResponseMessage> UpdateAsync(ContentActivity contentActivity)
        {
            return await AuthorizedHttpClient.PutAsJsonAsync<ContentActivity>(API_URL, contentActivity);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string id)
        {
            return await AuthorizedHttpClient.DeleteAsync($"{API_URL}?id={id}");
        }
    }
}
