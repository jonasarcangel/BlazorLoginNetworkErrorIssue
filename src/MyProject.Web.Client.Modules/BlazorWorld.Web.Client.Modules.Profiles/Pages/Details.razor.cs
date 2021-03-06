﻿using MyProject.Core.Entities.Configuration;
using MyProject.Web.Client.Modules.Common.Services;
using MyProject.Web.Client.Modules.Profiles.Models;
using MyProject.Web.Client.Shell;
using MyProject.Web.Client.Shell.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace MyProject.Web.Client.Modules.Profiles.Pages
{
    public partial class Details : ComponentBase
    {
        [Inject]
        protected INodeService NodeService { get; set; }
        [Inject]
        protected ISecurityService SecurityService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Slug { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private Profile Profile { get; set; }
        private bool CanEditProfile { get; set; } = false;

        protected override async Task OnParametersSetAsync()
        {
            var node = await NodeService.GetBySlugAsync(
                Constants.ProfilesModule,
                Constants.ProfileType,
                Slug);
            Profile = Profile.Create(node);
            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            var createdBy = node.CreatedBy;
            CanEditProfile = await SecurityService.AllowedAsync(
                loggedInUserId,
                createdBy,
                Constants.ProfilesModule,
                Constants.ProfileType,
                Actions.Add
            );
        }
    }
}
