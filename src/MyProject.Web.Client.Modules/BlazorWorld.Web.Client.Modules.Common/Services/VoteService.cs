﻿using MyProject.Core.Entities.Content;
using MyProject.Web.Client.Common;
using MyProject.Web.Client.Shell.Services;
using MyProject.Web.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyProject.Web.Client.Modules.Common.Services
{
    public class VoteService : ApiService, IVoteService
    {
        private const string API_URL = "api/Vote";

        public VoteService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {

        }

        public async Task<NodeVote> GetAsync(string id)
        {
            var request = $"{API_URL}?itemId={id}";
            var response = await AuthorizedHttpClient.GetAsync(request);
            var jsonString = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(jsonString)) return null;
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var vote = JsonSerializer.Deserialize<NodeVote>(jsonString, options);
            var voteString = JsonSerializer.Serialize(vote);
            return vote;
        }

        public async Task<int> AddAsync(string id, bool isUpVote)
        {
            var voteAction = new VoteAction()
            {
                ItemId = id,
                IsUpVote = isUpVote
            };
            var response = await AuthorizedHttpClient.PostAsJsonAsync(API_URL, voteAction);
            var votes = await response.Content.ReadAsStringAsync();
            int output = 0;
            int.TryParse(votes, out output);
            return output;
        }
    }
}
