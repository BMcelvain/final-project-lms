using System;

namespace Lms.Models
{
    public class CourseModel
    {
        public CourseModel Course { get; set; }

        public void AddCourse(CourseModel expectedCourse)
        {
            Course = expectedCourse;
        } 

        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public string CourseName { get; set; }
        public int SemesterId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CourseStatus { get; set; }
        
    }
}
