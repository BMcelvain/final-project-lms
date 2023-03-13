using Lms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lms.Daos
{
    public interface IStudentEnrollmentDao
    {
        Task<IEnumerable<StudentEnrollmentModel>> GetStudentEnrollmentHistoryById(int id);
        Task<IEnumerable<StudentEnrollmentModel>> GetStudentEnrollmentHistoryByStudentLastName(string studentLastName);
        Task<IEnumerable<StudentEnrollmentModel>> GetActiveStudentEnrollmentByStudentPhone(string studentPhone);
        Task<IEnumerable<StudentModel>> GetStudentsByCourseId(int id);
    }
}
