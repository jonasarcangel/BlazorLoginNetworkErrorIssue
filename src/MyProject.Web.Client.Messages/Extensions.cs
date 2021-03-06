﻿using MyProject.Web.Client.Messages.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MyProject.Web.Client.Messages
{
    public static class Extensions
    {
        public static void AddMessagesServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<HubClientService>();
            serviceCollection.AddTransient<IMessageService, MessageService>();
        }
    }
}
