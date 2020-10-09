using MyProject.Data;
using MyProject.Data.Identity;
using MyProject.Services;
using MyProject.Services.Content;
using MyProject.Web.Server.Hubs;
using MyProject.Web.Shared;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Http;

namespace MyProject.Web.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMyProjectIdentity(Configuration);
            services.AddMyProjectDataProvider(Configuration);

            // MyProject service additions
            // https://docs.microsoft.com/en-us/aspnet/core/security/blazor/webassembly/hosted-with-identity-server?view=aspnetcore-3.1&tabs=visual-studio
            services.Configure<IdentityOptions>(options =>
                options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);
            services.AddMyProjectIdentityRepositories();
            services.AddMyProjectApplicationRepositories();
            services.AddMyProjectServices(Configuration);
            services.AddSignalR();
            services.AddControllersWithViews();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Blazorworld API",
                    Description = "Social Publishing System API",
                    TermsOfService = new Uri("https://myproject.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Jase Banico",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/jasebanico"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "GPL",
                        Url = new Uri("https://www.gnu.org/licenses/gpl-3.0.html"),
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddResponseCompression(opts => // for SignalR
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            // MyProject configurations
            app.UseResponseCompression(); // for SignalR
            UseSwagger(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UpdateMyProjectIdentityDatabase();
            app.UpdateMyProjectApplicationDatabase();
            services.UseMyProjectSecurity(Configuration);

            app.Use(async (ctx, next) =>
            {
                ctx.Request.Scheme = "http";
                ctx.Request.Host = new HostString("167.172.118.170");
                await next();
            });
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHub<MessagesHub>(Constants.MessagesHubPattern);
                endpoints.MapFallbackToFile("index.html");
            });
        }

        private void UseSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyProject API V1");
            });
        }
    }
}
