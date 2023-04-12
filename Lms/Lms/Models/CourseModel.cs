using System;
using System.ComponentModel.DataAnnotations;

namespace Lms.Models
{
    public class CourseModel
    { 
        public Guid CourseId { get; set; }

        [Required]
        [RegularExpression(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$", ErrorMessage = "Please enter a valid guid in the correct format.")]
        public Guid TeacherId { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z][A-Za-z]+$", ErrorMessage = "Please enter Course name starting with capital letter, lowercase for the remaining letters.")]
        public string CourseName { get; set; }
        [Required]
        [RegularExpression(@"^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Please enter a date in a valid format: yyyy-mm-dd.")]
        public string StartDate { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Please enter a date in a valid format: yyyy-mm-dd.")]
        public string EndDate { get; set; }
        [Required]
        public string CourseStatus { get; set; }
    }
}