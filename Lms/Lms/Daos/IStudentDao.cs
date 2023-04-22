using Lms.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lms.Daos
{
    public interface IStudentDao
    {
        Task CreateStudent(StudentModel newStudent);
        Task<T> GetStudentById<T>(Guid id);
        Task<IEnumerable<StudentEnrollmentModel>> GetStudentEnrollmentHistory(Guid StudentId, string StudentPhone, string Cancelled, string HasPassed);
        Task<IEnumerable<StudentEnrollmentModel>> GetStudentsInCourseByCourseId(Guid id);
        Task PartiallyUpdateStudentById(StudentModel updateRequest);
        Task DeleteStudentById(Guid id);
        
    }
}