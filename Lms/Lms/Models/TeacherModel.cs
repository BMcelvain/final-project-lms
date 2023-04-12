using System;
using System.ComponentModel.DataAnnotations;

namespace Lms.Models
{
    public class TeacherModel
    {
        public Guid TeacherId { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z][A-Za-z]+$", ErrorMessage = "Please enter first name starting with capital letter, lowercase for the remaining letters.")]
        public string TeacherFirstName { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z][A-Za-z]+$", ErrorMessage = "Please enter last name starting with capital letter, lowercase for the remaining letters. Hyphenated last names are acceptable.")]
        public string TeacherLastName { get; set; }

        [Required(ErrorMessage = "Please enter phone number in a valid format: e.g. XXX-XXX-XXXX.")]
        [Phone]
        public string TeacherPhone { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Please enter an E-Mail in a valid format: example@vu.com.")]
        public string TeacherEmail { get; set; }
        public string TeacherStatus { get; set; }
    }
}
