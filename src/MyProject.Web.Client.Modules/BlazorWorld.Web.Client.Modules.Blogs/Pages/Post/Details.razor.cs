using MyProject.Core.Entities.Configuration;
using MyProject.Web.Client.Modules.Common.Components;
using MyProject.Web.Client.Modules.Common.Services;
using MyProject.Web.Client.Shell;
using MyProject.Web.Client.Shell.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace MyProject.Web.Client.Modules.Blogs.Pages.Post
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
        private Models.Post Post { get; set; }
        private Models.Blog Blog { get; set; }
        private bool CanEditPost { get; set; } = false;
        private bool CanDeletePost { get; set; } = false;
        private Modal ConfirmModal { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var node = await NodeService.GetBySlugAsync(
                Constants.BlogsModule,
                Constants.PostType,
                Slug);
            Post = Models.Post.Create(node);
            var blog = await NodeService.GetAsync(Post.BlogId);
            Blog = Models.Blog.Create(blog);
            var loggedInUserId = (await AuthenticationStateTask).LoggedInUserId();
            var createdBy = node.CreatedBy;
            CanEditPost = await SecurityService.AllowedAsync(
                loggedInUserId,
                createdBy,
                Constants.BlogsModule,
                Constants.BlogType,
                Actions.Add
            );
            CanDeletePost = await SecurityService.AllowedAsync(
                loggedInUserId,
                createdBy,
                Constants.BlogsModule,
                Constants.BlogType,
                Actions.Delete
            );
        }
    }
}