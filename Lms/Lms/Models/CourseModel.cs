namespace Lms.Models
{
    public class CourseModel
    {
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public string CourseName { get; set; }
        public int SemesterId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CourseStatus { get; set; }
    }
}