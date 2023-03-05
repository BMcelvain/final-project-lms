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
        Task AddStudentToCourse(StudentEnrollmentModel newStudentToCourse);
        Task<StudentEnrollmentModel> GetCourseByCourseId(int id);
        Task PartiallyUpdateStudentInCourseByCourseStudentId(StudentEnrollmentModel updateRequest);
        Task DeleteStudentInCourseByStudentCourseId(int studentId, int courseId);
    }
}
