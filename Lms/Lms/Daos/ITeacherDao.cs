using Lms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lms.Daos
{
    // Interfaces show a blueprint (no logic) of the methods for the class. 
    public interface ITeacherDao
    {
        void GetTeacher(bool shouldCallSql = true); //testing

        Task CreateTeacher(TeacherModel newTeacher);

        Task<IEnumerable<TeacherModel>> GetTeacher();

        Task<TeacherModel> GetTeacherById(int id);

        Task PartiallyUpdateTeacherById(TeacherModel updateRequest);

        Task DeleteTeacherById(int id);
    }
}