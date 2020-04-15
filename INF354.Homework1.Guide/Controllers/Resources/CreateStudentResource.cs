using System.ComponentModel.DataAnnotations;

namespace INF354.Homework1.Guide.Controllers.Resources
{
    public class CreateStudentResource
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}