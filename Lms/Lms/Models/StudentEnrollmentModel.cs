using System;
using System.ComponentModel.DataAnnotations;

namespace Lms.Models
{
    public class StudentEnrollmentModel
    {
        public Guid CourseId { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z][A-Za-z ]+$", ErrorMessage = "Please enter Course name starting with capital letter, lowercase for the remaining letters.")]
        public string CourseName { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Please enter a date in a valid format: yyyy-mm-dd.")]
        public string StartDate { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Please enter a date in a valid format: yyyy-mm-dd.")]
        public string EndDate { get; set; }
        public bool Cancelled { get; set; }
        public string CancellationReason { get; set; }
        public bool HasPassed { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Please enter an E-Mail in a valid format: example@vu.com.")]
        public string TeacherEmail { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Please enter an E-Mail in a valid format: example@vu.com.")]
        public string StudentEmail { get; set; }
    }
}

