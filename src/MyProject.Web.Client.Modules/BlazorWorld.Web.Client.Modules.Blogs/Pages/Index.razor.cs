﻿using MyProject.Core.Constants;
using MyProject.Core.Entities.Configuration;
using MyProject.Core.Repositories;
using MyProject.Web.Client.Modules.Blogs.Models;
using MyProject.Web.Client.Modules.Common.Services;
using MyProject.Web.Client.Shell;
using MyProject.Web.Client.Shell.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace MyProject.Web.Client.Modules.Blogs.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected INodeService NodeService { get; set; }
        [Inject]
        protected ISecurityService SecurityService { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private bool CanAddBlog { get; set; } = false;
        private BlogsModel Blogs { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            Blogs = new BlogsModel(NodeService)
            {
                NodeSearch = new NodeSearch()
                {
                    Module = Constants.BlogsModule,
                    Type = Constants.BlogType,
                    OrderBy = new string[]
                    {
                        OrderBy.Title
                    }
                }
            };
            await Blogs.InitAsync();
            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            CanAddBlog = await SecurityService.AllowedAsync(
                loggedInUserId,
                null,
                Constants.BlogsModule,
                Constants.BlogType,
                Actions.Add
            );
        }
    }
}
