﻿using MyProject.Core.Entities.Content;
using MyProject.Web.Client.Modules.Articles.Models;
using MyProject.Web.Client.Modules.Common;
using MyProject.Web.Client.Modules.Common.Services;
using MyProject.Web.Client.Shell.Services;
using MyProject.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Web.Client.Modules.Articles.Pages.Article
{
    public partial class Edit : ComponentBase
    {
        [Inject]
        protected ICategoryService CategoryService { get; set; }
        [Inject]
        protected ISecurityService SecurityService { get; set; }
        [Inject]
        protected INodeService NodeService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Slug { get; set; }
        [CascadingParameter]
        Task<AuthenticationState> AuthenticationStateTask { get; set; }
        private IEnumerable<Core.Entities.Content.Category> Categories { get; set; } = new List<Core.Entities.Content.Category>();
        private Models.Article Article { get; set; } = new Models.Article();
        private string Weight
        {
            get
            {
                return Article.Weight > 0 ? Article.Weight.ToString() : string.Empty;
            }
            set
            {
                var weight = 0;
                int.TryParse(value, out weight);
                Article.Weight = weight;
            }
        }
        private string ValidationMessage { get; set; } = string.Empty;
        private EditContext _editContext;
        private ValidationMessageStore _messages;

        protected override void OnInitialized()
        {
            _editContext = new EditContext(Article);
            _messages = new ValidationMessageStore(_editContext);
            base.OnInitialized();
        }

        protected override async Task OnParametersSetAsync()
        {
            Categories = await CategoryService.GetAllAsync(Constants.ArticlesModule);
            Categories = Categories.OrderBy(c => c.Name);
            var article = await NodeService.GetBySlugAsync(
                Constants.ArticlesModule,
                Constants.ArticleType,
                Slug);
            Article = Models.Article.Create(article);
        }

        protected async Task SubmitAsync()
        {
            Article.Slug = Article.Title.ToSlug();
            var existingArticle = await NodeService.GetBySlugAsync(
                Constants.ArticlesModule,
                Constants.ArticleType,
                Article.Slug);

            if (existingArticle == null || existingArticle.Id == Article.Id)
            {
                var contentActivity = new ContentActivity()
                {
                    Node = Article,
                    Message = $"Updated an article: {Article.Title}."
                };
                await NodeService.UpdateAsync(contentActivity);
                NavigationManager.NavigateTo($"article/{Article.Slug}");
            }
            else
            {
                ValidationMessage = "A similar title already exists.";
            }
        }
    }
}
