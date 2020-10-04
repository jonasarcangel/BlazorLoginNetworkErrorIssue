using System.Net.Http;

namespace MyProject.Web.Client.Common
{
    public class ApiService
    {
        protected IHttpClientFactory HttpClientFactory { get; set; }
        protected HttpClient AuthorizedHttpClient { get; set; }
        protected HttpClient PublicHttpClient { get; set; }

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
            AuthorizedHttpClient = HttpClientFactory.CreateClient("MyProject.Web.ServerAPI");
            PublicHttpClient = HttpClientFactory.CreateClient("MyProject.Web.PublicServerAPI");
        }
    }
}
