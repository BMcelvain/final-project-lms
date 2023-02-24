using Lms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lms.Daos
{
    // Interfaces show a blueprint (no logic) of the methods for the class. 
    public interface IAddStudentToCourseDao
    {

        Task AddStudentToCourse(AddStudentToCourseModel newStudentToCourse);

        Task<AddStudentToCourseModel> GetCourseByCourseId(int id);

        Task PartiallyUpdateStudentInCourseByCourseStudentId(AddStudentToCourseModel updateRequest);

        Task DeleteStudentInCourseByStudentCourseId(int studentId, int courseId);

    }
}
