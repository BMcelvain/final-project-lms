using Lms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lms.Daos
{
    public interface IStudentEnrollmentDao
    {
        Task<IEnumerable<StudentEnrollmentModel>> GetStudentEnrollmentHistoryById(int id);
        Task<IEnumerable<StudentEnrollmentModel>> GetStudentEnrollmentHistoryByStudentFirstName(string studentFirstName);
    }
}
