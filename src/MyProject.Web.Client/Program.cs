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

            var configuration = builder.Configuration.Build();
            builder.Services.AddOidcAuthentication(options =>
            {
	            options.ProviderOptions.Authority = $"{configuration["SiteScheme"]}://{configuration["SiteUrl"]}/";
                options.ProviderOptions.ClientId = "MyProject";
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                builder.Configuration.Bind("OidcConfiguration", options.ProviderOptions);
            });
            // End MyProject.Web.Client Updates

            builder.Services.AddHttpClient("MyProject.Web.ServerAPI",
                //client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                client => client.BaseAddress = new Uri($"{configuration["SiteScheme"]}://{configuration["SiteUrl"]}/"))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
            builder.Services.AddHttpClient("MyProject.Web.PublicServerAPI",
                //client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
                client => client.BaseAddress = new Uri($"{configuration["SiteScheme"]}://{configuration["SiteUrl"]}/"));

            await builder.Build().RunAsync();
        }
    }
}
