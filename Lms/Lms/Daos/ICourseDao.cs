using Lms.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lms.Daos
{
    public interface ICourseDao
    {
        Task CreateCourse(CourseModel newCourse);
        Task<IEnumerable<CourseModel>> GetCourseByStatus(string status);
        Task<T> GetCourseById<T>(Guid id);
        Task PartiallyUpdateCourseById(CourseModel updateRequest);
        Task DeleteCourseById(Guid id);

    }
}