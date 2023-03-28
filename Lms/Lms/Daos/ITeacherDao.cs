using Lms.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lms.Daos
{
    public interface ITeacherDao
    {
        Task CreateTeacher(TeacherModel newTeacher);
        Task<TeacherModel> GetTeacherById(Guid id);
        Task<IEnumerable<TeacherModel>> GetTeacherByStatus(string status);
        Task PartiallyUpdateTeacherById(TeacherModel updateRequest);
        Task DeleteTeacherById(Guid id);
    }
}