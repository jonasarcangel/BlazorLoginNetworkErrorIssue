using MyProject.Web.Client.Common;
using MyProject.Web.Client.Messages;
using MyProject.Web.Client.Shell;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyProject.Web.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("MyProject.Web.ServerAPI",
                client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            builder.Services.AddHttpClient("MyProject.Web.PublicServerAPI", 
                client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("MyProject.Web.ServerAPI"));

            builder.Services.AddApiAuthorization()
                .AddAccountClaimsPrincipalFactory<CustomUserFactory>();

            // Start MyProject.Web.Client Updates
            builder.Services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
            builder.Services.AddCommonServices();
            builder.Services.AddMessagesServices();
            builder.Services.AddShellServices();

            builder.Services.AddApiAuthorization();
            //builder.Services.AddOidcAuthentication(options =>
            //{
            //    //options.ProviderOptions.Authority = "https://localhost:5001/";
            //    // Configure your authentication provider options here.
            //    // For more information, see https://aka.ms/blazor-standalone-auth
            //    builder.Configuration.Bind("oidc", options.ProviderOptions);
            //});
            // End MyProject.Web.Client Updates

            await builder.Build().RunAsync();
        }
    }
}
