using Lms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lms.Daos
{
    public interface IStudentActiveEnrollmentDao
    {
        Task<IEnumerable<StudentActiveEnrollmentModel>> GetActiveStudentEnrollmentByStudentLastName(string studentLastName);
        Task<IEnumerable<StudentActiveEnrollmentModel>> GetActiveStudentEnrollmentByStudentPhone(string studentPhone);
    }
}
