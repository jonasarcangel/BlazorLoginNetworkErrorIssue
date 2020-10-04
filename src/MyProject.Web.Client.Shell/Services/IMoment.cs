using System.Threading.Tasks;

namespace MyProject.Web.Client.Shell.Services
{
    public interface IMoment
    {
        Task<string> FromNowAsync(string date);
        Task<string> LocalDateAsync(string date, string format);
    }
}
