using IdentityServer4.Configuration;
using MyProject.Data.Identity.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using Microsoft.AspNetCore.Authentication;

namespace MyProject.Data.Identity
{
    public static class Extensions
    {
        public static void AddMyProjectIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var provider = configuration["IdentityDbProvider"];
            if (string.IsNullOrEmpty(provider))
            {
                provider = "sqlite";
            }
            string connectionString = configuration.GetConnectionString("IdentityDbConnection");
            switch (provider.ToLower())
            {
                case "mssql":
                    services.AddDbContext<ApplicationIdentityDbContext>(options =>
                        options.UseSqlServer(connectionString));
                    break;
                case "mysql":
                    services.AddDbContext<ApplicationIdentityDbContext>(options =>
                        options.UseMySql(connectionString));
                    break;
                case "sqlite":
                    if (string.IsNullOrEmpty(connectionString))
                    {
                        var identityDbFilename = configuration["IdentityDbFilename"];
                        if (string.IsNullOrEmpty(identityDbFilename))
                            identityDbFilename = "myproject-identity.db";
                        var connectionStringBuilder = new Microsoft.Data.Sqlite.SqliteConnectionStringBuilder { DataSource = identityDbFilename };
                        connectionString = connectionStringBuilder.ToString();
                    }
                    services.AddDbContext<ApplicationIdentityDbContext>(options =>
                        options.UseSqlite(connectionString));
                    break;
            }

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();

            //services.AddIdentityServer(options =>
            //{
            //    options.IssuerUri = "http://167.172.118.170/";
            //})
            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser,
                ApplicationIdentityDbContext>(); 
            services.AddAuthentication()
                .AddIdentityServerJwt();
        }

        public static void UpdateMyProjectIdentityDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<ApplicationIdentityDbContext>();
            context.Database.Migrate();
        }

        public static void AddMyProjectIdentityRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
