namespace Lms.Models
{
    public class StudentModel
    {
        public StudentModel Student { get; set; }

        public void AddStudent(StudentModel expectedStudent)
        {
            Student = expectedStudent;
        }
        public int StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentPhone { get; set; }
        public string StudentEmail { get; set; }
        public string StudentStatus { get; set; }
        public int TotalPassCourses { get; set; }
    }
}
