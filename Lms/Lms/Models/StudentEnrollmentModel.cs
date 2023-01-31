namespace Lms.Models
{
    public class StudentEnrollmentModel
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int SemesterId { get; set; }
        public int StudentId { get; set;}
        public string EnrollmentDate { get; set; }
        public bool Cancelled { get; set; }
        public string CancellationReason { get; set; }
        public bool HasPassed { get; set; }
    }
}
