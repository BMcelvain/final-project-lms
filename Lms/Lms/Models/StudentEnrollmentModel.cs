namespace Lms.Models
{
    public class StudentEnrollmentModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int SemesterId { get; set; }
        public bool Cancelled { get; set; }
        public string CancellationReason { get; set; }
        public bool HasPassed { get; set; }
        public string EnrollmentDate { get; set; }
        public string TeacherEmail { get; set; }
        public int StudentId { get; set; }
        public string StudentPhone { get; set; }
        public int TotalPassCourses { get; set; }
    }
}

