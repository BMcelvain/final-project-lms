﻿namespace Lms.Models
{
    public class TeacherModel
    {

        public TeacherModel Teacher { get; set; }

        public void AddTeacher(TeacherModel expectedTeacher)
        {
            Teacher = expectedTeacher;
        }
        public int TeacherId { get; set; }
        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }
        public string TeacherPhone { get; set; }
        public string TeacherEmail { get; set; }
        public string TeacherStatus { get; set; }
    }
}
