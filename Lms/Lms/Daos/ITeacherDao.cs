using Lms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lms.Daos
{
    public interface ITeacherDao
    {
        Task CreateTeacher(TeacherModel newTeacher);
        Task<IEnumerable<TeacherModel>> GetTeachers();
        Task<TeacherModel> GetTeacherById(int id);
        Task<IEnumerable<TeacherModel>> GetTeacherByStatus(string status);
        Task PartiallyUpdateTeacherById(TeacherModel updateRequest);
        Task DeleteTeacherById(int id);
    }
}