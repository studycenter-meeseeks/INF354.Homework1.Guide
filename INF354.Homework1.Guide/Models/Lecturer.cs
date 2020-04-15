using System.Collections.Generic;

namespace INF354.Homework1.Guide.Models
{
    public class Lecturer
    {
        public Lecturer()
        {
            //Always initialize your lists
            Modules = new List<Module>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<Module> Modules { get; set; }
    }
}