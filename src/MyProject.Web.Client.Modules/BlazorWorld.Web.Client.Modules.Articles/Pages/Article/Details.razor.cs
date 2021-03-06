﻿using MyProject.Core.Entities.Configuration;
using MyProject.Core.Entities.Content;
using MyProject.Web.Client.Modules.Common.Components;
using MyProject.Web.Client.Modules.Common.Services;
using MyProject.Web.Client.Shell;
using MyProject.Web.Client.Shell.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace MyProject.Web.Client.Modules.Articles.Pages.Article
{
    public partial class Details : ComponentBase
    {
        [Inject]
        protected INodeService NodeService { get; set; }
        [Inject]
        protected ICategoryService CategoryService { get; set; }
        [Inject]
        protected ISecurityService SecurityService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Slug { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private Models.Article Article { get; set; }
        private Core.Entities.Content.Category Category { get; set; }
        private bool CanEditArticle { get; set; } = false;
        private bool CanDeleteArticle { get; set; } = false;
        private Modal ConfirmModal { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var node = await NodeService.GetBySlugAsync(
                Constants.ArticlesModule,
                Constants.ArticleType,
                Slug);
            Article = Models.Article.Create(node);
            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            var createdBy = node.CreatedBy;
            CanEditArticle = await SecurityService.AllowedAsync(
                loggedInUserId,
                createdBy,
                Constants.ArticlesModule,
                Constants.ArticleType,
                Actions.Edit
            );
            CanDeleteArticle = await SecurityService.AllowedAsync(
                loggedInUserId,
                createdBy,
                Constants.ArticlesModule,
                Constants.ArticleType,
                Actions.Delete
            );
            Category = await CategoryService.GetAsync(node.CategoryId);
        }
    }
}