using MyProject.Core.Entities.Organization;
using MyProject.Core.Helper;

namespace MyProject.Web.Client.Modules.Messages.Models
{
    public class MessagesGroup : Group
    {
        public MessagesGroup() : base()
        {
            Module = Constants.MessagesModule;
        }

        public static MessagesGroup Create(Group group)
        {
            return group.ConvertTo<MessagesGroup>();
        }
    }
}
