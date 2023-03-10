namespace Lms.Models
{
    public class StudentInCourseModel
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string EnrollmentDate { get; set; }
        public bool Cancelled { get; set; }
        public string CancellationReason { get; set; }
        public bool HasPassed { get; set; }

    }
}

