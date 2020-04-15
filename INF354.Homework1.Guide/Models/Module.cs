namespace INF354.Homework1.Guide.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual Student Student { get; set; }
        public int StudentId { get; set; }
        public virtual Lecturer Lecturer { get; set; }
        public int LecturerId { get; set; }

    }
}