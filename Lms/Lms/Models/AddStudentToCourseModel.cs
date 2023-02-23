namespace Lms.Models
{
    public class AddStudentToCourseModel
    {


        public int StudentId { get; set; }
        //public string StudentFirstName { get; set; }
        // public string StudentLastName { get; set; }
        public int CourseId { get; set; }
        //public string CourseName { get; set; }
        //public string StartDate { get; set; }
        // public string EndDate { get; set; }
        //public string CourseStatus { get; set; }
        public int SemesterId { get; set; }
        //public string Semester { get; set; }
        public string EnrollmentDate { get; set; }
        // public string TeacherLastName { get; set; }
        public bool Cancelled { get; set; }
        public string CancellationReason { get; set; }
        public bool HasPassed { get; set; }
     

    }
}
