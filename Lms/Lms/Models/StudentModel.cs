namespace Lms.Models
{
    public class StudentModel
    {
        public int StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentPhone { get; set; }
        public string StudentEmail { get; set; }
        public string StudentStatus { get; set; }
        public int TotalPassCourses { get; set; }
    }
}
