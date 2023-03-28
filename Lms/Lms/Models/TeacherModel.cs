using System;

namespace Lms.Models
{
    public class TeacherModel
    {
        public Guid TeacherId { get; set; }
        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }
        public string TeacherPhone { get; set; }
        public string TeacherEmail { get; set; }
        public string TeacherStatus { get; set; }
    }
}
