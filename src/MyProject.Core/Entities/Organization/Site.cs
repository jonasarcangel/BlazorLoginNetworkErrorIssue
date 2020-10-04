using MyProject.Core.Entities.Common;

namespace MyProject.Core.Entities.Organization
{
    public class Site : Entity
    {
        public string TenantId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}
