﻿using MyProject.Web.Client.Common;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MyProject.Web.Client.Shell.Services
{
    public class SecurityService : ApiService, ISecurityService
    {
        private const string API_URL = "api/Security";

        public SecurityService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {

        }

        public async Task<bool> AllowedAsync(string loggedInUserId,
            string createdBy, string module, string type, string action)
        {
            if (!string.IsNullOrEmpty(loggedInUserId) && loggedInUserId == createdBy) return true;

            if (!string.IsNullOrEmpty(loggedInUserId))
            {
                var request = $"{API_URL}?module={module}&type={type}&action={action}";
                return await AuthorizedHttpClient.GetFromJsonAsync<bool>(request);
            }
            else
            {
                var request = $"{API_URL}/GuestAllowed?module={module}&type={type}&action={action}";
                return await PublicHttpClient.GetFromJsonAsync<bool>(request);
            }
        }

    }
}
