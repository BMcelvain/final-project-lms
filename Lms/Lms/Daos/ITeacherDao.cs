using Lms.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace Lms.Daos
{
    public interface ITeacherDao
    {
        Task CreateTeacher(TeacherModel newTeacher);
        Task<IEnumerable<TeacherModel>>GetTeacher(Guid TeacherId, string TeacherLastName, string TeacherPhone, string TeacherStatus);
        Task PartiallyUpdateTeacherById(TeacherModel updateRequest);
        Task DeleteTeacherById(Guid id);
    }
}