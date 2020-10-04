using System.Threading.Tasks;

namespace MyProject.Web.Client.Shell.Services
{
    public interface IMarkdown
    {
        Task<string> RenderAsync(string text);
    }
}
