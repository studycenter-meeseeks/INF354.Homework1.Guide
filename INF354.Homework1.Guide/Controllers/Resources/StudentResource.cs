using System.Collections.Generic;

namespace INF354.Homework1.Guide.Controllers.Resources
{
    public class StudentResource
    {
        public StudentResource()
        {
            //Always initialize your lists
            ModuleResources = new List<ModuleResource>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<ModuleResource> ModuleResources { get; set; }
    }
}