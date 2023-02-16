namespace Lms.Models
{
    public class StudentActiveEnrollmentModel
    {
        public string StudentLastName { get; set; }
        public string StudentFirstName { get; set; }
        public int StudentId { get; set; }
        public string StudentPhone { get; set; }
        public string StudentEmail { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CourseStatus { get; set; }
        public int SemesterId { get; set; }
        public string EnrollmentDate { get; set; }
        public int TeacherId { get; set; }
        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }
        public string TeacherPhone { get; set; }
        public string TeacherEmail { get; set; }
        public string TeacherStatus { get; set; }
    }
}
