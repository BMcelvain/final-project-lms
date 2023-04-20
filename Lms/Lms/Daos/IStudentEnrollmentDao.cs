using Lms.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lms.Daos
{
    public interface IStudentEnrollmentDao
    {
        Task<IEnumerable<StudentEnrollmentModel>> GetStudentEnrollmentHistory(Guid StudentId,string StudentPhone, string StudentStatus, string Cancelled, string HasPassed);
        Task<IEnumerable<StudentModel>> GetStudentsInCourseByCourseId(Guid id);
    }
}