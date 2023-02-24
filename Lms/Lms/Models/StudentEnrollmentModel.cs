﻿namespace Lms.Models
{
    public class StudentEnrollmentModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool Cancelled { get; set; }
        public string CancellationReason { get; set; }
        public bool HasPassed { get; set; }
        public string TeacherEmail { get; set; }
        public string StudentEmail { get; set; }
        public int TotalPassCourses { get; set; }
    }
}

