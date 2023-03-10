using Lms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lms.Daos
{
    public interface ICourseDao
    {
        Task CreateCourse(CourseModel newCourse);
        Task<IEnumerable<CourseModel>> GetCourseByStatus(string status);
        Task<CourseModel> GetCourseById(int id);
        Task PartiallyUpdateCourseById(CourseModel updateRequest);
        Task DeleteCourseById(int id);
        Task StudentInCourse(StudentInCourseModel newStudentToCourse);
        Task PartiallyUpdateStudentInCourseByCourseStudentId(StudentInCourseModel updateRequest);
        Task DeleteStudentInCourseByStudentCourseId(int studentId, int courseId);
    }
}
