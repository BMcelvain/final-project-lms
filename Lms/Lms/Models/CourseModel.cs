using System;

namespace Lms.Models
{
    public class CourseModel
    { 
        public Guid CourseId { get; set; }
        public Guid TeacherId { get; set; }
        public string CourseName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CourseStatus { get; set; }
    }
}