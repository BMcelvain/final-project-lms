using Lms.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lms.Daos
{
    public interface IStudentDao
    {
        Task CreateStudent(StudentModel newStudent);
        Task<StudentModel> GetStudentById(Guid id);
        Task PartiallyUpdateStudentById(StudentModel updateRequest);
        Task DeleteStudentById(Guid id);      
    }
}