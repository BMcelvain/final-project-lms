using System;
using System.ComponentModel.DataAnnotations;

namespace Lms.Models
{
    public class StudentEnrollmentModel
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool Cancelled { get; set; }
        public string CancellationReason { get; set; }
        public bool HasPassed { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Please enter an E-Mail in a valid format: example@vu.com.")]
        public string TeacherEmail { get; set; }

        [Required(ErrorMessage = "Please enter phone number in a valid format: XXX-XXX-XXXX.")]
        [Phone]
        public string StudentPhone { get; set; }
        public int TotalPassCourses { get; set; }
    }
}

