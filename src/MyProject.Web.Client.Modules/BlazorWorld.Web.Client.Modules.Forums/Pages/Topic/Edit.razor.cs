﻿using MyProject.Web.Client.Modules.Common.Services;
using MyProject.Web.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MyProject.Web.Client.Modules.Forums.Pages.Topic
{
    [Authorize]
    public partial class Edit : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected INodeService NodeService { get; set; }
        [Parameter]
        public string Id { get; set; }
        private Models.Forum Forum { get; set; } = new Models.Forum();
        private Models.Topic Topic { get; set; } = new Models.Topic();

        protected override async Task OnParametersSetAsync()
        {
            var topicNode = await NodeService.GetAsync(Id);
            Topic = Models.Topic.Create(topicNode);
            var forumNode = await NodeService.GetAsync(Topic.ForumId);
            Forum = Models.Forum.Create(forumNode);
        }

        protected async Task SubmitAsync()
        {
            var contentActivity = new ContentActivity()
            {
                Node = Topic,
                Message = $"Updated a new forum topic: {Topic.Snippet}"
            };
            await NodeService.UpdateAsync(contentActivity);
            NavigationManager.NavigateTo($"forum/topic/{Topic.Id}");
        }
    }
}