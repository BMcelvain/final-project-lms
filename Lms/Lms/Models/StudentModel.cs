using System;
using System.ComponentModel.DataAnnotations;

namespace Lms.Models
{
    public class StudentModel
    {
        public Guid StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }

        [Required(ErrorMessage = "Please enter phone number")]
        [Display(Name = "Phone Number")]
        [Phone]
        public string StudentPhone { get; set; }

        [Required(ErrorMessage = "Please enter email address")]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string StudentEmail { get; set; }
        public string StudentStatus { get; set; }
        public int TotalPassCourses { get; set; }
    }
}