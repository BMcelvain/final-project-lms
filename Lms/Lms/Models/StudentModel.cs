using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Azure.Core.HttpHeader;
using System.Numerics;

namespace Lms.Models
{
    public class StudentModel
    {
        public Guid StudentId { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z][A-Za-z]+}$", ErrorMessage = "Please enter first name starting with capital letter, lowercase for the remaining letters.")]
        public string StudentFirstName { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z][A-Za-z-]+}$", ErrorMessage = "Please enter last name starting with capital letter, lowercase for the remaining letters.Hyphenated last names are acceptable.")]
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