using System;

namespace Lms.Models
{
    public class StudentInCourseModel
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public bool Cancelled { get; set; }
        public string CancellationReason { get; set; }
        public bool HasPassed { get; set; }
    }
}