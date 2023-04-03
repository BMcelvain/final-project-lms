using System;
using System.ComponentModel.DataAnnotations;

namespace Lms.Models
{
    public class StudentModel
    {
        public Guid StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }

        [Required(ErrorMessage = "Please enter phone number in a valid format: XXX-XXX-XXXX.")]
        [Phone]
        public string StudentPhone { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Please enter an E-Mail in a valid format: example@vu.com.")]
        public string StudentEmail { get; set; }
        public string StudentStatus { get; set; }
        public int TotalPassCourses { get; set; }
    }
}