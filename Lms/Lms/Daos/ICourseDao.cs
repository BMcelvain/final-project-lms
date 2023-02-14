using Lms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lms.Daos
{
    // Interfaces show a blueprint (no logic) of the methods for the class. 
    public interface ICourseDao
    {
        Task CreateCourse(CourseModel newCourse);

        Task<IEnumerable<CourseModel>> GetCourses();
        Task<IEnumerable<CourseModel>> GetCourseByStatus(string status);

        Task<CourseModel> GetCourseById(int id);

        Task PartiallyUpdateCourseById(CourseModel updateRequest);

        Task DeleteCourseById(int id);
    }
}
