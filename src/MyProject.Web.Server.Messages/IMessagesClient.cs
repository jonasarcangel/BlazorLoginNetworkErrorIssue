using MyProject.Core.Entities.Content;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Web.Server.Messages
{
    public interface IMessagesClient
    {
        Task ReceiveMessageAsync(Message message);
    }
}
