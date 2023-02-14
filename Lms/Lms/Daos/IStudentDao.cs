﻿using Lms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lms.Daos
{
    // Interfaces show a blueprint (no logic) of the methods for the class. 
    public interface IStudentDao
    {
        Task CreateStudent(StudentModel newStudent);

        Task<IEnumerable<StudentModel>> GetStudents();

        Task<StudentModel> GetStudentById(int id);

        Task PartiallyUpdateStudentById(StudentModel updateRequest);

        Task DeleteStudentById(int id);
        
    }
}
